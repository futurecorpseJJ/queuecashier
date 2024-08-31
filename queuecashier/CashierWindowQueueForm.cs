using System;
using System.Collections;
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
    public partial class CashierWindowQueueForm : Form
    {
        
        public CashierWindowQueueForm()
        {
            InitializeComponent();

            Timer timer = new Timer();
            timer.Interval = (1 * 1000);
            timer.Tick += new EventHandler(Timer1_tick);
            timer.Start();
        }
        Boolean openForm = false;
        CustomerView customerView = new CustomerView();
        FormCollection AllForms = Application.OpenForms;
        Form OpenedForm = new Form(); 

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplayCashierQueue(CashierClass.CashierQueue);

        }
        public void DisplayCashierQueue(IEnumerable CashierList)
        {
           listCashierQueue.Items.Clear();
            foreach(Object obj in CashierList)
            {
               listCashierQueue.Items.Add(obj.ToString());
            }

        }

        private void Timer1_tick(object sender, EventArgs e)
        {
            btnRefresh.PerformClick();
        }
        public delegate void PassControl(object sender);
        public PassControl passControl;

        public void OpenCashier()
        {
            if (openForm==false)
            {
                CashierWindowQueueForm cashierWindow = new CashierWindowQueueForm();
                cashierWindow.Visible = true;
                openForm = true;
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            NextServing();
            //CashierClass.CashierQueue.Dequeue();
        }

        public void NextServing()
        {
            foreach (Form form in AllForms)
            {
                if (form.Name == "Customer View")
                {
                    OpenedForm = form;
                    openForm = true;
                }
            }
            if (openForm == true)
            {
                if (passControl != null)
                {
                    customerView.lblNowServing.Text = CashierClass.CashierQueue.Peek();
                    CashierClass.CashierQueue.Dequeue();
                    passControl(customerView.lblNowServing);
                }
            }
            else
            {
                customerView.ShowDialog();
                customerView.lblNowServing.Text = CashierClass.CashierQueue.Peek().ToString();
                CashierClass.CashierQueue.Dequeue();
            }
        }
    }
}
