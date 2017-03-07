using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger
{
    public abstract class Bird
    {
        public PictureBox Pb;

        protected Bird(Point position)
        {
            this.Pb = new PictureBox
            {
                Location = position,
                Size = new Size(30, 30),
                SizeMode = PictureBoxSizeMode.Zoom
            };
        }
    }
}