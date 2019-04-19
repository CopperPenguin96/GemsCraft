using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using GemsCraft.Utils;

namespace GemsCraft.AppSystem
{
    internal static class Files
    {
        // The main directory for GemsCraft
        public const string BaseDir = "GemsCraft/";

        // Paths
        public const string PlayerDatabaseDir = BaseDir + "PlayerDB/";
        public const string LogDir = BaseDir + "Logs/";
        public const string MapPath = BaseDir + "Maps/";
        public const string BlockDBDir = BaseDir + "BlockDB/";

        // Files
        public const string ConfigName = BaseDir + "config.json";
        public const string RankConfig = BaseDir + "ranks.xml";
        public const string IPBanList = BaseDir + "IPBanList.txt";
        public const string Rules = BaseDir + "rules.txt";
        public const string Announcements = BaseDir + "announcements.txt";
        public const string Greeting = BaseDir + "greeting.txt";
        public const string WorldConfig = BaseDir + "worlds.xml";
        public const string ReqsFile = BaseDir + "reqs.txt";
        public const string SwearFile = BaseDir + "swears.txt";
        public const string PortalDB = BaseDir + "portaldb.txt";


        private static readonly string[] ProtectedFiles;
        internal static readonly string[] DataFilesToBackup;
        
        static Files()
        {
            string assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            WorkingPathDefault = assemblyDir != null ? Path.GetFullPath(assemblyDir) : Path.GetPathRoot(assemblyDir);
            
            ProtectedFiles = new[]{
                "GemsCraftGUI.exe",
                "GemsCraft.dll",
                "ServerCLI.exe",
                ConfigName,
                PlayerDatabaseDir,
                IPBanList,
                Rules,
                Announcements,
                Greeting,
                WorldConfig,
                ReqsFile,
                SwearFile
            };
        }

        #region Paths & Properties

        public static bool IgnoreMapPathConfigKey { get; internal set; }

        public static readonly string WorkingPathDefault;
        
        /// <summary>
        /// Working path (default: whatever directory GemsCraft.dll is located in)
        /// Can be overriden at startup via command line argument "--path="
        /// </summary>
        public static string WorkingPath { get; set; }


        #endregion

        #region Utility Methods

        public static void MoveOrReplace(string source, string destination)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            if (File.Exists(destination))
            {
                if (Path.GetPathRoot(Path.GetFullPath(source)) == Path.GetPathRoot(Path.GetFullPath(destination)))
                {
                    string backupFileName = destination + ".bak";
                    File.Replace(source, destination, backupFileName, true);
                    File.Delete(backupFileName);
                }
                else
                {
                    File.Copy(source, destination, true);
                }
            }
            else
            {
                File.Move(source, destination);
            }
        }

