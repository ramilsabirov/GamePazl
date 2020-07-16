using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabaPoTexProgrammirovanii
{
    public partial class Form_Reyting : Form
    {
        GameDataContext gameDataContext = new GameDataContext("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LabaPoTexProgrammirovanii.GameDataContext;Integrated Security=True");
        List<Reyting> listReyting;
        string s;
        string currentTime;
        public Form_Reyting(string time)
        {
            InitializeComponent();
            listReyting = gameDataContext.Reytings.ToList();
            currentTime = time;
        }
        public Form_Reyting()
        {
            InitializeComponent();
            listReyting = gameDataContext.Reytings.ToList();
            currentTime = "Вы не играли";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < listReyting.Count; i++)
            {
                for (int j = 0; j < listReyting.Count; j++)
                {
                    if (listReyting[i].Time < listReyting[j].Time)
                    {
                        Reyting r = listReyting[i];
                        listReyting[i] = listReyting[j];
                        listReyting[j] = r;
                    }
                }
            }

            for (int i = 0; i < listReyting.Count() && i <5; i++)
            {
                s += string.Format("{0} ------- {1}\n\n",i+1, listReyting[i].TimeForString() );
            }

            label2.Text = s;
            label4.Text = string.Format("Ваш результат {0}", currentTime);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
