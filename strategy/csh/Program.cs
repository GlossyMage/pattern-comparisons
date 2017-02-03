using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comparison3
{
    class MazeGame
    {

        public static Maze createMaze(MazeFactory mf)
        {
            Maze m = mf.makeMaze(5, 5);

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    m.addComponent(mf.makeWall(), x, y);
                }
            }
            m.addComponent(mf.makeRoom(), 1, 2);
            m.addComponent(mf.makeRoom(), 2, 2);
            m.addComponent(mf.makeRoom(), 3, 2);
            m.addComponent(mf.makeRoom(), 1, 1);
            m.addComponent(mf.makeRoom(), 3, 1);
            m.addComponent(mf.makeRoom(), 1, 3);
            m.addComponent(mf.makeRoom(), 3, 3);

            return m;
        }

        static void Main(string[] args)
        {
            MazeFactory mf = null;
            String input = Console.ReadLine();

            if (input.Equals("maze"))
            {
                mf = new MazeFactory();
            } else if (input.Equals("coin"))
            {
                mf = new CoinFactory();
            } else
            {
                Environment.Exit(0);
            }

            Maze m = createMaze(mf);
            m.printMaze();
            Console.ReadLine();
        }
    }

    class MazeFactory
    {
        public virtual Maze makeMaze(int x, int y)
        {
            return new Maze(x, y);
        }

        public virtual Room makeRoom()
        {
            return new Room();
        }

        public virtual Wall makeWall()
        {
            return new Wall();
        }
    }

    class CoinFactory : MazeFactory
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
        public virtual char getSymbol()
        {
            return '.';
        }

        public bool isSolid()
        {
            return false;
        }
    }

    class Wall : MazeComponent
    {
        public char getSymbol()
        {
            return '#';
        }

        public bool isSolid()
        {
            return true;
        }
    }

    class CoinRoom : Room
    {
        bool coin = false;
        static Random r = new Random();

        public CoinRoom()
        {
            if (r.Next(100) >= 75)
            {
                coin = true;
            }
        }

        public override char getSymbol()
        {
            return coin ? '@' : '.';
        }
    }
}
