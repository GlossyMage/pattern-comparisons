from abc import ABCMeta, abstractmethod
import random, time, sys, copy

class MazeGame:

    def createMaze(self, mazePrototypeFactory):
        m = mazePrototypeFactory.makeMaze()

        for x in range(5):
            for y in range(5):
                m.addComponent(mazePrototypeFactory.makeWall(), x, y)

        m.addComponent(mazePrototypeFactory.makeRoom(), 1, 2)
        m.addComponent(mazePrototypeFactory.makeRoom(), 2, 2)
        m.addComponent(mazePrototypeFactory.makeRoom(), 3, 2)
        m.addComponent(mazePrototypeFactory.makeRoom(), 1, 1)
        m.addComponent(mazePrototypeFactory.makeRoom(), 3, 1)
        m.addComponent(mazePrototypeFactory.makeRoom(), 1, 3)
        m.addComponent(mazePrototypeFactory.makeRoom(), 3, 3)

        return m

class MazePrototypeFactory:

    def __init__(self, maze, room, wall):
        self.prototypeMaze = maze
        self.prototypeRoom = room
        self.prototypeWall = wall

    def makeMaze(self):
        return self.prototypeMaze.clone()

    def makeRoom(self):
        return self.prototypeRoom.clone()

    def makeWall(self):
        return self.prototypeWall.clone()

class Maze:

    def __init__(self, x, y):
        self.length = x
        self.height = y
        self.grid = []
        
        for i in range(x):
            self.grid.append([])
            for j in range(y):
                self.grid[i].append(0)

    def addComponent(self, component, x, y):
        if isinstance(component, MazeComponent):
            try:
                self.grid[x][y] = component
            except:
                return

    def printMaze(self):
        for y in range(self.length):
            for x in range(self.height):
                print(self.grid[x][y].getSymbol(), end="")
            print("")

    def clone(self):
        return copy.deepcopy(self)

class MazeComponent:
    __metaclass__ = ABCMeta

    @abstractmethod
    def isSolid(self): pass

    @abstractmethod
    def getSymbol(self): pass

    def clone(self):
        return copy.deepcopy(self)

class Room(MazeComponent):
    def isSolid(self):
        return False

    def getSymbol(self):
        return '.'

class Wall(MazeComponent):
    def isSolid(self):
        return True

    def getSymbol(self):
        return '#'

class CoinRoom(Room):
    def __init__(self):
        self.coin = False

    def determineCoin(self):
        if random.randint(0, 100) >= 75:
            self.coin = True

    def getSymbol(self):
        if self.coin == True:
            return '@'
        else:
            return '.'

    def clone(self):
        c = copy.deepcopy(self)
        c.determineCoin()
        return c


def main():
    mf = None
    room = None
    if len(sys.argv) == 2:
        if sys.argv[1] == 'maze':
            room = Room()
        elif sys.argv[1] == 'coin':
            room = CoinRoom()
        else:
            print("Invalid input.")
            sys.exit()
    else:
        print("Usage: python MazeGame.py <maze|coin>")
        sys.exit()

    mf = MazePrototypeFactory(Maze(5, 5), room, Wall())
    t1 = time.time()
    mg = MazeGame()
    m = mg.createMaze(mf)
    t2 = time.time() - t1
    print(t2)
    m.printMaze()

if __name__ == "__main__":
    main()
