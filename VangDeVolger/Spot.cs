using System.Collections.Generic;
using System.Drawing;
using VangDeVolger.Elements;

namespace VangDeVolger
{
    public class Spot
    {
        public int Scale { get; set; }
        public Element Element { get; set; }
        public Dictionary<Direction, Spot> Neighbors { get; set; }

        // PathFinder Properties
        public int PathCost { get; set; }
        public Spot CameFromSpot { get; set; }

        /// <summary>
        /// Initialize Spot Class
        /// </summary>
        /// <param name="element">Element in Spot</param>
        /// <param name="scale">Scale of Picturebox</param>
        public Spot(Element element, int scale)
        {
            Element = element;
            if (Element != null)
            {
                Element.Parent = this;
                Element.Pb.Size = new Size(scale, scale);
            }
            Scale = scale;
            Neighbors = new Dictionary<Direction, Spot>();
        }
    }
}
