using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VangDeVolger
{
    internal class PlayerBird : Bird
    {
        private readonly Image _birdImage = VangDeVolger.Properties.Resources.vogel_groen;
        private readonly Image _birdImageActive = VangDeVolger.Properties.Resources.vogel_groen_active;

        public PlayerBird(Point position) : base(position)
        {
            this.Pb.Image = _birdImage;
        }
    }
}
