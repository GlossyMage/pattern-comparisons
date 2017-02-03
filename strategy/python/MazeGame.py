from abc import ABCMeta, abstractmethod
import random, time, sys

class MazeGame:

    def createMaze(self, mazeFactory):
        m = mazeFactory.makeMaze(5, 5)

        for x in range(5):
            for y in range(5):
                m.addComponent(mazeFactory.makeWall(), x, y)

        m.addComponent(mazeFactory.makeRoom(), 1, 2)
        m.addComponent(mazeFactory.makeRoom(), 2, 2)
        m.addComponent(mazeFactory.makeRoom(), 3, 2)
        m.addComponent(mazeFactory.makeRoom(), 1, 1)
        m.addComponent(mazeFactory.makeRoom(), 3, 1)
        m.addComponent(mazeFactory.makeRoom(), 1, 3)
        m.addComponent(mazeFactory.makeRoom(), 3, 3)

        return m

class MazeFactory:

    def makeMaze(self, x, y):
        return Maze(x, y)

    def makeRoom(self):
        return Room()

    def makeWall(self):
        return Wall()

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

class MazeComponent:
    __metaclass__ = ABCMeta

    @abstractmethod
    def isSolid(self): pass

    @abstractmethod
    def getSymbol(self): pass

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

class CoinFactory(MazeFactory):
    def makeRoom(self):
        return CoinRoom()

class CoinRoom(Room):
    def __init__(self):
        self.coin = False
        if random.randint(0, 100) >= 75:
            self.coin = True

    def getSymbol(self):
        if self.coin == True:
            return '@'
        else:
            return '.'

def main():
    mf = None
    if len(sys.argv) == 2:
        if sys.argv[1] == 'maze':
            mf = MazeFactory()
        elif sys.argv[1] == 'coin':
            mf = CoinFactory()
        else:
            print("Invalid input.")
            sys.exit()
    else:
        print("Usage: python MazeGame.py <maze|coin>")
        sys.exit()

    t1 = time.time()
    mg = MazeGame()
    m = mg.createMaze(mf)
    t2 = time.time() - t1
    print(t2)
    m.printMaze()

if __name__ == "__main__":
    main()
