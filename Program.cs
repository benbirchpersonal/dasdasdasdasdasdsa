using System;
using System.Diagnostics;

namespace MajorProjectDesktop // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Welcome to the maze\nPlease enter the height of the maze desired: (max {0})", Console.LargestWindowHeight);
            int des_height = int.Parse(Console.ReadLine());
            Console.Write("Please enter the width of the maze desired: (max {0})", Console.LargestWindowWidth);
            int des_width = int.Parse(Console.ReadLine());
            Maze maze1 = new Maze(des_height, des_width);
            //Stack stackDisplay = new Stack(des_height, des_width);

            int[] start = { 0, 0 };
            User user = new User(start);
            //Stopwatch x = new Stopwatch();
            //x.Start();
            maze1.Pathfind(start);
            //maze1.Render(des_height, des_width);

            aStar AStar = new aStar(start, des_width , des_height, maze1);
            AStar.Find();


            //aStar star = new aStar(start, des_height, des_width);
            //star.findSolution(maze1.CellList, start[0], start[1]);

            //x.Stop();
            //Console.WriteLine("took:" + x.ElapsedMilliseconds + " ms");
            Console.ReadKey();
        }
    }

}
