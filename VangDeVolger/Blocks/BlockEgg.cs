using System;
using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Blocks
{
    internal class BlockEgg : Block
    {
        private readonly Image _image = VangDeVolger.Properties.Resources.egg;
        private int _hatchTime = 20;
        public Label TimeLabel = new Label();
        private Timer _timer;

        /// <summary>
        /// Initialize BlockEgg Class
        /// </summary>
        /// <param name="position"></param>
        public BlockEgg(Point position) : base(position)
        {
            this.Pb.Image = _image;

            TimeLabel.AutoSize = true;
            TimeLabel.Name = "TimeLabel";
            TimeLabel.Size = new Size(56, 18);
            TimeLabel.Text = _hatchTime.ToString();
            TimeLabel.BackColor = Color.Transparent;
            TimeLabel.ForeColor = Color.Black;
            TimeLabel.Location = Pb.Location;
            TimeLabel.Parent = Pb;

            // TODO: Make label background transparent
            //Graphics.DrawString(_hatchTime.ToString(), Pb.Font, new SolidBrush(Color.White), 0f, 0f);
            
            TimeLabel.Visible = true;

            _timer = new Timer();
            _timer.Tick += T_Tick;
            _timer.Interval = 1000;
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
                TimeLabel.Text = _hatchTime.ToString();
                TimeLabel.Invalidate();
            }
            else if (_hatchTime <= 0) // het ei komt uit
            {
                //verwijder het ei eerst
                Game.Blocks.Remove(this);
                _hatchTime = 20;
                this.Pb.Dispose();
                TimeLabel.Dispose();
                // spawn een extra volger...
            }
        }

       
        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool Touch(Game.Directions direction)
        {
            // TODO: Logic for egg grab here 
            // hatch = extra volger als straf
            // touch = destroy

            Game.Blocks.Remove(this);
            _hatchTime = 20;
            this.Pb.Dispose();
            TimeLabel.Dispose();

            return false;
        }
    }
}
