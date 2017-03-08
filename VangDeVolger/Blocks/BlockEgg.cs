using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VangDeVolger.Blocks
{
    internal class BlockEgg : Block
    {
        private List<Block> _blocks;
        private readonly Image _image = VangDeVolger.Properties.Resources.egg;
        private int _hatchTime = 20;
        public Label TimeLabel = new Label();
        private Timer _timer;


        public BlockEgg(Point position, ref List<Block> blocks) : base(position, ref blocks)
        {
            this.Pb.Image = _image;
            _blocks = blocks;

            TimeLabel.AutoSize = true;
            TimeLabel.Name = "TimeLabel";
            TimeLabel.Size = new Size(56, 18);
            TimeLabel.Text = _hatchTime.ToString();
            TimeLabel.BackColor = Color.Transparent;
            TimeLabel.ForeColor = Color.Black;
            TimeLabel.Location = Pb.Location;
            TimeLabel.Parent = Pb;

            //Graphics.DrawString(_hatchTime.ToString(), Pb.Font, new SolidBrush(Color.White), 0f, 0f);
            
            TimeLabel.Visible = true;


            _timer = new Timer();
            _timer.Tick += new EventHandler(t_Tick);
            _timer.Interval = 1000;
            _timer.Start();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            if (_hatchTime > 0)
            {
                _hatchTime -= 1;
                TimeLabel.Text = _hatchTime.ToString();
                TimeLabel.Invalidate();
            }
            else if (_hatchTime <= 0)
            {
                _blocks.Remove(this);
                _hatchTime = 20;
            }
        }

        internal override void Move(ref List<Block> blocks, Game.Directions direction)
        {
            throw new System.NotImplementedException();
        }
    }
}
