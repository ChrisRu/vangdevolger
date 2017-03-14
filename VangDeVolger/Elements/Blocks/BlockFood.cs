using System;
using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Elements.Blocks
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
        /// <param name="scale"></param>
        public BlockFood(int x, int y, int scale) : base(x, y, scale)
        {
            Pb.Image = _image;
        }

        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool Touch(Direction direction)
        {
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
                _timer.Stop();
            }
        }
    }
}