using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Blocks
{
    public abstract class Block
    {
        public PictureBox Pb;

        /// <summary>
        /// Initialize Block Class
        /// </summary>
        /// <param name="position"></param>
        protected Block(Point position)
        {
            this.Pb = new PictureBox
            {
                Size = new Size(30, 30),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = position
            };
        }
    }
}
