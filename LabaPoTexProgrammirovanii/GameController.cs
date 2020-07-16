using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace LabaPoTexProgrammirovanii
{
    public partial class GameController : Form
    {

        Gamemodel gm;
        System.Drawing.Graphics g;
       
        GameDataContext gameDataContext = new GameDataContext("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LabaPoTexProgrammirovanii.GameDataContext;Integrated Security=True");
        string filepath = "C:\\файлы\\тпу\\9 семестр\\Тех  ПО\\лаба\\LabaPoTexProgrammirovanii\\LabaPoTexProgrammirovanii\\App_Data\\";
        int second;
        int minute;
        string time;
        int timeGame;
        bool gameStatus = false;

        public GameController()
        {
            InitializeComponent();
            gm = new Gamemodel();
            g = this.CreateGraphics();
            gm.eventStopGame += Gm_eventStopGame;
        }

        // отрисовывает поле
        private void DrawField()
        {
            // содержимое клеток
            if (gm.pics != null)
            {
                for (int i = 0; i < gm.nw; i++)
                    for (int j = 0; j < gm.nh; j++)
                    {
                        if (gm.field[i, j] != 0)
                            // выводим фишку с картинкой:
                            // ( ((field[i,j] - 1) % nw) * cw,
                            //   (int)((field[i,j] - 1) / nw) * ch ) -
                            // координаты левого верхнего угла
                            // области файла-источника картинки
                            g.DrawImage(gm.pics,
                                new Rectangle(i * gm.cw, j * gm.ch + menuStrip1.Height, gm.cw, gm.ch),
                                new Rectangle(
                                    ((gm.field[i, j] - 1) % gm.nw) * gm.cw,
                                    ((gm.field[i, j] - 1) / gm.nw) * gm.ch,
                                    gm.cw, gm.ch),
                                GraphicsUnit.Pixel);
                        else
                            // выводим пустую фишку
                            g.FillRectangle(SystemBrushes.ActiveCaption,
                                i * gm.cw, j * gm.ch + menuStrip1.Height, gm.cw, gm.ch);
                        // рисуем границу
                        g.DrawRectangle(Pens.Black,
                            i * gm.cw, j * gm.ch + menuStrip1.Height, gm.cw, gm.ch);

                        // номер фишки
                        if ((gm.showNumbers) && gm.field[i, j] != 0)
                            g.DrawString(Convert.ToString(gm.field[i, j]),
                                new Font("Tahoma", 10, FontStyle.Bold),
                                Brushes.Black, i * gm.cw + 5, j * gm.ch + 5 + menuStrip1.Height);

                    }
            }
        }

        // обработка события Paint

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawField();
        }


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // преобразуем координаты мыши в координаты клетки
            if (gameStatus)
            { 
            gm.MovePicture(e.X / gm.cw, (e.Y - menuStrip1.Height) / gm.ch);
            DrawField();
            }
        }

        private void выборРисункаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog file_dialog = new OpenFileDialog();

            file_dialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.PNG)|*.bmp;*.jpg;*.gif; *.png";

            file_dialog.Title = "Выберите картинку для новой игры...";

            if (file_dialog.ShowDialog() == DialogResult.OK)
            {
                gm.picture = file_dialog.FileName;
                StartGame();
                gameStatus = true;
            }
        }

        //сохранение игры
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData sv = new SaveData();
            sv.Nh = gm.nh;
            sv.Nw = gm.nw;
            sv.Ex = gm.ex;
            sv.Ey = gm.ey;
            sv.minute = minute;
            sv.second = second;
            sv.Picture = gm.picture;

            string json = JsonConvert.SerializeObject(gm.field);
            if (File.Exists(filepath + "save.json"))
            {
                File.Delete(filepath + "save.json");
            }
            File.AppendAllText(filepath + "save.json", json);

            gameDataContext.SaveDatas.Remove(gameDataContext.SaveDatas.FirstOrDefault());
            gameDataContext.SaveChanges();
            gameDataContext.SaveDatas.Add(sv);
            gameDataContext.SaveChanges();
        }



        private void загрузитьИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData sv = gameDataContext.SaveDatas.FirstOrDefault();
            gm.nh = sv.Nh;
            gm.nw = sv.Nw;
            gm.ex = sv.Ex;
            gm.ey = sv.Ey;
            second = sv.second;
            minute = sv.minute;
            gm.picture = sv.Picture;
            gm.field = JsonConvert.DeserializeObject<int[,]>(File.ReadAllText(filepath + "save.json"));
            gm.LoadSaveGame();
            DrawField();
            timer1.Start();
            label2.Visible = false;
            gameStatus = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (second < 59)
            {
                if (second >= 9)
                {
                    second++;
                    time = "0"+ minute.ToString() + ":" + (second).ToString();
                }
                else
                {
                    second++;
                    time = "0" + minute.ToString() + ":0" + (second).ToString();
                }

            }
            else
            {
                minute++;
                time = (minute).ToString() + ":0" + (second = 0).ToString();
            }
            label1.Text = time;
            timeGame++;
        }

        private void рейтингToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Reyting f = new Form_Reyting();
            f.Show();
        }


        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartGame();
            gameStatus = true;
        }


        private void Gm_eventStopGame()
        {
            
            timer1.Stop();
            Reyting r = new Reyting();
            r.Time = timeGame;
            gameDataContext.Reytings.Add(r);
            gameDataContext.SaveChanges();
            gameStatus = false;
            Form_Reyting f = new Form_Reyting(time);
            f.Show();

        }



        void StartGame()
        {
            label2.Visible = false;
            ValueStart();
            gm.StartGame();
            DrawField();
        }

        void ValueStart()
        {
            second = minute = timeGame = 0;
            time = "";
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (gameStatus)
            {
                Form_For_picture f = new Form_For_picture(gm.pics);
                 f.ShowDialog();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            новаяИграToolStripMenuItem_Click(sender, e);
        }
    }
}
