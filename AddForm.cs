using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace myProojectSQL
{
    public partial class AddForm : Form
    {
        public static MainForm mainForm1;
        public AddForm(MainForm f1)
        {
            InitializeComponent();


            mainForm1 = f1;
            mainForm1.Enabled = false;
            
            FormClosed += AddForm_FormClosed;
        }

        private void AddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            mainForm1.Enabled = true;
        }

        private void buttonAddPerson_Click(object sender, EventArgs e)
        {
            var p = new DataBaseConnection.Person()
            {
                Name = textBoxName.Text,
                LastName = textBoxLastname.Text,
                Telephone = Convert.ToInt32(textBoxPhoneNumber.Text),
                Profession = textBoxProfession.Text
            };
            bool res = DataBaseConnection.MySQLcontext.Insert(p);
            if (res)
            {
                MessageBox.Show("Добавлен обЪект");
                mainForm1.UpdateTable();
                mainForm1.UpdateCount();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
