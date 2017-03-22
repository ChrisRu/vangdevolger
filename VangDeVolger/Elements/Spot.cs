using System.Collections.Generic;

namespace VangDeVolger.Elements
{
    public class Spot
    {
        public Element Element { get; set; }
        public Dictionary<Direction, Spot> Neighbors { get; set; }

        public Spot(Element element)
        {
            Element = element;
            Neighbors = new Dictionary<Direction, Spot>();
        }
    }
}
