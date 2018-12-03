using System;
using System.Collections.Generic;
using System.Text;

namespace Goyada
{
    public class Constants
    {
        public enum TransactionStateName
        {
            PENDING, AUTHORIZED, DENIED, CLEARED, SETTLED, NOSETTLEMENT, CANCELLED, EXPIRED, REVERSEOK, UNKNOWN
        }

        public enum TransactionType
        {
            PURCHASE, RELOAD, RETURN, EMPTYCARD
        }

        public enum CardState
        {
            VALID, EXPIRED, CREATED, BLOCKED
        }
    }
}
