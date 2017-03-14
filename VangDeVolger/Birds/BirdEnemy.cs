using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VangDeVolger.PathFinding;

namespace VangDeVolger.Birds
{
    internal class EnemyBird : Bird
    {
        /// <summary>
        /// Initialize EnemyBird Class
        /// </summary>
        /// <param name="position"></param>
        public EnemyBird(Point position) : base(position)
        {
            ImageLeft = Properties.Resources.bird_red_left;
            ImageRight = Properties.Resources.bird_red_right;
        }

        /// <summary>
        /// Move Bird with controls
        /// </summary>
        /// <param name="e"></param>
        public override void Move(KeyEventArgs e)
        {

        }
    }
}
