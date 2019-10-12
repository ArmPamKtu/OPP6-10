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


            string command = "";
            Console.WriteLine("Welcome to splash Wars!");
            Console.WriteLine("Press S to start looking for a loby( you will be added to a lobby automatically)");
            command = Console.ReadLine();
          

            Map map1 = Map.GetInstance;
            Map map2 = Map.GetInstance;

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri("https://localhost:44371/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

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
            map[player.currentY][player.currentX] = 1;

            while (turnLimit > 0)
            {

                while (n < 5)
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
                    if (n != 5)
                    {
                        Console.WriteLine("Choose where to go next R,L,U,D?");
                        command = Console.ReadLine();
                        player.move(player, command, map);
                    }

                }
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
                else if (command.Equals("Standart"))
                    player.setAlgorithm(standart);
                turnLimit--;
                n = 0;
            }
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
               /* // Create a new product
                Product product = new Product
                {
                    Name = "Gizmo",
                    Price = 100,
                    Category = "Widgets"
                };
                
                var url =  CreateProductAsync(product);
                Console.WriteLine($"Created at {url}");*/

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
