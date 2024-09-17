using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);


            var q = from n in nwDataSet1.Orders
                    select n.OrderDate.Year;

            this.comboBox1.DataSource = q.Distinct().ToList();

        }
        int page = -1;
        int countPage = 10;

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()

            int.TryParse(this.textBox1.Text, out countPage);

            page += 1;
            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(countPage * page).Take(countPage).ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from p in files
                    where p.Extension == ".log"
                    select p;

            this.dataGridView1.DataSource = q.ToList();

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.nwDataSet1.Orders.ToList();

            var o =from d in nwDataSet1.Order_Details
                   join p in nwDataSet1.Products
                   on d.ProductID equals p.ProductID
                   select p;
            this.dataGridView2.DataSource = o.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int year;
            int.TryParse(this.comboBox1.Text, out year);

            var q = from n in this.nwDataSet1.Orders
                    where n.OrderDate.Year == year
                    select n;

            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;

            var o = from d in nwDataSet1.Order_Details
                    join p in nwDataSet1.Orders
                    on d.OrderID equals p.OrderID
                    where p.OrderDate.Year.ToString() == comboBox1.Text
                    select d;

            this.dataGridView2.DataSource= o.ToList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = 1997;


            var q = from n in this.nwDataSet1.Orders
                    where n.OrderDate.Year == year
                    select n;

            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from p in files
                    where p.CreationTime.Year == 2024
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from p in files
                    where p.Length > 30000
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int.TryParse(this.textBox1.Text, out countPage);

            page -= 1;
            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(countPage * page).Take(countPage).ToList();
        }
    }
}
