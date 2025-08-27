using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Services
{
    internal class MovementAnimation
    {
        public static PictureBox CreatePicture(Control parent, Image image, Size size, Point location)
        {
            PictureBox temp = null;
            parent.Invoke((MethodInvoker)(() =>
            {
                temp = new PictureBox
                {
                    Image = image,
                    Size = size,
                    Location = location,
                    BackColor = Color.Transparent,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                parent.Controls.Add(temp);
                temp.BringToFront();
            }));
            return temp;
        }
    }
}
