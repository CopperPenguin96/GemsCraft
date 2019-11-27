using System;
using JetBrains.Annotations;

namespace GemsCraft.AppSystem.Exceptions
{
    public sealed class RankDefinitionException: Exception
    {
        public string RankName { get; }

        public RankDefinitionException(string rankName, string message)
            : base(message)
        {
            RankName = rankName;
        }

        [StringFormatMethod("message")]
        public RankDefinitionException(string rankName, string message, params object[] args)
            : base(string.Format(message, args))
        {
            RankName = rankName;
        }
    }
}
