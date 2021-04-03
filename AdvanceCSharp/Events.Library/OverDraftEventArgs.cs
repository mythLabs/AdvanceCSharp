using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Library
{
    public class OverDraftEventArgs: EventArgs
    {
        public OverDraftEventArgs(decimal amountOverDrafted, string info)
        {
            AmountOverDrafted = amountOverDrafted;
            Info = info;
        }
        public decimal AmountOverDrafted { get; private set; }
        public string Info { get; private set; }

        public bool CancelTransaction { get; set; } = false;
    }
}
