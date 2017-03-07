using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VangDeVolger.Blocks;

namespace VangDeVolger.Birds
{
    internal class EnemyBird : Bird
    {
        private readonly Image _image = VangDeVolger.Properties.Resources.vogel_rood;
        private readonly Image _imageActive = VangDeVolger.Properties.Resources.vogel_rood_active;

        /// <summary>
        /// Initialize EnemyBird Class
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public EnemyBird(Point position, int speed) : base(position, speed)
        {
            this.Pb.Image = _image;
        }

        internal override void Move(ref List<Block> blocks, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
