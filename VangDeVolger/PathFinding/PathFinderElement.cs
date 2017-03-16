using System.Collections.Generic;
using VangDeVolger.Elements;

namespace VangDeVolger.PathFinding
{
    internal class PathFinderElement
    {
        public Element Element;
        public PathFinderElement CameFrom = null;
        public List<PathFinderElement> SiblingBlocks;

        public int X;
        public int Y;

        public int F;
        public int G;
        public int H;

        public PathFinderElement(Element element, int x, int y)
        {
            Element = element;
            X = x;
            Y = y;
            F = 0;
            G = 0;
            H = 0;
        }
    }
}
