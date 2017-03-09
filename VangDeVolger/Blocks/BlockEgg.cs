using System;
using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Blocks
{
    internal class BlockEgg : Block
    {
        private readonly Image _image = Properties.Resources.egg;
        private int _hatchTime = 20;
        public Label TimeLabel;

        /// <summary>
        /// Initialize BlockEgg Class
        /// </summary>
        /// <param name="position"></param>
        public BlockEgg(Point position) : base(position)
        {
            Pb.Image = _image;
            TimeLabel = CreateLabel("TimeLabel", new Size(56, 18), _hatchTime.ToString());

            var timer = new Timer();
            timer.Tick += T_Tick;
            timer.Interval = 1000;
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
                TimeLabel.Text = _hatchTime.ToString();
                TimeLabel.Invalidate();
            }
            else if (_hatchTime <= 0) // het ei komt uit
            {
                var birdLocation = Pb.Location;

                Remove();

                // TODO: spawn een extra volger...

            }
        }

       
        /// <summary>
        /// Execute on collision with other object
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override bool Touch(Game.Directions direction)
        {
            Remove();
            return false;
        }

        public void Remove()
        {
            Game.Blocks.Remove(this);
            _hatchTime = 20;
            Pb.Dispose();
            TimeLabel.Dispose();
        }

        /// <summary>
        /// Creates a label on the position of the block itself
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private Label CreateLabel(string name, Size size, string text)
        {
            var label = new Label
            {
                AutoSize = true,
                Name = name,
                Size = size,
                Text = text,
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                Location = Pb.Location,
                Parent = Pb,
                Visible = true
            };

            return label;
        }
    }
}
