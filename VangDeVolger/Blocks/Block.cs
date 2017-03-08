using System.Collections.Generic;
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
            this.Pb = new PictureBox
            {
                Size = new Size(Game.BlockSize, Game.BlockSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = position
            };
        }

        internal abstract void Move(ref List<Block> blocks, Game.Directions direction);
    }
}
