using System;
using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Blocks
{
    internal class BlockStopwatch : Block
    {
        private readonly Image _image = Properties.Resources.clock;
        private int _slowtTime = 20;
        private readonly Timer _timer = new Timer();

        /// <summary>
        /// Initialize BlockStopwatch Class
        /// </summary>
        /// <param name="position"></param>
        public BlockStopwatch(Point position) : base(position)
        {
            Pb.Image = _image;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool Touch(Game.Directions direction)
        {
            // start the slowdown
            Game.EnemySpeed = 1;
            Pb.Dispose();

            _timer.Tick += T_Tick;
            _timer.Interval = 1000;
            _timer.Start();

            Game.Blocks.Remove(this);
            return true;
        }

        /// <summary>
        /// On Timer tick -1 second of remaining slow boost time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_Tick(object sender, EventArgs e)
        {
            if (_slowtTime > 0)
            {
                _slowtTime -= 1;
            }
            else if (_slowtTime <= 0)
            {
                //stop the boost
                Game.EnemySpeed = 2;
                _timer.Stop();
            }
        }
    }
}