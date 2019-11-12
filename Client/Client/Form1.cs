using restClient_0;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static List<Ticket> list = new List<Ticket>(50);
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RESTClient rClient = new RESTClient();

            rClient.endPoint = @"https://railwaytickets.azurewebsites.net/api/RailwayTickets";

            string strJSON = string.Empty;

            list = rClient.makeRequest();
            var bindingList = new BindingList<Ticket>(list);
            var source = new BindingSource(bindingList, null);
            grid.DataSource = source;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            grid.Enabled = true;
        }

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var table = (from t in list where t.Id == 1 select t);
            var temp = table.ToList();
            list.Remove(temp[0]);
            grid.DataSource = null;
            grid.DataSource = list;
        }
    }
}
