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
        static async Task Main(string[] args)
        {
            int turnLimit = 4;

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);

            client.BaseAddress = new Uri("https://localhost:44372/"); //api /player/");
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(mediaType));

            GameManager gameManager = new GameManager(client);
           

            Console.WriteLine("Welcome to splash Wars!");
            Console.WriteLine("Enter map's size on X axis");
            int xSize = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter map's size on Y axis");
            int ySize = int.Parse(Console.ReadLine());

            gameManager.generateGrid(xSize, ySize);

            //For multi
            List<Unit> serverMap = await gameManager.GetPlayerMap(1);
            Map.GetInstance.ConvertListToArray(serverMap);


            string command = "";
            Console.WriteLine("Welcome to splash Wars!");
            Console.WriteLine("Enter player's name to start looking for a loby (you will be added to a lobby automatically)");
            command = Console.ReadLine();

            //     For multi
            gameManager.player.SetName(command);
            Uri url = await gameManager.CreatePlayerAsync(gameManager.player);

            Console.Write("\r{0}%   ", gameManager.LobbyIsFull());

            Player p = await gameManager.GetPlayer(url.PathAndQuery);

            string json = JsonConvert.SerializeObject(gameManager.player, Formatting.Indented);
            //Console.WriteLine(json);


            Console.WriteLine("choose your faction:");
            Console.WriteLine("Wolfs - they get an extra action each turn");
            Console.WriteLine("Hunter - they start with extra money");
            Console.WriteLine("Hard worker - you get a small increase in actions each turn and a little bit of money");
            command = Console.ReadLine();

            gameManager.CreatePlayerWithFaction(command, gameManager.standart);

            int n = 0;

            //For multi 
            gameManager.player.id = p.id;
            gameManager.player.currentX = p.currentX;
            gameManager.player.currentY = p.currentY;
            gameManager.player.color = p.color;
            //Console.WriteLine(JsonConvert.SerializeObject(player, Formatting.Indented));
            await UpdatePlayerAsync(gameManager.player);
            //Console.WriteLine(JsonConvert.SerializeObject(player, Formatting.Indented));
            gameManager.GetMap().GetUnit(gameManager.player.currentX, gameManager.player.currentY).TakeUnit(gameManager.player);
            gameManager.player.currentX = gameManager.player.currentX;
            gameManager.player.currentY = gameManager.player.currentY;

            //Sitas tris eilutes uzkomentuot jei multi
            //gameManager.TakeUnit();
            //gameManager.player.currentX = 0;
            //gameManager.player.currentY = 0;
            
            gameManager.makeMoves(serverMap, p, url);
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
    }
}
