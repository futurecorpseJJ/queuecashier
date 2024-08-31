using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace queuecashier
{

    public partial class QueuingForm : Form
    {
        private CashierClass cashier = new CashierClass();
       
        public QueuingForm()
        {
            InitializeComponent();
            cashier = new CashierClass();
            CashierWindowQueueForm obj = new CashierWindowQueueForm();
            obj.Show();
        }

        private void btnCashier_Click(object sender, EventArgs e)
        {
            lblQueue.Text = cashier.CashierGeneratedNumber("P - ");
            CashierClass.getNumberInQueue = lblQueue.Text;

            CashierClass.CashierQueue.Enqueue(CashierClass.getNumberInQueue);

        }
    }
}
