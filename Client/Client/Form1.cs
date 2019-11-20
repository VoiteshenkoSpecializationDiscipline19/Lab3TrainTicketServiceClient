using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using restClient_0;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Client
{
    partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            list = new List<Ticket>(50);
            dataGrid = grid;
            grid.Enabled = true;

        }
        public static string tokenPut = "1adb520e-5823-4058-8406-23899505a610";
        public static string tokenPost = "1adb520e-5823-4058-8406-23899505a610";
        public static List<Ticket> list;
        public static DataGridView dataGrid;
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
            grid.Enabled = true;
            grid.Columns[0].ReadOnly = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {


            Ticket ticket = new Ticket();
            var temp = grid[e.RowIndex, 0].Value;
            ticket.Id = (int)grid[0, e.RowIndex].Value;
            ticket.From = (string)grid[1, e.RowIndex].Value;
            ticket.To = (string)grid[2, e.RowIndex].Value;
            ticket.When = (string)grid[3, e.RowIndex].Value;
            ticket.Price = (double)grid[4, e.RowIndex].Value;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($@"https://railwaytickets.azurewebsites.net/api/RailwayTickets/{ticket.Id}/{tokenPut}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var counter = int.Parse(textBox1.Text);
            var table = (from t in list where t.Id == counter select t);
            var temp = table.ToList();
            list.Remove(temp[0]);
            grid.DataSource = null;
            grid.DataSource = list;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($@"https://railwaytickets.azurewebsites.net/api/RailwayTickets/{counter}");
            httpWebRequest.Method = "DELETE";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        private static readonly HttpClient client = new HttpClient();

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button5_Click(object sender, EventArgs e)
        {
       
            DateTime date1 = DateTime.Now.Date.AddDays(-1);
            DateTime date2 = DateTime.Now.Date.AddDays(2);
            Dates dates = new Dates();
            dates.DateFrom = $"{date1.Date.Day.ToString()}-{date1.Month.ToString()}-{date1.Year.ToString()}";
            dates.DateTo = $"{date2.Date.Day.ToString()}-{date2.Month.ToString()}-{date2.Year.ToString()}";

            var str = $@"http://cryptic-beach-05943.herokuapp.com/token/RailwayTickets/PostTicket";
            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                var uri = new Uri(str);

                var jsonRequest = JsonConvert.SerializeObject(dates,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    });
                response = await client.PostAsync(uri,
                    new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var paymentResponse = JsonConvert.DeserializeObject<PaymentResponce>(jsonResponse);

            if (paymentResponse.StatusCode == 404)
            {
                return;
            }
            tokenPost = paymentResponse.token;
        }

        
        private async void button6_Click(object sender, EventArgs e)
        {
            DateTime date1 = DateTime.Now.Date.AddDays(-1); 
            DateTime date2 = DateTime.Now.Date.AddDays(2);
            Dates dates = new Dates();
            dates.DateFrom = $"{date1.Date.Day.ToString()}-{date1.Month.ToString()}-{date1.Year.ToString()}";
            dates.DateTo = $"{date2.Date.Day.ToString()}-{date2.Month.ToString()}-{date2.Year.ToString()}"; HttpResponseMessage response = null;
            var str = $@"http://cryptic-beach-05943.herokuapp.com/token/RailwayTickets/PutTicket";
            using (var client = new HttpClient())
            {
                var uri = new Uri(str);

                var jsonRequest = JsonConvert.SerializeObject(dates,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    });
                response = await client.PostAsync(uri,
                    new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var paymentResponse = JsonConvert.DeserializeObject<PaymentResponce>(jsonResponse);

            if (paymentResponse.StatusCode == 404)
            {
                return;
            }
            tokenPut = paymentResponse.token;
        }
    }
}
