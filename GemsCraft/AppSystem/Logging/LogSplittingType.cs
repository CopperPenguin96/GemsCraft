namespace GemsCraft.AppSystem.Logging
{
    /// <summary>
    /// Log splitting type
    /// </summary>
    public enum LogSplittingType
    {
        /// <summary>
        /// All logs are written to one file.
        /// </summary>
        OneFile,

        /// <summary>
        /// A new timestamped logfile is made every time the server is
        /// started.
        /// </summary>
        SplitBySession,

        /// <summary>
        /// A new timestamped logfile is created every 24 hours.
        /// </summary>
        SplitByDay
    }
}
