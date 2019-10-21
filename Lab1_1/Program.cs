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
        private static Player player;
        private static ObstacleAbstractFactory shopFactory;
        private static ObstacleAbstractFactory mapFactory;

        private static string requestUri = "api/player/";
        private static int maxLobbyPlayers = 4;

        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            int turnLimit = 4;
            int[][] map = new int[10][];
            for (int i = 0; i < 10; i++)
            {
                map[i] = new int[10];
                for (int j = 0; j < 10; j++)
                {
                    map[i][j] = 0;
                }
            }


            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri("https://localhost:44371/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string command = "";
            Console.WriteLine("Welcome to splash Wars!");
            Console.WriteLine("Enter player's name to start looking for a loby( you will be added to a lobby automatically)");
            command = Console.ReadLine();
            player.SetName(command);

            CreatePlayerAsync(player).GetAwaiter().GetResult();

            ICollection<Player> playersInLobby = GetAllPlayersAsync(client.BaseAddress.PathAndQuery).GetAwaiter().GetResult();
            while (playersInLobby.Count < maxLobbyPlayers)
            {
                playersInLobby = GetAllPlayersAsync(client.BaseAddress.PathAndQuery).GetAwaiter().GetResult();
            }

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
            player.setAlgorithm(tower);

           /* player.Attach(new Tree());
            player.Attach(new Stone());
            player.Attach(new Tree());*/

            int n = 0;
            map[player.currentY][player.currentX] = 1;

            while (turnLimit > 0)
            {

                while (n < player.NumberOfActions)
                {
                    n++;
              
                    Console.WriteLine("Map looks like:");
                    Console.WriteLine("___________");
                    for (int i = 9; i >= 0; i--)
                    {
                        Console.Write("|");
                        for (int j = 0; j < 10; j++)
                        {
                            Console.Write(map[i][j]);
                        }
                        Console.Write("|\n");
                    }
                    Console.WriteLine("___________");
                    if (n != player.NumberOfActions)
                    {
                        Console.WriteLine("Choose where to go next R,L,U,D?");
                        command = Console.ReadLine();
                        player.move(player, command, map);
                       /* player.Notify();*/
                    }

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
                        ((Wolf)player).AttackASpecificArea(player, command, map);

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
                //  var ats = GetResponse(client);

                // RunAsync().GetAwaiter().GetResult();


            }
        }
        static void ShowPlayer(Player player)
        {
            Console.WriteLine("Player name: " + player.Name);
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://localhost:44371/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                string command = "";
                Console.WriteLine("1.1)\tCreate the player");
                command = Console.ReadLine();
                //        Player player = new Player
                //        {
                //            Name = "Studentas-" + playersList.Count.ToString(),
                //        };
                ICollection<Player> playersInLobby = await GetAllPlayersAsync(client.BaseAddress.PathAndQuery);
                while (playersInLobby.Count < maxLobbyPlayers)
                {
                    playersInLobby = await GetAllPlayersAsync(client.BaseAddress.PathAndQuery);
                }

                /* // Create a new product
                 Product product = new Product
                 {
                     Name = "Gizmo",
                     Price = 100,
                     Category = "Widgets"
                 };


                 var url =  CreateProductAsync(product);
                 Console.WriteLine($"Created at {url}");*/

                // Create a new player
                Console.WriteLine("1.1)\tCreate the player");
        //        Player player = new Player
        //        {
        //            Name = "Studentas-" + playersList.Count.ToString(),
        //        };


                // Get the product
                var ats = await GetProductAsync("api/values");
                Console.WriteLine(ats);

                // Update the product
           /*     Console.WriteLine("Updating price...");
                product.Price = 80;
                await UpdateProductAsync(product);

                // Get the updated product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Delete the product
                var statusCode = await DeleteProductAsync(product.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
                */
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
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
        static async Task<List<string>> GetProductAsync(string path)
        {
           
            List<String> a = new List<String>();
            HttpResponseMessage response = await client.GetAsync(path);
          
            if (response.IsSuccessStatusCode)
            {
                
                a = response.Content.ReadAsAsync<List<string>>().Result;
            }

            foreach(string var in a)
                Console.WriteLine(var);

            return a;
        }
    }
}
