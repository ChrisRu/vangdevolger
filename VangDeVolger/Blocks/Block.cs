using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Blocks
{
    public abstract class Block
    {
        public PictureBox Pb;
        public Block SiblingTop;
        public Block SiblingBottom;
        public Block SiblingLeft;
        public Block SiblingRight;

        /// <summary>
        /// Initialize Block Class
        /// </summary>
        /// <param name="position"></param>
        protected Block(Point position)
        {
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
