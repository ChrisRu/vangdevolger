using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VangDeVolger
{
    public abstract class Block
    {
        public PictureBox Pb;

        private readonly Image _image = VangDeVolger.Properties.Resources.rock;

        /// <summary>
        /// Initialize Block Class
        /// </summary>
        /// <param name="position"></param>
        protected Block(Point position)
        {
            this.Pb = new PictureBox
            {
                Image = _image,
                Size = new Size(30, 30),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = position
            };
        }
    }
}
