using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace GetData
{
    public partial class 事前課題１ : Form
    {
        DataTable table=new DataTable();
        List<DataRow> DataList=new List<DataRow>();
        public 事前課題１()
        {
            InitializeComponent();
        }
        public void InitTable()
        {
            table = new DataTable("MarketDataTable");
            table.Columns.Add("日付", typeof(string));
            table.Columns.Add("終値", typeof(string));
            dataGridView1.DataSource = table;
        }
        private void Form1_Load(object sender, EventArgs e)
        {        
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  Datetime dt1=dateTimePicker1.Value.Year.ToString("yyyy/MM/dd");
           
            String StartDate = "";
            String EndDate = "";
            string time = "1965.1.5 1:0:0";
            DateTime bottom = new DateTime();
            bottom = DateTime.Parse(time);

            StartDate=(-157453200 + 24*60*60*Convert.ToInt32(dateTimePicker1.Value.Subtract(bottom).TotalDays)).ToString();
            EndDate= (-157453200 + 24*60*60*Convert.ToInt32(dateTimePicker2.Value.Subtract(bottom).TotalDays)).ToString();

            InitTable();

            string result = "";
            HtmlWeb web = new HtmlWeb();//*
            var doc = web.Load("https://finance.yahoo.com/quote/%5EN225/history?period1="+StartDate+"&period2="+EndDate+"&interval=1d&filter=history&frequency=1d");
            var Date_Nodes = doc.DocumentNode.SelectNodes("//section/div[2]//table/tbody//tr//td[1]");          
            var Close_Nodes = doc.DocumentNode.SelectNodes("//section/div[2]//table/tbody//tr//td[5]");

            if (Date_Nodes == null)
            {
                label3.Text = "NO DATA AVAILABLE";
            }
            else if (Date_Nodes != null)
            {
                
                foreach (HtmlNode item in Date_Nodes)
                {
                    DataRow dr = table.NewRow();
                    dr[1] = "";
                    dr[0] = item.InnerText;
                    DataList.Add(dr);
                    /*result = item.InnerText;
                    // table.Rows.Add(new Object[] { 1, "Smith" });
                    DataRow dr = table.NewRow();
                    dr[0] = result;
                    
                    table.Rows.Add(dr);*/
                }
                int i = 0;
                foreach (HtmlNode item in Close_Nodes)
                {
                    DataRow dr2 = DataList[i];
                    dr2[1] = item.InnerText;
                    DataList[i] = dr2;
                    i++;
                }
                DataList.Reverse();

                foreach (DataRow dr in DataList)
                {
                    table.Rows.Add(dr);
                }
                dataGridView1.DataSource = table;
                dataGridView1.AllowUserToAddRows = false;
            }
            else
            {
                // Every. Single. Time.
            }
            table = new DataTable();
            DataList = new List<DataRow>();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}


            
    
