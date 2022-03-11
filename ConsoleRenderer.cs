using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32.SafeHandles;






namespace MajorProjectDesktop
{
    internal class ConsoleRenderer
    {

        public StringBuilder buffer = new StringBuilder();
        public const string BlackBG = "\u001b[40m";
        public const string RedBG = "\u001b[41m";
        public const string GreenBG = "\u001b[42m";
        public const string YelowBG = "\u001b[43m";
        public const string BlueBG = "\u001b[44m";
        public const string MagentaBG = "\u001b[45m";
        public const string CyanBG = "\u001b[46m";
        public const string WhiteBG = "\u001b[47m";
        private int _width;
        private int _height;
        private int _end;
        private int _cellscale;
        private Stream stdout;

        public int Width { get => _width; set => _width = value; }
        public int End { get => _end; set => _end = value; }
        public int Height { get => _height; set => _height = value; }
        public int Cellscale { get => _cellscale; set => _cellscale = value; }

        public ConsoleRenderer(int w, int h, int scale)
        {

            stdout = Console.OpenStandardOutput();
            var con = new StreamWriter(stdout, Encoding.ASCII);
            con.AutoFlush = true;
            Console.SetOut(con);
            buffer.EnsureCapacity((w + 1) * (h + 1));
            Width = w;
            Height = h;
            End = 0;
        }

        public void ClearScreen()
        {
            buffer.Clear();
            for (int i = 0; i < (Width + 1) * 2; i++)
            {
                buffer.Append(BlueBG + ' ');

            }
            buffer.Append("\n ");
        }
        public void AddColoredCellToBuffer(string bgCol, int CellSize = 1)
        {
            //stdout.WriteAsync(Encoding.ASCII.GetBytes(bgCol + ' '));
            if (End == Width)
            {
                for (int i = 0; i < CellSize - 1; i++)
                {
                    buffer.Append(GreenBG + ' ');
                }
                End = 0;
                return;
            }
            else
                buffer.Append(bgCol + ' ');
        }

        public void BufferNextLine()
        {
            //stdout.WriteAsync(Encoding.ASCII.GetBytes("\u001b[40m\n"));
            buffer.Append(BlueBG + ' ' + BlackBG + '\n' + BlueBG + ' ');

        }
        public void drawBuffer()
        {
            Console.SetCursorPosition(0,0);
            stdout.Write(Encoding.ASCII.GetBytes(buffer + "\0"), 0, buffer.Length);
            Console.Write(BlackBG + " ");
        }
    }
}
