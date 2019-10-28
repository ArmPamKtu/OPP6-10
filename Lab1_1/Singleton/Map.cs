using System;
using System.Collections.Generic;
using System.Text;
using Lab1_1.AbstractFactory;

namespace Lab1_1
{
    public class Map
    {
        private const double CWonderThreshold = 0.98;
        private const double CGoldMineThreshold = 0.85;
        private const double CStoneThreshold = 0.7;
        private Unit[][] Grid { get; set; }
        private static int counter = 0;
        private static MapFactory mapFactory = new MapFactory();
        private static readonly object Instancelock = new object();
        private Random random = new Random();

        private Map()
        {
            //GenerateGrid(xSize, ySize);
            counter++;
            Console.WriteLine("Counter Value " + counter.ToString());
        }
        private static Map instance = null;

        public static Map GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (Instancelock)
                    {
                        if (instance == null)
                        {
                            instance = new Map();
                        }
                    }
                }
                return instance;
            }
        }
        public void PrintDetails(string message)
        {
            Console.WriteLine(message);
        }

        public Unit[][] GenerateGrid(int xSize, int ySize)
        {
            Grid = new Unit[ySize][];
            for (int y = 0; y < ySize; y++)
            {
                Grid[y] = new Unit[xSize];
                for (int x = 0; x < xSize; x++)
                {
                    double obstacleValue = random.NextDouble();
                    if (obstacleValue > CWonderThreshold)
                        Grid[y][x] = mapFactory.CreateSuperObstacle("Wonder", x, y);
                    else if (obstacleValue > CGoldMineThreshold)
                        Grid[y][x] = mapFactory.CreateSuperObstacle("Gold Mine", x, y);
                    else if (obstacleValue > CStoneThreshold)
                        Grid[y][x] = mapFactory.CreateObstacle("Stone", x, y);
                    else
                        Grid[y][x] = new Unit(x, y);
                }
            }
            return Grid;
        }

        public List<Unit> ConvertArrayToList()
        {
            List<Unit> unitList = new List<Unit>();
            for (int y = 0; y < GetYSize(); y++)
            {
                for (int x = 0; x < GetXSize(); x++)
                {
                    unitList.Add(GetUnit(x, y));
                }
            }
            return unitList;
        }

        public void ConvertListToArray(List<Unit> unitList)
        {
            for (int y = 0; y < GetYSize(); y++)
            {
                for (int x = 0; x < GetXSize(); x++)
                {
                    Grid[y][x] = unitList[y * GetXSize() + x];
                }
            }
        }

        public int GetYSize()
        {
            return Grid.Length;
        }

        public int GetXSize()
        {
            return Grid[0].Length;
        }

        public Unit GetUnit(int x, int y)
        {
            return Grid[y][x];
        }
    }
}
