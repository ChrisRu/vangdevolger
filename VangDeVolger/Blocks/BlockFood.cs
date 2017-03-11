using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace VangDeVolger.Blocks
{
    internal class BlockFood : Block
    {
        private readonly Image _image = Properties.Resources.redbull;
        private int _boostTime = 20;
        private readonly Timer _timer = new Timer();

        /// <summary>
        /// Initialize BlockEgg Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public BlockFood(int x, int y) : base(x, y)
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
            Game.Player.Speed = 5;
            
            Pb.Dispose();

            _timer.Tick += T_Tick;
            _timer.Interval = 1000;
            _timer.Start();

            Game.Blocks[Pb.Location.Y / 32, Pb.Location.X / 32] = null;

            new SoundPlayer(Properties.Resources.Heart).Play();

            return true;
        }

        /// <summary>
        /// On Timer tick -1 second of remaining speed boost time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_Tick(object sender, EventArgs e)
        {
            if (_boostTime > 0)
            {
                _boostTime -= 1;
            }
            else if (_boostTime <= 0)
            {
                Game.Player.Speed = 3;
                 
                _timer.Stop();
            }
        }
    }
}