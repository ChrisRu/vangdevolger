
namespace VangDeVolger
{
    using System.Collections.Generic;
    using System.Drawing;
    using VangDeVolger.Elements;

    public class Spot
    {
        public int Scale { get; set; }

        public Element Element { get; set; }

        public Dictionary<Direction, Spot> Neighbors { get; set; }

        // PathFinder Properties
        public int PathCost { get; set; }

        public Spot CameFromSpot { get; set; }

        /// <summary>
        /// Initialize new Spot Class
        /// </summary>
        /// <param name="element">Element to be placed in Spot</param>
        /// <param name="scale">Scale of Picturebox</param>
        public Spot(Element element, int scale)
        {
            this.Element = element;
            this.Scale = scale;
            this.Neighbors = new Dictionary<Direction, Spot>();

            if (element != null)
            {
                this.Element.Pb.Size = new Size(this.Scale, this.Scale);
                this.Element.Parent = this;
            }
        }
    }
}
