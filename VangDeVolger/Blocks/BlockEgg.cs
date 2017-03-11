using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace VangDeVolger.Blocks
{
    internal class BlockEgg : Block
    {
        private readonly Image _image = Properties.Resources.egg;
        private int _hatchTime = 20; // 205
        private readonly Timer _timer = new Timer();

        public EventHandler SpawnBird;

        /// <summary>
        /// Initialize BlockEgg Class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public BlockEgg(int x, int y) : base(x, y)
        {
            Pb.Image = _image;

            _timer.Tick += T_Tick;
            _timer.Interval = 100;
            _timer.Start();
        }
        
        /// <summary>
        /// On Timer tick check if hatched
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_Tick(object sender, EventArgs e)
        {
            if (_hatchTime > 0)
            {
                _hatchTime -= 1;
            }
            else if (_hatchTime <= 0) // het ei komt uit
            {
                _timer.Stop();
                SpawnBird(this, new EventArgs());
                Game.Blocks[Pb.Location.Y / 32, Pb.Location.X / 32] = null;
                Pb.Dispose();
            }
        }

       
        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool Touch(Game.Directions direction)
        {
            _timer.Stop();
            Game.Blocks[Pb.Location.Y / 32, Pb.Location.X / 32] = null;
            Pb.Dispose();

            new SoundPlayer(Properties.Resources.EggCrack).Play();
            return true;
        }
    }
}
