using System;
using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Blocks
{
    internal class BlockEgg : Block
    {
        private readonly Image _image = Properties.Resources.egg;
        private int _hatchTime = 215;

        public EventHandler SpawnBird;

        /// <summary>
        /// Initialize BlockEgg Class
        /// </summary>
        /// <param name="position"></param>
        public BlockEgg(Point position) : base(position)
        {
            Pb.Image = _image;

            var timer = new Timer();
            timer.Tick += T_Tick;
            timer.Interval = 100;
            timer.Start();
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
                var birdLocation = Pb.Location;

                Dispose();

                SpawnBird(this, new EventArgs());
            }
        }

       
        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool Touch(Game.Directions direction)
        {
            Dispose();
            return true;
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
