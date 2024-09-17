using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_3 : Form
    {
        private List<Student> students_scores;
        public Frm作業_3()
        {
            InitializeComponent();

            //hint
            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };
        }

        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?	
            textBox1.Text = students_scores.Count().ToString();

            // 找出 前面三個 的學員所有科目成績	

            // 找出 後面兩個 的學員所有科目成績					

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績						

            // 找出學員 'bbb' 的成績	                          

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	

            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |				
            // 數學不及格 ... 是誰 
            #endregion
        }

        private void button37_Click(object sender, EventArgs e)
        {
            //個人 sum, min, max, avg
            // 統計 每個學生個人成績 並排序

            var stu = from s in students_scores
                      select new
                      {
                          s.Name,
                          Sum = s.Math + s.Chi + s.Eng,
                          Min = Math.Min(Math.Min(s.Chi, s.Math), s.Eng),
                          Max = Math.Max(Math.Max(s.Chi, s.Math), s.Eng),
                          Avg = (s.Chi + s.Math + s.Eng) / 3
                      };
            var o = stu.OrderByDescending(p => p.Sum);
            this.dataGridView1.DataSource = o.ToList();


        }

        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            var stuAvg = from s in students_scores
                         select new
                         { s.Name, Avg = (s.Chi+ s.Math+ s.Eng) / 3 };

            var splScore = from s in stuAvg
                           group s by MyAvg(s.Avg) into g
                           select new { Name };
            this.dataGridView1.DataSource = splScore.ToList();
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
        }
        private string MyAvg(int s)
        {
            if (s < 70)
            {
                return "待加強";
            }
            else if (s < 90)
            {
                return "佳";
            }
            else
            {
                return "優良";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //NOTE subquery
            //var q = from o in this.dbContext.Orders
            //        group o by o.OrderDate.Year into g
            //        select new
            //        {
            //            Year = g.Key,
            //            YearCount = g.Count(),
            //            MonthGroup = (from o in g
            //                          group o by o.OrderDate.Month into g1
            //                          select new { Month = g1.Key, MonthCount = g1.Count(), Orders = g1 })
            //        };
        }
        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 找出 前面三個 的學員所有科目成績	
            var top3 = students_scores.Take(3).Select(p => new { Name = p.Name, Chinese = p.Chi, Math = p.Math, English = p.Eng });
            this.dataGridView1.DataSource = top3.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 找出 後面兩個 的學員所有科目成績
            var last2 = students_scores.Skip(students_scores.Count - 2).Select(p => new { Name = p.Name, Chinese = p.Chi, Math = p.Math, English = p.Eng });
            this.dataGridView1.DataSource = last2.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績
            var abc = from s in students_scores
                      where s.Name == "aaa" ||s.Name == "bbb" ||s.Name == "ccc"
                      select (new {s.Name , Chinese = s.Chi, English = s.Eng});

            this.dataGridView1.DataSource =abc.ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // 找出學員 'bbb' 的成績	
            var bbb = students_scores.Where(p => p.Name == "bbb").Select(p => new { p.Name, Chinese = p.Chi, English = p.Eng, Math = p.Math });
            this.dataGridView1.DataSource=bbb.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)
            var acdef = students_scores.Where(p => p.Name != "bbb").Select(p => new { p.Name, Chinese = p.Chi, English = p.Eng, Math = p.Math });
            this.dataGridView1.DataSource = acdef.ToList();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績
            //var abcCM = from p in students_scores
            //            where p.Name =="aaa" ||p.Name == "bbb"|| p.Name =="ccc"
            //            select(new {p.Name, Chinese = p.Chi, p.Math});

            //this.dataGridView1.DataSource == abcCM.ToList();        }
            var studntScore = students_scores.Where(p => p.Name == "aaa" || p.Name == "bbb" || p.Name == "ccc").Select(p => new { p.Name, Chinese = p.Chi, p.Math });
            this.dataGridView1.DataSource = studntScore.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // 數學不及格 ... 是誰
            var mathFail = students_scores.Where(p => p.Math < 60).Select(p => p.Name);
            foreach(var score in mathFail)
            {
                textBox2.Text = score.ToString();
            }    
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    group f by MyLength(f.Length) into g
                    select new
                    {
                        MyKey = g.Key,
                        MyGroup = g.Select(f => new
                        {
                            FileName = f.Name,
                            FileSize = f.Length,
                            Group = g.Key
                        }).OrderByDescending(f => f.FileSize)
                    };

            List<dynamic> groupFiles = new List<dynamic>();
            foreach(var group in q)
            {
                foreach(var item in group.MyGroup)
                {
                    groupFiles.Add(item);
                }
            }
            this.dataGridView1.DataSource = groupFiles.ToList();
        }
        private object MyLength(long n)
        {
            if (n > 5000)
            {
                return "大檔案";
            }
            else if (n > 1000 && n < 5000)
            {
                return "中檔案";
            }
            else return "小檔案";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    group f by f.CreationTime.Year into g
                    select new
                    {
                        MyKey = g.Key,
                        MyGroup = g.Select(f => new
                        {
                            FileName = f.Name,
                            FileYear = f.CreationTime.Year,
                            Group = g.Key
                        }).OrderByDescending(f => f.FileYear)
                    };
                    List<dynamic> groupFile = new List<dynamic>();
            foreach (var group in q) 
            { 
                 foreach(var item in group.MyGroup)
                {
                    groupFile.Add(item);
                }
            }
            this.dataGridView1.DataSource = q.ToList();
        }
    }
}
