using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Blocks
{
    public abstract class Block
    {
        public const int BlockSize = 32;
        public PictureBox Pb;
        public int X;
        public int Y;

        public Block SiblingTop;
        public Block SiblingBottom;
        public Block SiblingLeft;
        public Block SiblingRight;

        /// <summary>
        /// Initialize Block Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected Block(int x, int y)
        {
            X = x;
            Y = y;
            var position = new Point(x * BlockSize, y * BlockSize);
            Pb = new PictureBox
            {
                Size = new Size(Game.BlockSize, Game.BlockSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = position
            };
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="directions"></param>
        /// <returns></returns>
        public abstract bool Touch(Game.Directions directions);
    }
}
