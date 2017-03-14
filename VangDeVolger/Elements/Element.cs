using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Elements
{
    public abstract class Element
    {
        public int X;
        public int Y;
        public PictureBox Pb;

        protected Element(int x, int y, int scale)
        {
            X = x;
            Y = y;
            Pb = new PictureBox
            {
                Size = new Size(scale, scale),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(x * scale, y * scale)
            };
        }

        public abstract bool Move(Direction direction);
    }
}
