using Lab1_1.AbstractFactory;
using Lab1_1.Streategy;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_1.Facade
{
    class GameManager
    {

        private Lobby lobby;
        public AlgorithmFactory algorithmFactory;
        private ObstacleAbstractFactory mapFactory;
        private ObstacleAbstractFactory shopFactory;
        public Player player;
        public FactionFactory factory;

        public Algorithm standart;
        public Algorithm hopper;
        public Algorithm tower;
        public Algorithm teleport;

        public HttpClient client { get; set; }

        public GameManager(HttpClient client)
        {
            this.client = client;
            lobby = new Lobby();
            algorithmFactory = new AlgorithmFactory();
            player = new Player();
            factory = new FactionFactory();
            standart = algorithmFactory.GetDefault("Standart");
            hopper = algorithmFactory.GetDefault("Hopper");
            tower = algorithmFactory.GetDefault("Tower");
            teleport = algorithmFactory.GetDefault("Teleport");
        }
        public Unit[][] generateGrid(int xSize, int ySize)
        {
            Map.GetInstance.GenerateGrid(xSize, ySize);
        }
        public int GetYSize()
        {
            return Map.GetInstance.GetYSize();
        }
        public int GetXSize()
        {
            return Map.GetInstance.GetXSize();
        }
        public ConsoleColor getColor(int x, int y)
        {
            return Map.GetInstance.GetUnit(x, y).GetColor();
        }
        public char getSymbol(int x, int y)
        {
            return Map.GetInstance.GetUnit(x, y).GetSymbol();
        }
        public void takeUnit()
        {
            Map.GetInstance.GetUnit(0, 0).TakeUnit(player);
        }
        public void Undo(string command, ref bool finishedIteration)
        {
            if (command.Equals("Yes"))
            {
                player.Undo();
                finishedIteration = false;
            }
            else
            {
                player.ResetCommands();
                finishedIteration = true;
            }
        }
        public void MovePlayerNext(string command, ref bool succesfulMove)
        {
            string[] numbers = command.Split(' ');
            int XPosition = 0;
            int YPosition = 0;

            bool number1Success = Int32.TryParse(numbers[0], out XPosition);
            bool number2Success = Int32.TryParse(numbers[1], out YPosition);

            if (number1Success && number1Success && XPosition < 20 && YPosition < 20 && XPosition >= 0 && YPosition >= 0 && Map.GetInstance.GetUnit(XPosition, YPosition).symbol.Equals('0'))
            {
                player.ExecuteMove(command, player);
                succesfulMove = false;
            }
            else
            {
                Console.WriteLine("Your number where wrong or you are teleporting on an obstacle");
            }
        }
        public void MovePlayer(string command, ref bool succesfulMove)
        {
            switch (command)
            {
                case ("D"):
                    if (player.currentY + player.Power < Map.GetInstance.GetYSize() && Map.GetInstance.GetUnit(player.currentX, player.currentY + player.Power).symbol.Equals('0'))
                    {
                        player.ExecuteMove(command, player);
                        succesfulMove = false;
                    }
                    else
                        Console.WriteLine("You are at the edge of the map OR going into an obstacle");
                    break;
                case ("U"):
                    if (player.currentY - player.Power >= 0 && Map.GetInstance.GetUnit(player.currentX, player.currentY - player.Power).symbol.Equals('0'))
                    {
                        player.ExecuteMove(command, player);
                        succesfulMove = false;
                    }
                    else
                        Console.WriteLine("You are at the edge of the map OR going into an obstacle");
                    break;
                case ("R"):
                    if (player.currentX + player.Power < Map.GetInstance.GetXSize() && Map.GetInstance.GetUnit(player.currentX + player.Power, player.currentY).symbol.Equals('0'))
                    {
                        player.ExecuteMove(command, player);
                        succesfulMove = false;
                    }
                    else
                        Console.WriteLine("You are at the edge of the map OR going into an obstacle");
                    break;
                case ("L"):
                    if (player.currentX - player.Power >= 0 && Map.GetInstance.GetUnit(player.currentX - player.Power, player.currentY).symbol.Equals('0'))
                    {
                        player.ExecuteMove(command, player);
                        succesfulMove = false;
                    }
                    else
                        Console.WriteLine("You are at the edge of the map OR going into an obstacle");
                    break;
            }
        }
        public void CreatePlayerWithFaction(string command, Algorithm standart)
        {
            player = factory.CreatePlayerWithFaction(command);
            player.Creation();
            player.setAlgorithm(standart);
        }
        public void CreateAnObstacle(string command)
        {
            switch (command)
            {
                case "Stone":
                    mapFactory.CreateObstacle(command);
                    break;
                case "Tree":
                    shopFactory.CreateObstacle(command);
                    break;
                case "Gold Mine":
                    mapFactory.CreateSuperObstacle(command);
                    break;
                case "Action Tower":
                    shopFactory.CreateSuperObstacle(command);
                    break;
                case "Wonder":
                    mapFactory.CreateSuperObstacle(command);
                    break;
                default:
                    break;
            }
        }

        public string LobbyIsFull()
        {
            while (lobby.LobbyIsFullAsync().Result)
            {
                return "Waiting for other players...";
            }
            return "Lobby is full";
        }
        public Uri CreatePlayer(Player player)
        {
            return lobby.CreatePlayerAsync(player).Result;
        }
        public async Task<Player> GetPlayer(string path)
        {
            return await lobby.GetPlayerAsync(path);
        }
    }
}
