using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VangDeVolger.Blocks;

namespace VangDeVolger.Birds
{
    internal class EnemyBird : Bird
    {
        private readonly Image _imageLeft = Properties.Resources.bird_red_left;
        private readonly Image _imageRight = Properties.Resources.bird_red_right;

        /// <summary>
        /// Initialize EnemyBird Class
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public EnemyBird(Point position, int speed) : base(position, speed)
        {
            Pb.Image = _imageLeft;
        }

        internal override void Move(ref List<Block> blocks, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
