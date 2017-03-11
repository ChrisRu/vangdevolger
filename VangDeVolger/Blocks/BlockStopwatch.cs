using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using VangDeVolger.Birds;

namespace VangDeVolger.Blocks
{
    internal class BlockStopwatch : Block
    {
        private readonly Image _image = Properties.Resources.clock;
        private int _slowDownTime = 20;
        private readonly Timer _timer = new Timer();

        /// <summary>
        /// Initialize BlockStopwatch Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public BlockStopwatch(int x, int y) : base(x, y)
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
            foreach (var enemy in Game.Enemies)
            {
                enemy.Speed = 1;
            }

            Pb.Dispose();

            _timer.Tick += T_Tick;
            _timer.Interval = 1000;
            _timer.Start();

            Game.Blocks[Pb.Location.Y / 32, Pb.Location.X / 32] = null;
            new SoundPlayer(Properties.Resources.ClockTick).Play();
            return true;
        }

        /// <summary>
        /// On Timer tick -1 second of remaining slow boost time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_Tick(object sender, EventArgs e)
        {
            if (_slowDownTime > 0)
            {
                _slowDownTime -= 1;
            }
            else if (_slowDownTime <= 0)
            {
                //stop the boost
                foreach (var enemy in Game.Enemies)
                {
                    enemy.Speed = 2;
                }

                _timer.Stop();
            }
        }
    }
}