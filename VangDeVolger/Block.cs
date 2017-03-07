using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VangDeVolger
{
    internal abstract class Block
    {
        public PictureBox Pb;
        private readonly Image _image = VangDeVolger.Properties.Resources.rock;

        protected Block(Point position)
        {
            this.Pb = new PictureBox
            {
                Image = _image,
                Size = new Size(30, 30),
                Location = position
            };
        }
    }
}
