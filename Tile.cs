using System;
using System.Collections.Generic;
using System.Linq;
namespace MajorProjectDesktop
{
    class Tile
    {
        private int[] _coords = new int[2];
        private int _cost;
        private int _distance;
        private Tile _parent;
        public int CostDistance => Cost + Distance;


        public int[] Coords { get => _coords; set => _coords = value; }
        public int Cost { get => _cost; set => _cost = value; }
        public int Distance { get => _distance; set => _distance = value; }
        public Tile Parent { get => _parent; set => _parent = value; }

        public Tile()
        {
            //this.Distance = (End[0] - Coords[0]) + (End[1] - Coords[1]);
        }

        public void SetDistance(int[] End)
        {
            this.Distance = (End[0] - Coords[0]) + (End[1] - Coords[1]);

        }
    }
}

