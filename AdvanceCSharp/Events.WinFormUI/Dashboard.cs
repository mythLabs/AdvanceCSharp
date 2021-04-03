using Events.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Events.WinFormUI
{
    public partial class Dashboard : Form
    {
        Customer customer = new Customer();
        public Dashboard()
        {
            InitializeComponent();

            LoadTestingData();

            MakeForm();
        }

        private void LoadTestingData()
        {
            customer.CustomerName = "Amit bisht";
            customer.CheckingAccount = new Account();
            customer.SavingsAccount = new Account();

            customer.CheckingAccount.AccountName = "Checking Account";
            customer.SavingsAccount.AccountName = "Savings Account";

            customer.CheckingAccount.AddDeposit("Initial Balance", 110.56M);
            customer.SavingsAccount.AddDeposit("Initial Balance", 150.86M);
        }

        private void recordTransactionsButton_Click(object sender, EventArgs e)
        {
            Transactions transactions = new Transactions(customer);
            transactions.Show();
        }

        private void errorMessage_Click(object sender, EventArgs e)
        {
            errorMessage.Visible = false;
        }
        void MakeForm()
        {
            customerText.Text = customer.CustomerName;
            checkingTransactions.DataSource = customer.CheckingAccount.Transactions;
            savingsTransactions.DataSource = customer.SavingsAccount.Transactions;
            checkingBalanceValue.Text = string.Format("{0:C2}", customer.CheckingAccount.Balance);
            savingsBalanceValue.Text = string.Format("{0:C2}", customer.SavingsAccount.Balance);

            customer.CheckingAccount.TransactionApproveEvent += CheckingAccount_TransactionApproveEvent;
            customer.SavingsAccount.TransactionApproveEvent += SavingsAccount_TransactionApproveEvent;
            customer.CheckingAccount.OverDraftEvent += CheckingAccount_OverDraftEvent;
        }

        private void CheckingAccount_OverDraftEvent(object sender, OverDraftEventArgs o)
        {
            errorMessage.Text = $"Overdraft protection transfer of { string.Format("{0:c2} ", o.AmountOverDrafted)}";
            o.CancelTransaction = denyOverdraft.Checked;
            errorMessage.Visible = true;
        }

        private void SavingsAccount_TransactionApproveEvent(object sender, string e)
        {
            savingsTransactions.DataSource = null;
            savingsTransactions.DataSource = customer.SavingsAccount.Transactions;
            savingsBalanceValue.Text = string.Format("{0:C2}", customer.SavingsAccount.Balance);
        }

        private void CheckingAccount_TransactionApproveEvent(object sender, string e)
        {
            checkingTransactions.DataSource = null;
            checkingTransactions.DataSource = customer.CheckingAccount.Transactions;
            checkingBalanceValue.Text = string.Format("{0:C2}", customer.CheckingAccount.Balance);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
