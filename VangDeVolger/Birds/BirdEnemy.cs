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
        public EnemyBird(Point position) : base(position)
        {
            Pb.Image = _imageLeft;
        }

        /// <summary>
        /// Move Bird with controls
        /// </summary>
        /// <param name="e"></param>
        public override void Move(KeyEventArgs e)
        {
            // TODO: Add AI

            ChangeDirection(_imageLeft, _imageRight);
        }
    }
}
