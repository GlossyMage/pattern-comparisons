using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comparison4
{
    class MazeGame
    {
        public static Maze createMaze(MazePrototypeFactory mpf)
        {
            Maze m = mpf.makeMaze();

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    m.addComponent(mpf.makeWall(), x, y);
                }
            }

            m.addComponent(mpf.makeRoom(), 1, 2);
            m.addComponent(mpf.makeRoom(), 2, 2);
            m.addComponent(mpf.makeRoom(), 3, 2);
            m.addComponent(mpf.makeRoom(), 1, 1);
            m.addComponent(mpf.makeRoom(), 3, 1);
            m.addComponent(mpf.makeRoom(), 1, 3);
            m.addComponent(mpf.makeRoom(), 3, 3);

            return m;
        }

        static void Main(string[] args)
        {
            MazePrototypeFactory mpf = new MazePrototypeFactory(new Maze(5, 5), new CoinRoom(), new Wall());
            Maze m = createMaze(mpf);
            m.printMaze();
            Console.ReadLine();
        }
    }

    class MazePrototypeFactory
    {
        private Maze prototypeMaze;
        private Room prototypeRoom;
        private Wall prototypeWall;

        public MazePrototypeFactory(Maze m, Room r, Wall w)
        {
            prototypeMaze = m;
            prototypeRoom = r;
            prototypeWall = w;
        }

        public Maze makeMaze()
        {
            return prototypeMaze.clone();
        }

        public Room makeRoom()
        {
            return (Room) prototypeRoom.clone();
        }

        public Wall makeWall()
        {
            return (Wall) prototypeWall.clone();
        }
    }

    class Maze
    {
        private MazeComponent[,] map;
        private int length, height;

        public Maze(int x, int y)
        {
            length = x;
            height = y;
            map = new MazeComponent[x, y];
        }

        private Maze(Maze other)
        {
            length = other.getLength();
            height = other.getHeight();
            map = new MazeComponent[length, height];
            MazeComponent[,] otherMap = other.getMap();

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (null != otherMap[i,j])
                    {
                        map[i, j] = otherMap[i, j].clone();
                    }
                }
            }
        }

        public Maze clone()
        {
            return new Maze(this);
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

        public int getLength()
        {
            return length;
        }

        public int getHeight()
        {
            return height;
        }

        public MazeComponent[,] getMap()
        {
            return map;
        }
    }

    class Room : MazeComponent
    {
        public virtual MazeComponent clone()
        {
            return new Room();
        }

        public virtual char getSymbol()
        {
            return '.';
        }

        public bool isSolid()
        {
            return false;
        }
    }

    class CoinRoom : Room
    {
        bool coin;
        public static int seed;

        public CoinRoom()
        {
            Random r;
            if (seed == 0)
            {
                r = new Random();
                seed = r.Next();
            }
            r = new Random(seed++);
            coin = false;
            if (r.Next(100) >= 75)
            {
                coin = true;
            }
        }

        public override MazeComponent clone()
        {
            return new CoinRoom();
        }

        public override char getSymbol()
        {
            return coin ? '@' : '.';
        }
    }

    class Wall : MazeComponent
    {
        public MazeComponent clone()
        {
            return new Wall();
        }

        public char getSymbol()
        {
            return '#';
        }

        public bool isSolid()
        {
            return true;
        }
    }
}
