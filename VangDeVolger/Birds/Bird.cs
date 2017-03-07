using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VangDeVolger.Blocks;

namespace VangDeVolger.Birds
{
    public abstract class Bird
    {
        public PictureBox Pb;

        protected int PlayerSpeed;

        /// <summary>
        /// Initialize Bird Class
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        protected Bird(Point position, int speed)
        {
            this.Pb = new PictureBox
            {
                Location = position,
                Size = new Size(Game.BirdSize, Game.BirdSize),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            PlayerSpeed = speed;
        }

        internal abstract void Move(ref List<Block> blocks, KeyEventArgs e);
    }
}