using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comparison2
{
    class MazeGame
    {
        private MazeGenerator mazeGen;

        public MazeGame(String type)
        {
            if (type.Equals("box"))
            {
                mazeGen = new BoxGenerator();
            } else if (type.Equals("line"))
            {
                mazeGen = new LineGenerator();
            } else
            {
                Environment.Exit(0);
            }
        }

        public Maze createMaze(int x, int y)
        {
            if (mazeGen != null)
            {
                return mazeGen.generate(x, y);
            }
            return null;
        }

        Maze makeMaze(int x, int y)
        {
            return new Maze(x, y);
        }

        static void Main(string[] args)
        {
            MazeGame mg = new MazeGame(Console.ReadLine());
            Maze m = mg.createMaze(10, 10);
            m.printMaze();
            Console.ReadLine();
        }
    }

    abstract class MazeGenerator
    {
        public abstract Maze generate(int x, int y);

        protected Wall makeWall()
        {
            return new Wall();
        }

        protected Room makeRoom()
        {
            return new Room();
        }
    }

    class LineGenerator : MazeGenerator
    {
        public override Maze generate(int x, int y)
        {
            Maze m = new Maze(x, y);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    m.addComponent(makeWall(), i, j);
                }
            }

            int k = y / 2;

            for (int i = 1; (i < (x - 1)) && (k > 0) && (k < (y - 1)); i++)
            {
                m.addComponent(makeRoom(), i, k);
            }

            return m;
        }
    }

    class BoxGenerator : MazeGenerator
    {
        public override Maze generate(int x, int y)
        {
            if (x < 3 || y < 3)
            {
                Console.Write("WHAT");
                return null;
            }

            Maze m = new Maze(x, y);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    m.addComponent(makeWall(), i, j);
                }
            }

            for (int i = 1; i < (x - 1); i++)
            {
                m.addComponent(makeRoom(), i, 1);
                m.addComponent(makeRoom(), i, y - 2);
            }

            for (int j = 1; j < (y - 1); j++)
            {
                m.addComponent(makeRoom(), 1, j);
                m.addComponent(makeRoom(), x - 2, j);
            }

            return m;
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
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < length; x++)
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
}
