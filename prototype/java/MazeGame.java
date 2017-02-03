import java.util.Random;

public class MazeGame {

    public static Maze createMaze(MazePrototypeFactory mpf) {
        Maze m = mpf.makeMaze();

        for (int x = 0; x < 5; x++) {
            for (int y = 0; y < 5; y++) {
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

    public static void main(String[] args) {
        MazePrototypeFactory mpf = null;
        Room protoRoom = null;
        if (args.length > 0) {
            if (args[0].equals("maze")) {
                protoRoom = new Room();
            } else if (args[0].equals("coin")) {
                protoRoom = new CoinRoom();
            }
        } else {
            System.out.println("Usage: java MazeGame <coin|maze>");
            System.exit(0);
        }
        mpf = new MazePrototypeFactory(new Maze(5, 5), protoRoom, new Wall());
        Maze m = createMaze(mpf);
        m.printMaze();
    }
}

class MazePrototypeFactory {
    private Maze prototypeMaze;
    private Room prototypeRoom;
    private Wall prototypeWall;
    
    public MazePrototypeFactory(Maze m, Room r, Wall w) {
        prototypeMaze = m;
        prototypeRoom = r;
        prototypeWall = w;
    }

    public Maze makeMaze() {

        return prototypeMaze.clone();
    }

    public Room makeRoom() {
        return prototypeRoom.clone();
    }

    public Wall makeWall() {
        return prototypeWall.clone();
    }
}

class Maze {
    private MazeComponent[][] map;
    private int length;
    private int height;

    public Maze(int x, int y) {
        map = new MazeComponent[x][y];
        length = x;
        height = y;
    }

    private Maze(Maze other) {
        length = other.getLength();
        height = other.getHeight();
        map = new MazeComponent[length][height];
        MazeComponent[][] otherMap = other.getMap();

        for (int i = 0; i < length; i++) {
            for (int j = 0; j < height; j++) {
                if (otherMap[i][j] != null) {
                    map[i][j] = otherMap[i][j].clone();
                }
            }
        }
    }

    public Maze clone() {
        return new Maze(this);
    }

    public void addComponent(MazeComponent c, int x, int y) {
        if (!(x >= length || y >= height || x < 0 || y < 0)) {
            map[x][y] = c;
        }
    }

    public void printMaze() {
        for (int y = 0; y < 5; y++) {
            for (int x = 0; x < 5; x++) {
                System.out.print(map[x][y].getSymbol());
            }
            System.out.println("");
        }
    }

    public int getLength() {
        return length;
    }

    public int getHeight() {
        return height;
    }

    public MazeComponent[][] getMap() {
        return map;
    }
}

class Room implements MazeComponent {
    
    public Room clone() {
        return new Room();
    }

    public boolean isSolid() {
        return false;
    }

    public char getSymbol() {
        return '.';
    }
}

class CoinRoom extends Room {
    boolean coin;

    public CoinRoom() {
        Random r = new Random();
        if (r.nextInt(100) >= 75) {
            coin = true;
        }
    }

    public CoinRoom clone() {
        return new CoinRoom();
    }

    public char getSymbol() {
        return coin ? '@' : '.';
    }
}

class Wall implements MazeComponent {

    public Wall clone() {
        return new Wall();
    }

    public boolean isSolid() {
        return true;
    }

    public char getSymbol() {
        return '#';
    }
}
