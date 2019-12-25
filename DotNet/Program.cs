using System;
using System.Collections.Generic;
using System.Linq;

namespace Mars_Rover
{
    public class Program
    {

        /// <summary>
        ///  Gets the related inputs and return last positions of rovers
        /// </summary>
        /// <param name="corner">First line of input</param>
        /// <param name="rest">Input for rovers and instructions</param>
        /// <returns></returns>
        public static IEnumerable<Position> Find(string corner, params string[] rest)
        {
            // Split first line of input by space to get rectangle upper-right coordinates
            string[] corners = corner.Split(' ');
            int maxX = int.Parse(corners[0]);
            int maxY = int.Parse(corners[1]);
            char[] compass = new char[] { 'W', 'N', 'E', 'S' };

            var RoverList = new List<Rover>();
            // doubly iterate over 'rest' parameter to get position & instructions for each rovers
            for (int i = 0; i < rest.Length; i += 2)
            {
                // get the position for current rover
                string[] positionDetail = rest[i].Split(' ');

                // add to list each rover position and instructions after parsing related input
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

            // iterate over that list to find out each rover next move
            foreach (var rover in RoverList)
            {
                foreach (var instruction in rover.Instructions)
                {
                    if (instruction == 'M')
                    {
                        Step(rover.Position, maxX, maxY);   // step if the current instruction shows 'M' (move)
                    }
                    else
                    {
                        Spin(compass, rover.Position, instruction);  // spin 90 degrees left (L) or right (R) respectively without move
                    }
                }
            }

            return RoverList.Select(x => x.Position);
        }

        /// <summary>
        /// Updates rover direction by instruction.
        /// </summary>
        /// <param name="Position"> Rover position instance (X,Y,Direction)</param>
        /// <param name="direction"> Instruction direction (left or right) </param>
        public static void Spin(char[] compass, Position Position, char direction)
        {
            var currentIndex = Array.IndexOf(compass, Position.Direction);

            if (direction == 'L')
            {
                currentIndex = currentIndex == 0 ? compass.Length : currentIndex;   // shoud act like circular
                Position.Direction = compass[currentIndex - 1];
            }
            else
            {
                Position.Direction = compass[(currentIndex + 1) % compass.Length];
            }
        }

        /// <summary>
        /// Updates rover x and y coordinates by orientation.
        /// </summary>
        /// <param name="Position">Rover position instance (X,Y,Direction) </param>
        /// <param name="maxX"> Right edge coordinate from corner input (first line) </param>
        /// <param name="maxY"> Upper edge coordinate from corner input (first line) </param>
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
            // sample inputs
            var positions = Find("5 5",
                                "1 2 N",
                                "LMLMLMLMM",
                                "3 3 E",
                                "MMRMMRMRRM");
            // sample outputs
            positions.ToList().ForEach(p =>
            {
                Console.WriteLine(string.Join(" ", p.X, p.Y, p.Direction));
            });
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
