using System;
using System.Collections.Generic;
using System.Linq;

namespace Mars_Rover
{
    public class Program
    {
        public static IEnumerable<Position> Find(string corner, params string[] rest)
        {
            string[] corners = corner.Split(' ');
            int maxX = int.Parse(corners[0]);
            int maxY = int.Parse(corners[1]);

            var RoverList = new List<Rover>();
            for (int i = 0; i < rest.Length; i += 2)
            {
                string[] positionDetail = rest[i].Split(' ');

                RoverList.Add(new Rover
                {
                    Instructions = rest[i + 1],
                    Position = new Position
                    {
                        X = int.Parse(positionDetail[0]),
                        Y = int.Parse(positionDetail[1]),
                        Direction = char.Parse(positionDetail[2])
                    }
                });
            }

            foreach (var rover in RoverList)
            {
                foreach (var instruction in rover.Instructions)
                {
                    if (instruction == 'M')
                    {
                        Step(rover.Position, maxX, maxY);
                    }
                    else
                    {
                        Spin(rover.Position, instruction);
                    }
                }
            }

            return RoverList.Select(x => x.Position);
        }

        public static void Spin(Position Position, char direction)
        {
            char[] compass = new char[] { 'W', 'N', 'E', 'S' };
            var currentIndex = Array.IndexOf(compass, Position.Direction);

            if (direction == 'L')
            {
                currentIndex = currentIndex == 0 ? compass.Length : currentIndex;
                Position.Direction = compass[currentIndex - 1];
            }
            else
            {
                Position.Direction = compass[(currentIndex + 1) % compass.Length];
            }
        }

        public static void Step(Position Position, int maxX, int maxY)
        {
            switch (Position.Direction)
            {
                case 'W':
                    Position.X = Math.Max(Position.X - 1, 0);
                    break;
                case 'N':
                    Position.Y = Math.Min(Position.Y + 1, maxY);
                    break;
                case 'E':
                    Position.X = Math.Min(Position.X + 1, maxX);
                    break;
                case 'S':
                    Position.Y = Math.Max(Position.Y - 1, 0);
                    break;
            }
        }

        static void Main(string[] args)
        {
            var positions = Find("5 5",
                                "1 2 N",
                                "LMLMLMLMM",
                                "3 3 E",
                                "MMRMMRMRRM");
            Console.WriteLine(positions);
        }
    }
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Direction { get; set; }
    }
    public class Rover
    {
        public Position Position { get; set; }
        public string Instructions { get; set; }
    }
}
