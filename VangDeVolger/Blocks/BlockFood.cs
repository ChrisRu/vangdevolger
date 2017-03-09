using System;
using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Blocks
{
    internal class BlockFood : Block
    {
        private readonly Image _image = Properties.Resources.heart;
        private int _boostTime = 20;
        private readonly Timer _timer = new Timer();

        /// <summary>
        /// Initialize BlockEgg Class
        /// </summary>
        /// <param name="position"></param>
        public BlockFood(Point position) : base(position)
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
            // start the boost

            _timer.Tick += T_Tick;
            _timer.Interval = 1000;
            _timer.Start();

            Dispose();
            return true;
        }

        /// <summary>
        /// On Timer tick -1 second of remaining speed boost time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_Tick(object sender, EventArgs e)
        {
            if(_boostTime > 0)
            {
                _boostTime -= 1;
            }
            else if(_boostTime <= 0)
            {
                //stop the boost

                Dispose();
                _timer.Stop();
            }
        }

        /// <summary>
        /// Dispose Class and remove from view and blocks list
        /// </summary>
        public void Dispose()
        {
            Game.Blocks.Remove(this);
            Pb.Dispose();
        }
    }
}