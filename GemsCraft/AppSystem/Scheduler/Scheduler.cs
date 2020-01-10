// Copyright 2009-2012 Matvei Stefarov <me@matvei.org>
// Modified by apotter96 for GemsCraft

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using GemsCraft.AppSystem.Logging;
using GemsCraft.Configuration;
using GemsCraft.Utils;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Scheduler
{
    public static class Scheduler
    {
        private static readonly HashSet<SchedulerTask> Tasks = new HashSet<SchedulerTask>();
        private static SchedulerTask[] _taskCache;
        private static readonly Queue<SchedulerTask> BackgroundTasks = new Queue<SchedulerTask>();
        private static readonly object TaskListLock = new object();
        private static readonly object BackgroundTaskListLock = new object();

        private static Thread _schedulerThread;
        private static Thread _backgroundThread;

        internal static void Start()
        {
            _schedulerThread = new Thread(MainLoop)
            {
                Name = "GemsCraft.Main"
            };

            _schedulerThread.Start();

            _backgroundThread = new Thread(BackgroundLoop)
            {
                Name = "GemsCraft.Background"
            };
            _backgroundThread.Start();
        }

        private static void MainLoop()
        {
            while (!Server.IsShuttingDown)
            {
                DateTime ticksNow = DateTime.UtcNow;

                SchedulerTask[] taskListCache = _taskCache;

                for (int i = 0; i < taskListCache.Length && !Server.IsShuttingDown; i++)
                {
                    SchedulerTask task = taskListCache[i];
                    if (task.IsStopped || task.NextTime > ticksNow) continue;
                    if (task.IsRecurring && task.AdjustForExecutionTime)
                    {
                        task.NextTime += task.Interval;
                    }

                    if (task.IsBackground)
                    {
                        lock (BackgroundTaskListLock)
                        {
                            BackgroundTasks.Enqueue(task);
                        }
                    }
                    else
                    {
                        task.IsExecuting = true;
                        try
                        {
                            task.Callback(task);
                        }
                        catch (Exception ex)
                        {
                            Logger.Write("Exception thrown by Scheduler Task callback", LogType.SeriousError);
                            Logger.Write(ex.ToString(), LogType.SeriousError);
                        }
                        finally
                        {
                            task.IsExecuting = false;
                        }
                    }

                    if (!task.IsRecurring || task.MaxRepeats == 1)
                    {
                        task.Stop();
                        continue;
                    }

                    task.MaxRepeats--;

                    ticksNow = DateTime.UtcNow;
                    if (!task.AdjustForExecutionTime)
                    {
                        task.NextTime = ticksNow.Add(task.Interval);
                    }
                }

                Thread.Sleep(10);
            }
        }

        private static void BackgroundLoop()
        {
            while (!Server.IsShuttingDown)
            {
                lock (BackgroundTaskListLock)
                {
                    if (BackgroundTasks.Count > 0)
                    {
                        SchedulerTask task;
                        lock (BackgroundTaskListLock)
                        {
                            task = BackgroundTasks.Dequeue();
                        }
                        task.IsExecuting = true;
                        try
                        {
                            task.Callback(task);
                        }
                        catch (Exception ex)
                        {
                            Logger.Write("Exception thrown by ScheduledTask callback", LogType.SeriousError);
                            Logger.Write(ex.ToString(), LogType.SeriousError);
                        }
                        finally
                        {
                            task.IsExecuting = false;
                        }
                    }
                }

                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Schedules a given task for execution.
        /// </summary>
        /// <param name="task"> Task to schedule. </param>
        internal static void AddTask([NotNull] SchedulerTask task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            lock (TaskListLock)
            {
                if (Server.IsShuttingDown) return;
                task.IsStopped = false;
                if (Tasks.Add(task))
                {
                    UpdateCache();
                }
            }
        }

        /// <summary>
        /// Creates a new SchedulerTask object to run in the main thread.
        /// Use this if your task is time-sensitive or frequent, and your
        /// callback won't take too long to execute.
        /// </summary>
        /// <param name="callback"> Method to call when the task is triggered. </param>
        /// <returns> Newly created SchedulerTask object. </returns>
        public static SchedulerTask NewTask([NotNull] SchedulerCallback callback)
        {
            return new SchedulerTask(callback, false);
        }

        /// <summary>
        /// Creates a new SchedulerTask object to run in the background thread.
        /// Use this if your task is not very time-sensitive or frequent, or if
        /// your callback is resource-intensive.
        /// </summary>
        /// <param name="callback"> Method to call when the task is triggered. </param>
        /// <returns> Newly created SchedulerTask object. </returns>
        public static SchedulerTask NewBackgroundTask([NotNull] SchedulerCallback callback)
        {
            return new SchedulerTask(callback, true);
        }


        /// <summary>
        /// Creates a new SchedulerTask object to run in the main thread.
        /// Use this if your task is time-sensitive or frequent, and your
        /// callback won't take too long to execute.
        /// </summary>
        /// <param name="callback"> Method to call when the task is triggered. </param>
        /// <param name="userState"> Parameter to pass to the method. </param>
        /// <returns> Newly created SchedulerTask object. </returns>
        public static SchedulerTask NewTask([NotNull] SchedulerCallback callback, [CanBeNull] object userState)
        {
            return new SchedulerTask(callback, false, userState);
        }


        /// <summary>
        /// Creates a new SchedulerTask object to run in the background thread.
        /// Use this if your task is not very time-sensitive or frequent, or
        /// if your callback is resource-intensive. 
        /// </summary>
        /// <param name="callback"> Method to call when the task is triggered. </param>
        /// <param name="userState"> Parameter to pass to the method. </param>
        /// <returns> Newly created SchedulerTask object. </returns>
        public static SchedulerTask NewBackgroundTask([NotNull] SchedulerCallback callback, [CanBeNull] object userState)
        {
            return new SchedulerTask(callback, true, userState);
        }

        /// <summary>
        /// Removes stopped tasks from the list.
        /// </summary>
        internal static void UpdateCache()
        {
            List<SchedulerTask> newList = new List<SchedulerTask>();
            List<SchedulerTask> deletionList = new List<SchedulerTask>();
            lock (TaskListLock)
            {
                foreach (SchedulerTask task in Tasks)
                {
                    if (task.IsStopped)
                    {
                        deletionList.Add(task);
                    }
                    else
                    {
                        newList.Add(task);
                    }
                }

                foreach (var t in deletionList)
                {
                    Tasks.Remove(t);
                }
            }
            _taskCache = newList.ToArray();
        }

        internal static void BeginShutdown()
        {
            if (Config.Advanced.HeartbeatSaverEnabled)
            {
                if (!Server.IsRestarting)
                {
                    try
                    {
                        if (!File.Exists("HeartbeatSaver.exe"))
                        {
                            Logger.Log(LogType.Warning, "heartbeatsaver.exe does not exist and failed to launch");
                            return;
                        }

                        // start the heartbeat saver in win
                        if (!MonoCompat.IsMono)
                        {
                            Process heartbeatSaver = new Process();
                            Logger.Log(LogType.System, "Starting the HeartBeat Saver");
                            heartbeatSaver.StartInfo.FileName = "HeartbeatSaver.exe";
                            heartbeatSaver.Start();
                        }
                        //run heartbeat saver in mono environment
                        else
                        {
                            try
                            {
                                Logger.Log(LogType.System, "Starting the HeartBeat Saver");
                                ProcessStartInfo proc = new ProcessStartInfo("mono")
                                {
                                    Arguments = "HeartbeatSaver.exe",
                                    UseShellExecute = true,
                                    CreateNoWindow = true
                                };
                                Process.Start(proc);
                            }
                            catch (Exception e)
                            {
                                Logger.Log(LogType.Warning, e.ToString());
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.Log(LogType.Error, "HeartBeatSaver: " + ex);
                    }
                }
                else
                    Logger.Log(LogType.System, "HeartBeat Saver was not launched");
            }
            lock (TaskListLock)
            {
                foreach (SchedulerTask task in Tasks)
                {
                    task.Stop();
                }
                Tasks.Clear();
                _taskCache = new SchedulerTask[0];
            }

            // reset all tempranks
            // TODO: TEMPRANK
            /*foreach (Player p in Server.Tem)
            {
                if (p.isTempRanked)
                {
                    p.ChangeRank(Player.Console, p.PreviousRank, "Temprank interrupted by server shutdown.", false, true, false);
                }
            }*/
        }

        /// <summary>
        /// Makes sure that both scheduler threads finish and quit
        /// </summary>
        internal static void EndShutdown()
        {
            try
            {
                if (_schedulerThread != null && _schedulerThread.IsAlive)
                {
                    _schedulerThread.Join();
                }
                _schedulerThread = null;
            }
            catch (ThreadStateException) { }

            try
            {
                if (_backgroundThread != null && _backgroundThread.IsAlive)
                {
                    _backgroundThread.Join();
                }

                _backgroundThread = null;
            }
            catch (ThreadStateException)
            {

            }
        }
    }
}
