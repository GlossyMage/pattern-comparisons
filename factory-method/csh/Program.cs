using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comparison1
{
    class MazeGame
    {
        public Maze createMaze()
        {
            Maze m = makeMaze(5, 5);

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    m.addComponent(makeWall(), x, y);
                }
            }

            m.addComponent(makeRoom(), 1, 2);
            m.addComponent(makeRoom(), 2, 2);
            m.addComponent(makeRoom(), 3, 2);
            m.addComponent(makeRoom(), 1, 1);
            m.addComponent(makeRoom(), 3, 1);
            m.addComponent(makeRoom(), 1, 3);
            m.addComponent(makeRoom(), 3, 3);

            return m;
        }

        public Maze makeMaze(int x, int y)
        {
            return new Maze(x, y);
        }

        public virtual Room makeRoom()
        {
            return new Room();
        }

        public Wall makeWall()
        {
            return new Wall();
        }

        static void Main(string[] args)
        {
            MazeGame mg = null;
            mg.makeRoom();
            Console.ReadLine();

            /* if (args.Length > 0)
             {
                 if (args[0].Equals("maze"))
                 {
                     mg = new MazeGame();
                 } else if (args[0].Equals("coin"))
                 {
                     mg = new CoinMaze();
                 }
             } else
             {
                 Console.WriteLine("Usage: MazeGame <coin|maze>");
                 Console.ReadLine();
                 Environment.Exit(0);
             }*/
            Maze m = mg.createMaze();
            m.printMaze();
            Console.ReadLine();
        }
    }

    class CoinMaze : MazeGame
    {
        public override Room makeRoom()
        {
            return new CoinRoom();
        }
    }

    class Maze
    {
        private MazeComponent[,] map;
        private int length;
        private int height;

        public Maze(int x, int y)
        {
            map = new MazeComponent[x, y];
            length = x;
            height = y;
        }

        public void addComponent(MazeComponent c, int x, int y)
        {
            if (!(x >= length || y >= height || x < 0 || y < 0))
            {
                map[x, y] = c;
            }
        }

        public void printMaze()
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    Console.Write(map[x, y].getSymbol());
                }
                Console.WriteLine();
            }
        }
    }

    class Room : MazeComponent
    {
        public bool isSolid()
        {
            return false;
        }

        public char getSymbol()
        {
            return '.';
        }
    }

    class Wall : MazeComponent
    {
        public bool isSolid()
        {
            return true;
        }

        public char getSymbol()
        {
            return '#';
        }
    }

    class CoinRoom : Room
    {
        bool coin;

        public CoinRoom()
        {
            Random r = new Random();
            if (r.Next(100) >= 75)
            {
                coin = true;
            }
        }

        public new char getSymbol()
        {
            return coin ? '@' : '.';
        }
    }
}
