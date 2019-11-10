using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using System.Diagnostics;
using WindowsFormsApp2.DecoratorPattern;
using Lab1_1.Facade;
using Lab1_1.Streategy;
using Lab1_1.Observer;
using Lab1_1;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public static int mapSize = 10;
        public static int border = 4;
        public static int titleSizeX = 100;
        public static int titleSizeY = 100;

        public int maxLobbyPlayers = 2;
        public int turnLimit = 4;
        public int n = 0;
        public string command;
        public bool succesfulMove = true;
        public bool finishedIteration = false;
        public bool activeButton = true;
        public Map map;
        List<Unit> serverMap;
        Player p;
        public Uri url;

        public int offsetX = 0;
        public int offsetY = 0;

        Bitmap bm;
        Graphics gfx;

        private static string requestUri = "/api/player/";
        private static string gmRequestUri = "/api/gamecontroller/";
        static string mediaType = "application/json";

        public static HttpClient client = new HttpClient();

        public GameManager gameManager = new GameManager(client);

        public Form1()
        {
            InitializeComponent();
        }

        private async Task Init()
        {
            bm = new Bitmap(mapSize * titleSizeX, mapSize * titleSizeY);
            gfx = Graphics.FromImage(bm);

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri("http://localhost:54084/"); //api /player/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(mediaType));

            gameManager = new GameManager(client);
            gameManager.generateGrid(mapSize, mapSize);

            //For multi
            //serverMap = await GetMap(1);
            //Map.GetInstance.ConvertListToArray(serverMap);

            Debug.WriteLine("Welcome to splash Wars!");
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await Init();
            RenderMap();
        }

        private void RenderMap()
        {
            Cell c = new Cell();
            List<TextureBrush> tb= new List<TextureBrush>();

            tb.Add((TextureBrush)new GrassTile(c).GetBrush().Clone());
            tb.Add((TextureBrush)new YellowTile(c).GetBrush().Clone());
            tb.Add((TextureBrush)new YellowDot(c).GetBrush().Clone());
            tb.Add((TextureBrush)new StoneTile(c).GetBrush().Clone());
            tb.Add((TextureBrush)new WonderTile(c).GetBrush().Clone());
            tb.Add((TextureBrush)new GoldMineTile(c).GetBrush().Clone());
            tb.Add((TextureBrush)new BlueTile(c).GetBrush().Clone());
            tb.Add((TextureBrush)new BlueDot(c).GetBrush().Clone());

            gfx.FillRectangle(Brushes.Black, 0, 0, mapSize * titleSizeX, mapSize * titleSizeY + 100);
            for (int x = 0; x < gameManager.GetYSize(); x++)
            {
                for (int y = 0; y < gameManager.GetXSize(); y++)
                {
                    TextureBrush tBrush = GetBrush(gameManager.GetColor(x, y), gameManager.GetSymbol(x, y), tb);
                    tBrush.Transform = new System.Drawing.Drawing2D.Matrix(
                        100.0f / 512.0f,
                        0.0f,
                        0.0f,
                        100.0f / 512.0f,
                        0.0f,
                        0.0f);
                    gfx.FillRectangle(tBrush, 0 + offsetX + border, 0 + offsetY + border, titleSizeX - 5, titleSizeY - 5);

                    offsetY += titleSizeY;
                }
                offsetY = 0;
                offsetX += titleSizeX;
            }
            offsetX = 0;
            pictureBox1.Image = bm;

            //for (int y = 0; y < gameManager.GetYSize(); y++)
            //{
            //    for (int x = 0; x < gameManager.GetXSize(); x++)
            //    {
            //        Debug.Write(gameManager.GetSymbol(x, y));
            //    }
            //    Debug.WriteLine("");
            //}
        }

        private async void Move(string direction)
        {
            if (n == 0 && finishedIteration)
            {
                Debug.WriteLine("Turn count " + turnLimit);
                finishedIteration = false;
            }

            if (turnLimit > 0)
            {
                ((Teleport)gameManager.teleport).SetStartingPosition(gameManager.player.currentX, gameManager.player.currentY);
                if (!finishedIteration)
                {
                    if (n < gameManager.player.NumberOfActions)
                    {
                        n++;

                        gameManager.MovePlayer(direction, ref succesfulMove);
                        RenderMap();
                    }
                    else
                    {
                        n = 0;

                    }
                    if (n == 0)
                    {
                        textBox3.Enabled = true;
                        button2.Enabled = true;
                        activeButton = false;

                        Debug.WriteLine("\n\n");
                        Debug.WriteLine("Would you like to undo your moves? (Yes/No)");
                    }
                }
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (activeButton)
            {
                if (e.KeyCode == Keys.A)
                {
                    Move("L");
                }
                if (e.KeyCode == Keys.D)
                {
                    Move("R");
                }
                if (e.KeyCode == Keys.W)
                {
                    Move("U");
                }
                if (e.KeyCode == Keys.S)
                {
                    Move("D");
                }
            }
        }

        private TextureBrush GetBrush(ConsoleColor n, char symbol, List<TextureBrush> tb)
        {
            switch (symbol)
            {
                case '*':
                    if (n == (ConsoleColor)14)
                    {
                        return tb[2];
                    }
                    else if (n == (ConsoleColor)9)
                    {
                        return tb[7];
                    }
                    return tb[0];
                case 'G':
                    return tb[5];
                case 'S':
                    return tb[3];
                case 'W':
                    return tb[4];
                case '0':
                    if (n == (ConsoleColor)14)
                    {
                        return tb[1];
                    }
                    else if (n == (ConsoleColor)9)
                    {
                        return tb[6];
                    }
                    else if (n == (ConsoleColor)15)
                    {
                        return tb[0];
                    }
                    return tb[0];
                default:
                    return tb[0];
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string playerName = textBox1.Text;
            string faction = textBox2.Text;
            textBox1.Enabled = false;
            textBox2.Enabled = false;

            //For multi
            //gameManager.player.SetName(playerName);
            //url = await CreatePlayerAsync(gameManager.player);

            gameManager.CreatePlayerWithFaction(faction, gameManager.standart);

            //For multi
            //Player p = await gameManager.GetPlayer(url.PathAndQuery);
            //gameManager.player.id = p.id;
            //gameManager.player.currentX = p.currentX;
            //gameManager.player.currentY = p.currentY;
            //gameManager.player.color = p.color;
            ////Console.WriteLine(JsonConvert.SerializeObject(player, Formatting.Indented));
            //await UpdatePlayerAsync(gameManager.player);
            ////Console.WriteLine(JsonConvert.SerializeObject(player, Formatting.Indented));
            //gameManager.GetMap().GetUnit(gameManager.player.currentX, gameManager.player.currentY).TakeUnit(gameManager.player);
            //gameManager.player.currentX = gameManager.player.currentX;
            //gameManager.player.currentY = gameManager.player.currentY;
            //ICollection<Player> playersInLobby = await GetAllPlayersAsync();
            //while (playersInLobby.Count < maxLobbyPlayers)
            //{
            //    playersInLobby = await GetAllPlayersAsync();
            //}

            //For normal
            gameManager.CreatePlayerWithFaction(faction, gameManager.standart);
            gameManager.TakeUnit();
            gameManager.player.currentX = 0;
            gameManager.player.currentY = 0;


            RenderMap();           
            button1.Enabled = false;
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            command = textBox3.Text;
            gameManager.Undo(command, ref finishedIteration);
            ((Teleport)gameManager.teleport).SetStartingPosition(gameManager.player.currentX, gameManager.player.currentY);
            RenderMap();

            if (finishedIteration)
            {
                //For multi
                //GameState gs = gameManager.GetGameState();
                //gs = await UpdateMap(gameManager.player.id, Map.GetInstance.ConvertArrayToList());
                //while (gs.StateGame == "Updating")
                //{
                //    gs = await UpdateMap(gameManager.player.id, Map.GetInstance.ConvertArrayToList());
                //}
                ////Console.WriteLine(JsonConvert.SerializeObject(player, Formatting.Indented));
                //serverMap = await GetMap(gameManager.player.id);
                //Map.GetInstance.ConvertListToArray(serverMap);
                //p = await gameManager.GetLobby().GetPlayerAsync(url.PathAndQuery);
                //gameManager.player.MoneyMultiplier = p.MoneyMultiplier;
                //gameManager.player.NumberOfActions = p.NumberOfActions;
                //RenderMap();
                ////Console.WriteLine(JsonConvert.SerializeObject(player, Formatting.Indented));

                turnLimit--;
            }

            textBox3.Enabled = false;
            button2.Enabled = false;
            activeButton = true;
        }

        //--------

        public async Task<ICollection<Player>> GetAllPlayersAsync()
        {
            ICollection<Player> players = null;
            HttpResponseMessage response = await client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<ICollection<Player>>();
            }
            return players;
        }

        public async Task<Player> GetPlayerAsync(string path)
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

        public async Task<Uri> CreatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                requestUri, player);
            response.EnsureSuccessStatusCode();

            Player player2 = await response.Content.ReadAsAsync<Player>();

            return response.Headers.Location;
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