        /// <summary>
        /// Makes sure that the path format is valid, that it exists, that it is accessible and writeable.
        /// </summary>
        /// <param name="pathLabel">Name of the path that's being tested (e.g. "map path"). Used for logging</param>
        /// <param name="path">Full or partial path.</param>
        /// <param name="checkForWriteAccess">If set, tries to write to the given directory.</param>
        /// <returns>Full path of the directory (on success) or null (on failure).</returns>
        public static bool TestDirectory(string pathLabel, string path, bool checkForWriteAccess)
        {
            if (pathLabel == null) throw new ArgumentNullException(nameof(pathLabel));
            if (path == null) throw new ArgumentNullException(nameof(path));
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                DirectoryInfo info = new DirectoryInfo(path);
                if (!checkForWriteAccess) return true;
                string randomFileName = Path.Combine(info.FullName, "GemsCraft_write_test_" + Guid.NewGuid());
                using (File.Create(randomFileName)) { }
                File.Delete(randomFileName);
                return true;

            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ArgumentException _:
                    case NotSupportedException _:
                    case PathTooLongException _:
                        Logger.Log(LogType.Error,
                            "Paths.TestDirectory: Specified path for {0} is invalid or incorrectly formatted ({1}: {2}).",
                            pathLabel, ex.GetType().Name, ex.Message);
                        break;
                    case SecurityException _:
                    case UnauthorizedAccessException _:
                        Logger.Log(LogType.Error,
                            "Paths.TestDirectory: Cannot create or write to file/path for {0}, please check permissions ({1}: {2}).",
                            pathLabel, ex.GetType().Name, ex.Message);
                        break;
                    case DirectoryNotFoundException _:
                        Logger.Log(LogType.Error,
                            "Paths.TestDirectory: Drive/volume for {0} does not exist or is not mounted ({1}: {2}).",
                            pathLabel, ex.GetType().Name, ex.Message);
                        break;
                    case IOException _:
                        Logger.Log(LogType.Error,
                            "Paths.TestDirectory: Specified directory for {0} is not readable/writable ({1}: {2}).",
                            pathLabel, ex.GetType().Name, ex.Message);
                        break;
                    default:
                        throw;
                }
            }
            return false;
        }

        /// <summary>
        /// Makes sure that the path format is valid, and optionally whether it is readable/writeable.
        /// </summary>
        /// <param name="fileLabel">Name of the path that's being tested (e.g. "map path"). Used for logging.</param>
        /// <param name="filename">Full or partial path of the file</param>
        /// <param name="createIfDoesNotExist">If target file is missing this option is OFF< TestFile returns true.</param>
        /// <param name="neededAccess">If file is present, type of access to test.</param>
        /// <returns>Whether target file passes all tests. A+</returns>
        public static bool TestFile(string fileLabel, string filename,
                                     bool createIfDoesNotExist, FileAccess neededAccess)
        {
            if (fileLabel == null) throw new ArgumentNullException(nameof(fileLabel));
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            try
            {
                var fileInfo = new FileInfo(filename);
                if (File.Exists(filename))
                {
                    if ((neededAccess & FileAccess.Read) == FileAccess.Read)
                    {
                        using (File.OpenRead(filename)) { }
                    }

                    if ((neededAccess & FileAccess.Write) != FileAccess.Write) return true;
                    using (File.OpenWrite(filename)) { }
                }
                else if (createIfDoesNotExist)
                {
                    using (File.Create(filename)) { }
                }
                return true;

            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ArgumentException _:
                    case NotSupportedException _:
                    case PathTooLongException _:
                        Logger.Log(LogType.Error,
                            "Paths.TestFile: Specified path for {0} is invalid or incorrectly formatted ({1}: {2}).",
                            fileLabel, ex.GetType().Name, ex.Message);
                        break;
                    case SecurityException _:
                    case UnauthorizedAccessException _:
                        Logger.Log(LogType.Error,
                            "Paths.TestFile: Cannot create or write to {0}, please check permissions ({1}: {2}).",
                            fileLabel, ex.GetType().Name, ex.Message);
                        break;
                    case DirectoryNotFoundException _:
                        Logger.Log(LogType.Error,
                            "Paths.TestFile: Drive/volume for {0} does not exist or is not mounted ({1}: {2}).",
                            fileLabel, ex.GetType().Name, ex.Message);
                        break;
                    case IOException _:
                        Logger.Log(LogType.Error,
                            "Paths.TestFile: Specified file for {0} is not readable/writable ({1}: {2}).",
                            fileLabel, ex.GetType().Name, ex.Message);
                        break;
                    default:
                        throw;
                }
            }
            return false;
        }

        public static bool IsDefaultMapPath(string path)
        {
            return string.IsNullOrEmpty(path) || Compare(MapPath, path);
        }


        /// <summary>
        /// Returns true if paths or filenames reference the same location (accounts for all the filesystem quirks).
        /// </summary>
        public static bool Compare(string p1, string p2)
        {
            if (p1 == null) throw new ArgumentNullException("p1");
            if (p2 == null) throw new ArgumentNullException("p2");
            return Compare(p1, p2, MonoCompat.IsCaseSensitive);
        }


        /// <summary>
        /// Returns true if paths or filenames reference the same location (accounts for all the filesystem quirks).
        /// </summary>
        public static bool Compare(string p1, string p2, bool caseSensitive)
        {
            if (p1 == null) throw new ArgumentNullException("p1");
            if (p2 == null) throw new ArgumentNullException("p2");
            StringComparison sc = (caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
            return string.Equals(Path.GetFullPath(p1).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar),
                Path.GetFullPath(p2).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar),
                sc);
        }

        public static bool IsValidPath(string path)
        {
            try
            {
                new FileInfo(path);
                return true;
            }
            catch (ArgumentException)
            {
            }
            catch (PathTooLongException)
            {
            }
            catch (NotSupportedException)
            {
            }
            return false;
        }

        /// <summary>
        /// Checks whether childPath is inside parentPath
        /// </summary>
        /// <param name="parentPath">Path that is supposed to contain the child</param>
        /// <param name="childPath">Path that is supposed to be the kid</param>
        /// <returns>Returns true if childPath is indeed the kid of parent</returns>
        public static bool Contains(string parentPath, string childPath)
        {
            if (parentPath == null) throw new ArgumentNullException("parentPath");
            if (childPath == null) throw new ArgumentNullException("childPath");
            return Contains(parentPath, childPath, MonoCompat.IsCaseSensitive);
        }

        /// <summary>
        /// Checks whether childPath is inside parentPath
        /// </summary>
        /// <param name="parentPath">Path that is supposed to contain the child</param>
        /// <param name="childPath">Path that is supposed to be the kid</param>
        /// <returns>Returns true if childPath is indeed the kid of parent</returns>
        public static bool Contains(string parentPath, string childPath, bool caseSensitive)
        {
            if (parentPath == null) throw new ArgumentNullException("parentPath");
            if (childPath == null) throw new ArgumentNullException("childPath");
            string fullParentPath = Path.GetFullPath(parentPath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            string fullChildPath = Path.GetFullPath(childPath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            StringComparison sc = (caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
            return fullChildPath.StartsWith(fullParentPath, sc);
        }
        /// <summary> Checks whether the file exists in a specified way (case-sensitive or case-insensitive) </summary>
        /// <param name="fileName"> filename in question </param>
        /// <param name="caseSensitive"> Whether check should be case-sensitive or case-insensitive. </param>
        /// <returns> true if file exists, otherwise false </returns>
        public static bool FileExists(string fileName, bool caseSensitive)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            if (caseSensitive == MonoCompat.IsCaseSensitive)
            {
                return File.Exists(fileName);
            }
            else
            {
                return new FileInfo(fileName).Exists(caseSensitive);
            }
        }


        /// <summary>Checks whether the file exists in a specified way (case-sensitive or case-insensitive)</summary>
        /// <param name="fileInfo">FileInfo object in question</param>
        /// <param name="caseSensitive">Whether check should be case-sensitive or case-insensitive.</param>
        /// <returns>true if file exists, otherwise false</returns>
        public static bool Exists(this FileInfo fileInfo, bool caseSensitive)
        {
            if (fileInfo == null) throw new ArgumentNullException("fileInfo");
            if (caseSensitive == MonoCompat.IsCaseSensitive)
            {
                return fileInfo.Exists;
            }
            else
            {
                DirectoryInfo parentDir = fileInfo.Directory;
                StringComparison sc = (caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
                return parentDir.GetFiles("*", SearchOption.TopDirectoryOnly)
                                .Any(file => file.Name.Equals(fileInfo.Name, sc));
            }
        }


        /// <summary> Allows making changes to filename capitalization on case-insensitive filesystems. </summary>
        /// <param name="originalFullFileName"> Full path to the original filename </param>
        /// <param name="newFileName"> New file name (do not include the full path) </param>
        public static void ForceRename(string originalFullFileName, string newFileName)
        {
            if (originalFullFileName == null) throw new ArgumentNullException("originalFullFileName");
            if (newFileName == null) throw new ArgumentNullException("newFileName");
            FileInfo originalFile = new FileInfo(originalFullFileName);
            if (originalFile.Name == newFileName) return;
            FileInfo newFile = new FileInfo(Path.Combine(originalFile.DirectoryName, newFileName));
            string tempFileName = originalFile.FullName + Guid.NewGuid();
            MoveOrReplace(originalFile.FullName, tempFileName);
            MoveOrReplace(tempFileName, newFile.FullName);
        }


        /// <summary> Find files that match the name in a case-insensitive way. </summary>
        /// <param name="fullFileName"> Case-insensitive filename to look for. </param>
        /// <returns> Array of matches. Empty array if no files matches. </returns>
        public static FileInfo[] FindFiles(string fullFileName)
        {
            if (fullFileName == null) throw new ArgumentNullException("fullFileName");
            FileInfo fi = new FileInfo(fullFileName);
            DirectoryInfo parentDir = fi.Directory;
            return parentDir.GetFiles("*", SearchOption.TopDirectoryOnly)
                            .Where(file => file.Name.Equals(fi.Name, StringComparison.OrdinalIgnoreCase))
                            .ToArray();
        }


        public static bool IsProtectedFileName(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            return ProtectedFiles.Any(t => Compare(t, fileName));
        }
        #endregion
    }
}
