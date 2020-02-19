namespace GemsCraft.AppSystem
{
    /// <summary>
    /// Reasons that lead to shutdowns
    /// </summary>
    public enum ShutdownReason
    {
        Unknown,

        /// <summary>
        /// Use for mod or plugin triggers
        /// </summary>
        Other,

        /// <summary>
        /// InitLibrary or InitServer failed D:
        /// </summary>
        FailedToInitialize,

        /// <summary>
        /// Server start failed
        /// </summary>
        FailedToStart,

        /// <summary>
        /// Server is just simply restarting
        /// </summary>
        Restarting,

        /// <summary>
        /// The server has experienced a non-recoverable error.
        /// </summary>
        Crashed,

        /// <summary>
        /// Server is shutting down
        /// </summary>
        ShuttingDown,

        /// <summary>
        /// Server process is being closed/killed.
        /// </summary>
        ProcessClosing,

        /// <summary>
        /// Server is being shutdown for a software update.
        /// </summary>
        Updating
    }
}
