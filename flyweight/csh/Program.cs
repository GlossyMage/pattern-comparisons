using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comparison5
{
    class MazeGame
    {
        ComponentFactory cf;

        public MazeGame()
        {
            cf = new ComponentFactory();
        }

        public Maze MakeMaze(int x, int y)
        {
            return new Maze(x, y);
        }

        public Maze CreateMaze()
        {
            Maze m = MakeMaze(5, 5);

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    m.AddComponent(MakeWall(), x, y);
                }
            }

            m.AddComponent(MakeRoom(), 1, 2);
            m.AddComponent(MakeRoom(), 2, 2);
            m.AddComponent(MakeRoom(), 3, 2);
            m.AddComponent(MakeRoom(), 1, 1);
            m.AddComponent(MakeRoom(), 3, 1);
            m.AddComponent(MakeRoom(), 1, 3);
            m.AddComponent(MakeRoom(), 3, 3);

            return m;
        }



        public MazeComponent MakeRoom()
        {
            return cf.CreateComponent(ComponentType.CoinRoom);
        }

        public MazeComponent MakeWall()
        {
            return cf.CreateComponent(ComponentType.Wall);
        }

        static void Main(string[] args)
        {
            MazeGame mg = new MazeGame();
            Maze m = mg.CreateMaze();
            Console.WriteLine("1:");
            m.PrintMaze();
            Console.WriteLine("2:");
            m.PrintMaze();
            Console.ReadLine();
        }
    }

    class Maze
    {
        private MazeComponent[,] map;
        private int length, height, seed;

        public Maze(int x, int y)
        {
            map = new MazeComponent[x, y];
            length = x;
            height = y;

            Random r = new Random();
            seed = r.Next();
        }

        public void AddComponent(MazeComponent c, int x, int y)
        {
            if (!(x >= length || y >= height || x < 0 || y < 0))
            {
                map[x, y] = c;
            }
        }

        public void PrintMaze()
        {
            MazeContext mc = new MazeContext(seed);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Console.Write(map[x, y].GetSymbol(mc));
                    mc.Next();
                }
                Console.WriteLine();
            }
        }
    }

    class ComponentFactory
    {
        private MazeComponent[] flyweights;
        
        public ComponentFactory()
        {
            flyweights = new MazeComponent[Enum.GetNames(typeof(ComponentType)).Length];
        }

        public MazeComponent CreateComponent(ComponentType ct)
        {
            if (null == flyweights[(int) ct])
            {
                flyweights[(int) ct] = new MazeComponent(ct);
            }

            return flyweights[(int) ct];
        }
    }

    enum ComponentType
    {
        Wall, Room, CoinRoom
    }

    class MazeComponent
    {
        ComponentType type;

        public MazeComponent(ComponentType ct)
        {
            this.type = ct;
        }

        public bool IsSolid()
        {
            return type == ComponentType.Wall ? true : false;
        }

        public char GetSymbol(MazeContext mc)
        {
            switch (type)
            {
                case ComponentType.Wall:
                    return '#';
                case ComponentType.Room:
                    return '.';
                case ComponentType.CoinRoom:
                    return mc.HasCoin() ? '@' : '.';
                default:
                    return ' ';
            }
        }
    }

    class MazeContext
    {
        private int seed;
        private int index = 0;

        public MazeContext(int seed)
        {
            this.seed = seed;
        }

        public void Next()
        {
            index++;
        }

        public bool HasCoin()
        {
            Random r = new Random(seed * index);
            if (r.Next(100) >= 75)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
