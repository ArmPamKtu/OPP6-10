using Lab1_1.AbstractFactory;
using Lab1_1.Streategy;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_1
{
    class Program
    {
        private static Factory factory;
        private static Player player = new Player();
        private static Map map;
        private static ObstacleAbstractFactory shopFactory;
        private static ObstacleAbstractFactory mapFactory;

        private static string requestUri = "/api/player/";
        private static string gmRequestUri = "/api/gamecontroller/";
        static string mediaType = "application/json";
        private static int maxLobbyPlayers = 4;

        static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            int turnLimit = 4;
            /*
            int[][] map = new int[10][];
            for (int i = 0; i < 10; i++)
            {
                map[i] = new int[10];
                for (int j = 0; j < 10; j++)
                {
                    map[i][j] = 0;
                }
            }*/

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);

            client.BaseAddress = new Uri("https://localhost:44394/"); //api /player/");
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(mediaType));

            Console.WriteLine("Welcome to splash Wars!");
            Console.WriteLine("Enter map's size on X axis");
            int xSize = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter map's size on Y axis");
            int ySize = int.Parse(Console.ReadLine());
            Map.GetInstance.GenerateGrid(xSize, ySize);

            //--------
            /* Player player = new Player();
             Map.GetInstance.GetUnit(0, 0).TakeUnit(player);*/

            //----

            //List<Unit> mappp = await GetMap(1);
            //Console.WriteLine(mappp.Count);
            //Map.GetInstance.ConvertListToArray(mappp);

            for (int y = 0; y < Map.GetInstance.GetYSize(); y++)
            {
                for (int x = 0; x < Map.GetInstance.GetXSize(); x++)
                {
                    Console.ForegroundColor =Map.GetInstance.GetUnit(x, y).GetColor();
                    Console.Write(Map.GetInstance.GetUnit(x, y).GetSymbol());
                }
                Console.WriteLine();
            }
            Console.ResetColor();
         

            string command = "";
            Console.WriteLine("Welcome to splash Wars!");
            Console.WriteLine("Enter player's name to start looking for a loby( you will be added to a lobby automatically)");
            command = Console.ReadLine();

            //player.Name = command;
            //await CreatePlayerAsync(player);
            //ICollection<Player> playersInLobby = await GetAllPlayersAsync(client.BaseAddress.PathAndQuery);
            //while (playersInLobby.Count < maxLobbyPlayers)
            //{
            //    //Console.WriteLine("Waiting for other players");
            //    playersInLobby = GetAllPlayersAsync(client.BaseAddress.PathAndQuery).GetAwaiter().GetResult();
            //}

            Map map1 = Map.GetInstance;
            Map map2 = Map.GetInstance;

            AlgorithmFactory algorithmFactory = new AlgorithmFactory();
            Algorithm standart = algorithmFactory.GetDefault("Standart");
            Algorithm hopper = algorithmFactory.GetDefault("Hopper");
            Algorithm tower = algorithmFactory.GetDefault("Tower");
            Algorithm teleport = algorithmFactory.GetDefault("Teleport");



            Console.WriteLine("choose your faction:");
            Console.WriteLine("Wolfs - they get an extra action each turn");
            Console.WriteLine("Hunter - they start with extra money");
            Console.WriteLine("Hard worker - you get a small increase in actions each turn and a little bit of money");

            command = Console.ReadLine();

            factory = new FactionFactory();
           
            player = factory.CreatePlayerWithFaction(command);
            player.Creation();
            player.setAlgorithm(teleport);

            int n = 0;
            map1.GetUnit(0, 0).TakeUnit(player);

            player.currentX = 0;
            player.currentY = 0;
            while (turnLimit > 0)
            {
                ((Teleport)teleport).SetStartingPosition(player.currentX, player.currentY);
                while (n < player.NumberOfActions)
                {
                    
                    n++;
              
                    Console.WriteLine("Map looks like:");
                    Console.WriteLine("___________");

                    for (int y = 0; y < Map.GetInstance.GetYSize(); y++)
                    {
                        for (int x = 0; x < Map.GetInstance.GetXSize(); x++)
                        {
                            Console.ForegroundColor = Map.GetInstance.GetUnit(x, y).GetColor();
                            Console.Write(Map.GetInstance.GetUnit(x, y).GetSymbol());
                        }
                        Console.WriteLine();
                    }
                    Console.ResetColor();

                    Console.WriteLine("___________");

                    if (n != player.NumberOfActions)
                    {
                        Console.WriteLine("Choose where to go next R,L,U,D?");
                        command = Console.ReadLine();
                        player.ExecuteMove(command, player);
                      //  player.move(player, command, Map.GetInstance);
                    }

                }
                Console.WriteLine("\n\n");
                Console.WriteLine("Would you like to undo your moves? (Yes/No)");
                command = Console.ReadLine();
                if(command.Equals("Yes"))
                {
                    player.Undo();
                }
                else
                {
                    player.ResetCommands();
                }

                Console.WriteLine("You have " + player.Money + " Money");
                Console.WriteLine("Do you want to move like:");
                Console.WriteLine("Tower - go till the end of the line");
                Console.WriteLine("Hopper - jump over a space");
                Console.WriteLine("Teleport - write two digits and teleport on those coordinates");
                Console.WriteLine("Standart - go one space in what direction you want");
                command = Console.ReadLine();
                if (command.Equals("Tower"))
                    player.setAlgorithm(tower);
                else if(command.Equals("Hopper"))
                    player.setAlgorithm(hopper);
                else if (command.Equals("Teleport"))
                    player.setAlgorithm(teleport);
                else 
                    player.setAlgorithm(standart);

                
                if (player is HardWorker)
                {
                    Console.WriteLine("As a Hard worker you can work harder and get more money per action( double), but have less actions per round( 6 actions)");
                    if(player.MoneyMultiplier == 1)
                        Console.WriteLine("Type 'Work' to do it!");
                    else
                        Console.WriteLine("Type 'Stop' to go back to normal mode");
                    command = Console.ReadLine();

                    if(command.Equals("Work"))
                        ((HardWorker)player).WorkHarder();
                    else if(command.Equals("Stop"))
                        ((HardWorker)player).GetBackToNormal();
                }
                else if(player is Wolf)
                {
                    if (((Wolf)player).GetAttackLimit() != 0)
                    {
                        Console.WriteLine("As a Wolf you can attack an area and capture it");
                        Console.WriteLine("If you want to attack, type a direction: R,L,U,D");
                        command = Console.ReadLine();
                        ((Wolf)player).AttackASpecificArea(player, command, Map.GetInstance);

                    }
                }
                else
                {

                }


                turnLimit--;
                n = 0;
            }
           // shopFactory = new ShopFactory();
           // mapFactory = new MapFactory();

            while (!command.Equals("Stop"))
            {
                Console.WriteLine("Create an obstacle:");
                Console.WriteLine("Stone; Tree; GoldMine; ActionTower; Wonder");

                command = Console.ReadLine();

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
        }
        static void ShowPlayer(Player player)
        {
            Console.WriteLine("Player name: " + player.Name);
        }

        static async Task<Uri> CreatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                requestUri, player);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            Player player2 = await response.Content.ReadAsAsync<Player>();
            if (player2 != null)
            {
                ShowPlayer(player2);
            }

            // return URI of the created resource.
            return response.Headers.Location;
        }
        static async Task<ICollection<Player>> GetAllPlayersAsync(string path)
        {
            ICollection<Player> players = null;
            HttpResponseMessage response = await client.GetAsync(path + "api/player");
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<ICollection<Player>>();
            }
            return players;
        }

        static async Task<List<Unit>> GetMap( int id)
        {
            List<Unit> map = null;
            HttpResponseMessage response = await client.GetAsync(gmRequestUri + $"{id}");
            if (response.IsSuccessStatusCode)
            {
                map = await response.Content.ReadAsAsync<List<Unit>>();
            }
            return map;
        }
    }
}
