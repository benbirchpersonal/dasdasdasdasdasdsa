using System;
using System.Collections.Generic;
using System.Linq;
namespace MajorProjectDesktop
{
    class aStar
    {
        private int[] _startCoords = new int[2];
        private int[] _end = new int[2];
        private List<Tile> _activeTiles = new List<Tile>();
        private List<Tile> _visitedTiles = new List<Tile>();
        private Cell[,] _map;
        private Maze _mazeSolved;


        public int[] StartCoords { get => _startCoords; set => _startCoords = value; }
        public int[] End { get => _end; set => _end = value; }
        internal List<Tile> ActiveTiles { get => _activeTiles; set => _activeTiles = value; }
        internal List<Tile> VisitedTiles { get => _visitedTiles; set => _visitedTiles = value; }
        internal Cell[,] Map { get => _map; set => _map = value; }
        internal Maze MazeSolved { get => _mazeSolved; set => _mazeSolved = value; }

        public aStar(int[] inputStart, int x, int y, Maze inputMaze)
        {
            StartCoords = inputStart;
            End[0] = x ; End[1] = y ;
            MazeSolved = inputMaze;
            //Map = MazeSolved.CellList;
        }

        public void Find()
        {
            MazeSolved.Render(End[0], End[1]); //MAZE IS BEING PASSED THROUGH CORRECTLY
                                               //Map = CellList;
            Tile Start = new Tile();
            Start.Coords = StartCoords;
            //start.SetDistance(End);

            Tile Finish = new Tile();
            Finish.Coords = End;

            Start.SetDistance(End);

            List<Tile> ActiveTiles = new List<Tile>();
            List<Tile> VisitedTiles = new List<Tile>();
            ActiveTiles.Add(Start);
            VisitedTiles.Add(Start);
            int cnt = 0;
            while (ActiveTiles.Any())
            {

                Tile CheckTile = ActiveTiles.OrderBy(x => x.CostDistance).First();
                //Console.WriteLine($"x:{CheckTile.Coords[0]},y:{CheckTile.Coords[1]}");
                    MazeSolved.CellList[CheckTile.Coords[0], CheckTile.Coords[1]].Path = true;
                cnt++;
                    MazeSolved.Render(End[0], End[1]);

                foreach(var cur in ActiveTiles)
                {
                    if(cur.Coords == Finish.Coords)
                    {
                        //Found the destination and can be sure its the most low cost option because of OrderBy above
                        Tile tile = CheckTile;
                        Console.WriteLine("Retracing steps backwards...");

                        //Retracing Steps
                        while (true)
                        {
                            //Change output for this coordinate
                            //Console.WriteLine($"{tile.Coords[0]} : {tile.Coords[1]}");

                            //System.Threading.Thread.Sleep(100);
                            MazeSolved.CellList[tile.Coords[0], tile.Coords[1]].Path = true;
                            tile = tile.Parent;
                            if (tile == null)
                            {
                                //You've retraced back to the start
                                Console.WriteLine("map looks like:");
                                MazeSolved.Render(End[0], End[1]);
                                Console.WriteLine("Done!");
                                return;
                            }
                        }
                    }
                }






              //

                List<Tile> WalkableTiles = GetWalkableTiles(CheckTile, Finish);
                //List<Char> WalkableTiles = Map[StartCoords[0], StartCoords[1]].neighbours(Map);


                foreach (Tile WalkableTile in WalkableTiles)
                {
                    if (ActiveTiles.Any(x => x.Coords == WalkableTile.Coords)) //We've already visited this tile so no need to visit it again
                    {
                        continue;
                    }

                    if (ActiveTiles.Any(x => x.Coords == WalkableTile.Coords)) //Already in the active list, but thats fine as this new tile may have a better value
                    {
                        Tile ExistingTile = ActiveTiles.First(x => x.Coords == WalkableTile.Coords);
                        if (ExistingTile.CostDistance > WalkableTile.CostDistance)
                        {
                            //ActiveTiles.Remove(ExistingTile);
                            ActiveTiles.Add(WalkableTile);
                        }
                    }
                    else
                    {
                        ActiveTiles.Add(WalkableTile); //Never seen this tile before so add it to the list
                        //VisitedTiles.Add(CheckTile);
                    }
                }
            }
            Console.WriteLine("No path found");
        }

