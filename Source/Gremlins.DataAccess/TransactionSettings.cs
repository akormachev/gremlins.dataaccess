using System;
using System.Transactions;

namespace Gremlins.DataAccess
{
    public sealed class TransactionSettings
    {
        public TransactionScopeOption ScopeOption { get; set; }

        public TransactionOptions Options { get; set; }

        private readonly static TransactionSettings _default = new TransactionSettings
        {
            Options = new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromMinutes(5) },
            ScopeOption = TransactionScopeOption.Required
        };

        public static TransactionSettings Default
        {
            get { return _default; }
        }
    }
}
