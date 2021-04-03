using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Library
{
    public class Account
    {
        public event EventHandler<string> TransactionApproveEvent;
        public event EventHandler<OverDraftEventArgs> OverDraftEvent;
        public string AccountName { get; set; }
        public decimal Balance { get; private set; }

        private List<string> _transactions = new List<string>();

        public IReadOnlyList<string> Transactions
        {
            get { return _transactions.AsReadOnly(); }
        }

        public bool AddDeposit(string depositName, decimal amount)
        {
            _transactions.Add($"Deposited { string.Format("{0:c2}", amount) } for { depositName }");
            Balance += amount;
            TransactionApproveEvent?.Invoke(this, depositName);
            return true;
        }

        public bool MakePayment(string paymentName, decimal amount, Account backupAccount = null)
        {
            if(Balance >= amount)
            {
                _transactions.Add($"Withdraw { string.Format("{0:c2}", amount) } for { paymentName }");
                Balance -= amount;
                TransactionApproveEvent?.Invoke(this, paymentName);
                return true;
            } else
            {
                if(backupAccount != null)
                {
                    if((backupAccount.Balance + Balance) >= amount)
                    {
                        decimal amountNeeded = amount - Balance;
                        OverDraftEventArgs args = new OverDraftEventArgs(amountNeeded, paymentName);
                        OverDraftEvent?.Invoke(this, args);

                        if(args.CancelTransaction)
                        {
                            return false;
                        }
                        bool overdraftSucceeded = backupAccount.MakePayment("Overdraft Protection", amountNeeded);

                        if(overdraftSucceeded == false)
                        {
                            return false;
                        }

                        AddDeposit("Overdraft Protection Deposit", amountNeeded);

                        _transactions.Add($"Withdraw { string.Format("{0:c2}", amount) } for { paymentName }");
                        Balance -= amount;
                        TransactionApproveEvent?.Invoke(this, paymentName);
                        
                        return true;
                    } else
                    {
                        return false;
                    }
                } else
                {
                    return false;
                }
            }
        }
    }
}
