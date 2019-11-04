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
using Newtonsoft.Json;
using Lab1_1.Facade;

namespace Lab1_1
{
    class Program
    {
        private static string requestUri = "/api/player/";
        private static string gmRequestUri = "/api/gamecontroller/";
        static string mediaType = "application/json";

        public static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            int turnLimit = 4;

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);

            client.BaseAddress = new Uri("https://localhost:44397/"); //api /player/");
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(mediaType));

            GameManager gameManager = new GameManager(client);

            Console.WriteLine("Welcome to splash Wars!");
            Console.WriteLine("Enter map's size on X axis");
            int xSize = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter map's size on Y axis");
            int ySize = int.Parse(Console.ReadLine());
            //-----
            Map.GetInstance.GenerateGrid(xSize, ySize);         //Čia turėtų būti gameManager.GenerateGrid(xSize, ySize);
            //-----

            //For multi
            //List<Unit> serverMap = await GetMap(1);
            //Map.GetInstance.ConvertListToArray(serverMap);


            string command = "";
            Console.WriteLine("Welcome to splash Wars!");
            Console.WriteLine("Enter player's name to start looking for a loby (you will be added to a lobby automatically)");
            command = Console.ReadLine();

            //     For multi
            //gameManager.player.SetName(command);
            //var url = gameManager.CreatePlayer(player);

            //Console.Write("\r{0}%   ", gameManager.LobbyIsFull());

            //Player p = await gameManager.GetPlayer(url.PathAndQuery);

            string json = JsonConvert.SerializeObject(gameManager.player, Formatting.Indented);
            //Console.WriteLine(json);

            //-----
            //Šitie kintamieji turėtų būti perkelti į GameManager
            Algorithm standart = gameManager.algorithmFactory.GetDefault("Standart");
            Algorithm hopper = gameManager.algorithmFactory.GetDefault("Hopper");
            Algorithm tower = gameManager.algorithmFactory.GetDefault("Tower");
            Algorithm teleport = gameManager.algorithmFactory.GetDefault("Teleport");
            //-----


            Console.WriteLine("choose your faction:");
            Console.WriteLine("Wolfs - they get an extra action each turn");
            Console.WriteLine("Hunter - they start with extra money");
            Console.WriteLine("Hard worker - you get a small increase in actions each turn and a little bit of money");
            command = Console.ReadLine();

            gameManager.CreatePlayerWithFaction(command, standart);

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
            Map.GetInstance.GetUnit(0, 0).TakeUnit(gameManager.player);
            gameManager.player.currentX = 0;
            gameManager.player.currentY = 0;

            while (turnLimit > 0)
            {
                ((Teleport)teleport).SetStartingPosition(gameManager.player.currentX, gameManager.player.currentY);
                bool finishedIteration = false;
                while (!finishedIteration)
                {
                    while (n < gameManager.player.NumberOfActions)
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
                        if (n != gameManager.player.NumberOfActions)
                        {
                            while (succesfulMove)
                            {

                                if (!(gameManager.player.getAlgorithm() is Teleport))
                                {
                                    Console.WriteLine("Choose where to go next R,L,U,D?");
                                    command = Console.ReadLine();

                                    gameManager.MovePlayer(command, ref succesfulMove);

                                }
                                else
                                {
                                    Console.WriteLine("Choose where to go next, type in two numbers with a space between them");
                                    command = Console.ReadLine();


                                    gameManager.MovePlayerNext(command, ref succesfulMove);

                                }
                            }
                            succesfulMove = true;
                        }

                    }
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Would you like to undo your moves? (Yes/No)");
                    command = Console.ReadLine();

                    gameManager.Undo(command, ref finishedIteration);
                    n = 0;
                }
            

                Console.WriteLine("You have " + gameManager.player.Money + " Money");
                Console.WriteLine("Do you want to move like:");
                Console.WriteLine("Tower - go till the end of the line");
                Console.WriteLine("Hopper - jump over a space");
                Console.WriteLine("Teleport - write two digits and teleport on those coordinates");
                Console.WriteLine("Standart - go one space in what direction you want");
                command = Console.ReadLine();
                if (command.Equals("Tower"))
                    gameManager.player.setAlgorithm(tower);
                else if(command.Equals("Hopper"))
                    gameManager.player.setAlgorithm(hopper);
                else if (command.Equals("Teleport"))
                    gameManager.player.setAlgorithm(teleport);
                else 
                    gameManager.player.setAlgorithm(standart);

                
                if (gameManager.player is HardWorker)
                {
                    Console.WriteLine("As a Hard worker you can work harder and get more money per action( double), but have less actions per round( 6 actions)");
                    if(gameManager.player.MoneyMultiplier == 1)
                        Console.WriteLine("Type 'Work' to do it!");
                    else
                        Console.WriteLine("Type 'Stop' to go back to normal mode");
                    command = Console.ReadLine();

                    if(command.Equals("Work"))
                        ((HardWorker)gameManager.player).WorkHarder();
                    else if(command.Equals("Stop"))
                        ((HardWorker)gameManager.player).GetBackToNormal();
                }
                else if(gameManager.player is Wolf)
                {
                    if (((Wolf)gameManager.player).GetAttackLimit() != 0)
                    {
                        Console.WriteLine("As a Wolf you can attack an area and capture it");
                        Console.WriteLine("If you want to attack, type a direction: R,L,U,D");
                        command = Console.ReadLine();
                        ((Wolf)gameManager.player).AttackASpecificArea(gameManager.player, command, Map.GetInstance);

                    }
                }
                else
                {

                }

                if (gameManager.player.getAlgorithm() is Tower)
                    ((Tower)gameManager.player.getAlgorithm()).ResetStartingList();

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
