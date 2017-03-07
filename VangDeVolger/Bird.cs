using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger
{
    public abstract class Bird
    {
        protected int PlayerSpeed;
        public PictureBox Pb;

        protected Bird(Point position, int speed)
        {
            this.Pb = new PictureBox
            {
                Location = position,
                Size = new Size(30, 30),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            PlayerSpeed = speed;
        }

        internal abstract void Move(KeyEventArgs e);
    }
}