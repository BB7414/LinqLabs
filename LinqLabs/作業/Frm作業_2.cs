using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            this.productPhotoTableAdapter1.Fill(this.awDataSet1.ProductPhoto);

            var q = from n in awDataSet1.ProductPhoto
                    select n.ModifiedDate.Year;

            this.comboBox3.DataSource = q.Distinct().ToList();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.awDataSet1.ProductPhoto;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var q = from n in this.awDataSet1.ProductPhoto
                    where n.ModifiedDate > dateTimePicker1.Value && n.ModifiedDate < dateTimePicker2.Value
                    select n;

            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int year;
            int.TryParse(this.comboBox3.Text, out year);

            var q = from n in this.awDataSet1.ProductPhoto
                    where n.ModifiedDate.Year == year
                    select n;

            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var season = from n in this.awDataSet1.ProductPhoto
                         where Season(n.ModifiedDate.Month) == comboBox2.Text
                         select n;
            dataGridView1.DataSource = season.ToList();
        }

        private string Season(int n)
        {
            if (n == 1 || n == 2 || n == 3)
            {
                return "第一季";
            }
            else if (n == 3 || n == 4 || n == 5)
            {
                return "第二季";
            }
            else if (n == 6 || n == 7 || n == 8)
            {
                return "第三季";
            }
            else return "第四季";
        }
    }
    }

