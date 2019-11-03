using Lab1_1.AbstractFactory;
using Lab1_1.Streategy;
using Lab1_1.Observer;
using System;
using System.Collections.Generic;
using System.Net;
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

            GameState gs = new GameState();
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);

            client.BaseAddress = new Uri("https://localhost:44397/"); //api /player/");
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(mediaType));

            Console.WriteLine("Welcome to splash Wars!");
            Console.WriteLine("Enter map's size on X axis");
            int xSize = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter map's size on Y axis");
            int ySize = int.Parse(Console.ReadLine());
            Map.GetInstance.GenerateGrid(xSize, ySize);

            //----

            //For multi
            //List<Unit> serverMap = await GetMap(1);
            //Map.GetInstance.ConvertListToArray(serverMap);

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

            //For multi
            //player.Name = command;
            //var url = await CreatePlayerAsync(player);
            //ICollection<Player> playersInLobby = await GetAllPlayersAsync(client.BaseAddress.PathAndQuery);
            //while (playersInLobby.Count < maxLobbyPlayers)
            //{
            //    playersInLobby = await GetAllPlayersAsync(client.BaseAddress.PathAndQuery);
            //}
            //Player p = await GetPlayerAsync(url.PathAndQuery);
            ////string json = JsonConvert.SerializeObject(p, Formatting.Indented);
            ////Console.WriteLine(json);

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
            player.setAlgorithm(standart);

            int n = 0;

            //For multi 
            //player.id = p.id;
            //player.currentX = p.currentX;
            //player.currentY = p.currentY;
            //player.color = p.color;
            ////Console.WriteLine(JsonConvert.SerializeObject(player, Formatting.Indented));
            //await UpdatePlayerAsync(player);
            ////Console.WriteLine(JsonConvert.SerializeObject(player, Formatting.Indented));
            //map1.GetUnit(player.currentX, player.currentY).TakeUnit(player);
            //player.currentX = player.currentX;
            //player.currentY = player.currentY;

            //Sitas tris eilutes uzkomentuot jei multi
            map1.GetUnit(0, 0).TakeUnit(player);
            player.currentX = 0;
            player.currentY = 0;
            while (turnLimit > 0)
            {
                ((Teleport)teleport).SetStartingPosition(player.currentX, player.currentY);
                bool finishedIteration = false;
                while (!finishedIteration)
                {
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

                        bool succesfulMove = true;
                        if (n != player.NumberOfActions)
                        {
                            while (succesfulMove)
                            {

                                if (!(player.getAlgorithm() is Teleport))
                                {
                                    Console.WriteLine("Choose where to go next R,L,U,D?");
                                    command = Console.ReadLine();

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
                                else
                                {
                                    Console.WriteLine("Choose where to go next, type in two numbers with a space between them");
                                    command = Console.ReadLine();

                                    string[] numbers = command.Split(' ');
                                    int XPosition = 0;
                                    int YPosition = 0;
                                    
                                    bool number1Success = Int32.TryParse(numbers[0], out XPosition);
                                    bool number2Success = Int32.TryParse(numbers[1], out YPosition);

                                    if(number1Success && number1Success && XPosition < 20 && YPosition < 20 && XPosition >= 0 && YPosition >= 0 && Map.GetInstance.GetUnit(XPosition, YPosition).symbol.Equals('0'))
                                    {
                                        player.ExecuteMove(command, player);
                                        succesfulMove = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Your number where wrong or you are teleporting on an obstacle");
                                    }
                                }
                            }
                            succesfulMove = true;
                        }

                    }
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Would you like to undo your moves? (Yes/No)");
                    command = Console.ReadLine();
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
                    n = 0;
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

                if (player.getAlgorithm() is Tower)
                    ((Tower)player.getAlgorithm()).ResetStartingList();

                //For multi
                //gs = await UpdateMap(player.id, Map.GetInstance.ConvertArrayToList());
                //while (gs.StateGame == "Updating")
                //{
                //    gs = await UpdateMap(player.id, Map.GetInstance.ConvertArrayToList());
                //}
                ////Console.WriteLine(JsonConvert.SerializeObject(player, Formatting.Indented));
                //serverMap = await GetMap(player.id);
                //Map.GetInstance.ConvertListToArray(serverMap);
                //p = await GetPlayerAsync(url.PathAndQuery);
                //player.MoneyMultiplier = p.MoneyMultiplier;
                //player.NumberOfActions = p.NumberOfActions;
                ////Console.WriteLine(JsonConvert.SerializeObject(player, Formatting.Indented));

                turnLimit--;
               
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
        static async Task<Player> GetPlayerAsync(string path)
        {
            Player player = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                player = await response.Content.ReadAsAsync<Player>();
            }
            return player;
        }

        static async Task<List<Unit>> GetMap(long id)
        {
            List<Unit> map = null;
            HttpResponseMessage response = await client.GetAsync(gmRequestUri + $"{id}");
            if (response.IsSuccessStatusCode)
            {
                map = await response.Content.ReadAsAsync<List<Unit>>();
            }
            return map;
        }

        static async Task<HttpStatusCode> UpdatePlayerAsync(Player player)
        {
            Console.WriteLine(client + requestUri + $"{player.id}");
            HttpResponseMessage response = await client.PutAsJsonAsync(
                requestUri + $"{player.id}", player);
            response.EnsureSuccessStatusCode();

            //return response.StatusCode;
            return response.StatusCode;
        }

        static async Task<GameState> UpdateMap(long id, List<Unit> map)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                gmRequestUri + $"{id}", map);
            response.EnsureSuccessStatusCode();

            GameState gs = await response.Content.ReadAsAsync<GameState>();

            return gs;
        }
    }
}
