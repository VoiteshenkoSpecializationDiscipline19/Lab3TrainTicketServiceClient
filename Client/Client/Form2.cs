using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Client
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ticket ticket = new Ticket();
            ticket.Id = int.Parse(textBox1.Text);
            ticket.From = textBox2.Text;
            ticket.To = textBox3.Text;
            ticket.When = $"{textBox4.Text}-{textBox5.Text}-{textBox6.Text}T{textBox7.Text}:{textBox8.Text}:00Z";
            ticket.When = "2000-12-01T09:43:00Z";
            ticket.Price = double.Parse(textBox10.Text);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create($@"https://railwaytickets.azurewebsites.net/api/RailwayTickets/{Form1.tokenPost}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(ticket);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            Form1.list.Add(ticket);

            Form1.dataGrid.DataSource = null;
            Form1.dataGrid.DataSource = Form1.list;
            Close();
        }
    }
}
