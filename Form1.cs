using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace myProojectSQL
{
    
    public partial class MainForm : Form
    {
        private List<DataBaseConnection.Person> peopleList { get; set; } 
        public MainForm()
        {
            InitializeComponent();
            btn_openData.MouseHover += Btn_openData_MouseHover;
            btn_openData.MouseLeave += Btn_openData_MouseLeave;


            var conn = DataBaseConnection.MySQLcontext.SqlConnection();
 

            try
            {
                conn.Open();
                peopleList = new List<DataBaseConnection.Person>();
                table.AutoGenerateColumns = true;
                
                table.Enabled = false;
                load_items.DataSource = peopleList;
                UpdateTable();
                conn.Close();
                UpdateCount();
            }
            catch (Exception e)
            {
                Enabled = false;
                MessageBox.Show("Отсуствует соеденение с бд");
            }
            
            
        }

        public void UpdateTable()
        {
            peopleList.Clear();
            var list = DataBaseConnection.MySQLcontext.GetPeople();
            if (list != null && list.Count() > 0)
            {
                peopleList.AddRange(list);
                load_items.ResetBindings(false);
            }
        }

        private void Btn_openData_MouseLeave(object sender, EventArgs e)
        {
            btn_openData.BackColor = Color.Transparent;
            btn_openData.ForeColor = Color.Black;
        }

        private void Btn_openData_MouseHover(object sender, EventArgs e)
        {
            btn_openData.BackColor = Color.Blue;
            btn_openData.ForeColor = Color.White;
        }

        private void btn_openData_Click(object sender, EventArgs e)
        {
            AddForm add = new AddForm(this);
           
            add.Show();
           
        }

        public void UpdateCount()
        {
            var count = DataBaseConnection.MySQLcontext.GetCountItems();
            count_items.Text = "Количество " +
                "элементов " + Convert.ToString(count);
        } 


    }
}