        List<Tile> GetWalkableTiles(Tile CurrentTile, Tile EndTile)
        {
            List<Tile> PossibleTiles = new List<Tile>();
            PossibleTiles.Clear();
            List<Char> FoundTiles = new List<Char>();

            FoundTiles = MazeSolved.CellList[CurrentTile.Coords[0],CurrentTile.Coords[1]].neighboursWalls(MazeSolved.CellList, End);



            if (FoundTiles.Contains('N'))
            {
                Tile inputTile = new Tile();
                inputTile.Coords[0] = CurrentTile.Coords[0];
                inputTile.Coords[1] = CurrentTile.Coords[1] - 1;
                inputTile.Parent = CurrentTile;
                inputTile.Cost = CurrentTile.Cost + 1;
                PossibleTiles.Add(inputTile);

            }

            if (FoundTiles.Contains('S'))
            {
                Tile inputTile = new Tile();
                inputTile.Coords[0] = CurrentTile.Coords[0];
                inputTile.Coords[1] = CurrentTile.Coords[1] + 1;
                inputTile.Parent = CurrentTile;
                inputTile.Cost = CurrentTile.Cost + 1;
                PossibleTiles.Add(inputTile);


            }

            if (FoundTiles.Contains('W'))
            {
                Tile inputTile = new Tile();
                inputTile.Coords[0] = CurrentTile.Coords[0] - 1;
                inputTile.Coords[1] = CurrentTile.Coords[1];
                inputTile.Parent = CurrentTile;
                inputTile.Cost = CurrentTile.Cost + 1;
                PossibleTiles.Add(inputTile);

            }

            if (FoundTiles.Contains('E'))
            {
                Tile inputTile = new Tile();
                inputTile.Coords[0] = CurrentTile.Coords[0] + 1;
                inputTile.Coords[1] = CurrentTile.Coords[1];
                inputTile.Parent = CurrentTile;
                inputTile.Cost = CurrentTile.Cost + 1;
                PossibleTiles.Add(inputTile);

            }

            PossibleTiles.ForEach(tile => tile.SetDistance(EndTile.Coords));

            //PossibleTiles.ForEach(tile);

            //var maxX = Map.First
            //var maxY =

            return PossibleTiles
                .Where(tile => tile.Coords[0] >= 0 && tile.Coords[0] < End[0])
                .Where(tile => tile.Coords[1] >= 0 && tile.Coords[1] < End[1])
                .ToList();
        }

    }
}

/*private int[] start = new int[2];
//private int[] coords = new int[2];
private int[] end = new int[2];



public aStar(int[] inputStart, int y, int x)
{
	start = inputStart;
	end[0] = x; end[1] = y;
	//coords = inputStart;

}

public int[] Start { get => start; set => start = value; }
//public int[] Coords { get => coords; set => coords = value; }
public int[] End { get => end; set => end = value; }

public void findSolution(Cell[,] CellList, int x, int y)
{
	//CellList[coords[0], coords[1]].Walls[0] == true;

	if (CellList[x, y].Walls[0] = true && CellList[x, y].Walls[1] == true)
	{
		if(pythag(CellList[x + 1, y], x, y) > pythag(CellList[x, y], x, y)) //If moving right is closer to end than moving down...
		{
			findSolution(CellList, x + 1, y);

		}
		else
		{
			findSolution(CellList, x, y + 1);
		}
	}
	else if (CellList[x, y].Walls[0] = true && CellList[x, y].Walls[1] == false) //If right is
	{
		findSolution(CellList, x + 1, y);

	}
	else if (CellList[x, y].Walls[0] = false && CellList[x, y].Walls[1] == true)
	{
		findSolution(CellList, x, y + 1);
	}
	//else
}

public double pythag(Cell CurrentCell, int x, int y)
{
	double hor = end[0] - x;
	double vert = end[1] - y;
	return Math.Sqrt((hor * hor) + (vert * vert));
} */

