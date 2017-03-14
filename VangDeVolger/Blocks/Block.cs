using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Blocks
{
    public abstract class Block
    {
        public PictureBox Pb;
        protected int X;
        protected int Y;
        protected Image Image;

        /// <summary>
        /// Initialize Block Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        protected Block(int x, int y, int scale)
        {
            X = x;
            Y = y;
            var position = new Point(x * scale, y * scale);
            Pb = new PictureBox
            {
                Size = new Size(scale, scale),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = position,
                Image = Image
            };
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public abstract bool Touch(Direction direction);
    }
}
