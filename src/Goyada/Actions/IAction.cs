using System;
using System.Collections.Generic;
using System.Text;

namespace Goyada.Actions
{
    public interface IAction
    {
        string Method { get; }
        DateTime TimeStamp { get; set; }
        string MerchantId { get; set; }
    }
}
