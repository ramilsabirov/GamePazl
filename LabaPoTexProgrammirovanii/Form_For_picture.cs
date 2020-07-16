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
    public partial class Form_For_picture : Form
    {
        Bitmap bitmap;
        public Form_For_picture(Bitmap picture)
        {
            InitializeComponent();
            bitmap = picture;
        }

        private void Form_For_picture_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, new PointF(0, 0));
        }
    }
}
