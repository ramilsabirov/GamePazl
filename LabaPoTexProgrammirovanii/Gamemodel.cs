using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabaPoTexProgrammirovanii
{
    public delegate void DelegateHelper();
    class Gamemodel
    {
        public string picture = "puzzle.bmp";

        public int nw = 4, nh = 4;
        public  Bitmap pics;
        public int cw, ch;
        public int[,] field;
        public int ex, ey;
        public Boolean showNumbers = false;
        public event DelegateHelper eventStopGame;



        private void NewGame()
        { // располагаем фишки в правильном порядке
            for (int j = 0; j < nh; j++)
                for (int i = 0; i < nw; i++)
                    field[i, j] = j * nw + i + 1;

            // последняя фишка - пустая
            field[nw - 1, nh - 1] = 0;
            ex = nw - 1; ey = nh - 1;
    
            this.Mixer();        // перемешиваем фишки
        }

                
        private void Mixer()
        {
            int d;    // положение (относительно пустой) перемещаемой
            // клетки: 0 - слева; 1 - справа; 2 - сверху; 3 - снизу.

            int x, y; // перемещаемая клетка

            // генератор случайных чисел
            Random rnd = new Random();

            for (int i = 0; i < nw * nh * 10; i++)
            // nw * nh * 10 - кол-во перестановок
            {
                x = ex;
                y = ey;

                d = rnd.Next(4);
                switch (d)
                {
                    case 0: if (x > 0) x--; break;
                    case 1: if (x < nw - 1) x++; break;
                    case 2: if (y > 0) y--; break;
                    case 3: if (y < nh - 1) y++; break;
                }
                // здесь определили фишку, которую
                // нужно переместить в пустую клетку
                field[ex, ey] = field[x, y];
                field[x, y] = 0;

                // запоминаем координаты пустой фишки
                ex = x; ey = y;
            }
        }

        private Boolean Finish()
        {
            // координаты клетки
            int i = 0;
            int j = 0;

            int c;       // число в клетке

            // фишки расположены правильно, если
            // числа в них образуют матрицу:
            //   1  2  3  4
            //   5  6  7  8
            //   9 10 11 12
            //  13 14 15
                
            for (c = 1; c < nw * nh; c++)
            {
                if (field[i, j] != c) return false;

                // к следующей клетке
                if (i < nw - 1) i++;
                else { i = 0; j++; }
            }
            return true;
        }

        public void MovePicture(int cx, int cy)
        {
            // проверим, возможен ли обмен
            if (!(((Math.Abs(cx - ex) == 1) && (cy - ey == 0)) ||
                ((Math.Abs(cy - ey) == 1) && (cx - ex == 0))))
                return;
            // обмен. переместим фишку из (x, y) в (ex, ey)
            field[ex, ey] = field[cx, cy];
            field[cx, cy] = 0;

            ex = cx; ey = cy;

            if (this.Finish())
            {
                field[nw - 1, nh - 1] = nh * nw;               
                eventStopGame();
            }
        }


        private void CreatePicture()
        {
            try
            {
                // загружаем файл картинки
                pics = new Bitmap(Image.FromFile(picture), 700,500);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Файл " + picture + " не найден.\n",
                    "Собери картинку",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);  
                return;
            }

            field = new int[nw, nh];

            // определяем высоту и ширину клетки (фишки)
            cw = (int)(pics.Width / nw);
            ch = (int)(pics.Height / nh);
        }


        public void StartGame()
        {
            CreatePicture();
            NewGame();
        }

        public void LoadSaveGame()
        {
            try
            {
                // загружаем файл картинки
                pics = new Bitmap(Image.FromFile(picture), 700, 500);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Файл " + picture + " не найден.\n",
                    "Собери картинку",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error); 
                return;
            }


            // определяем высоту и ширину клетки (фишки)
            cw = (int)(pics.Width / nw);
            ch = (int)(pics.Height / nh);
        }
    }
}
