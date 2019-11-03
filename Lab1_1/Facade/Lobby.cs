using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_1.Facade
{
    public class Lobby
    {
        private static HttpClient client = new HttpClient();
        private static string requestUri = "/api/player/";
        public int maxLobbyPlayers { get; set; }
        public async Task<bool> LobbyIsFullAsync()
        {
            ICollection<Player> playersInLobby = await GetAllPlayersAsync(client.BaseAddress.PathAndQuery);

            if (playersInLobby.Count < maxLobbyPlayers)
                return false;

            return true;
        }
        private async Task<ICollection<Player>> GetAllPlayersAsync(string path)
        {
            ICollection<Player> players = null;
            HttpResponseMessage response = await client.GetAsync(path + requestUri);
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<ICollection<Player>>();
            }
            return players;
        }
        static void ShowPlayer(Player player)
        {
            Console.WriteLine("Player name: " + player.Name);
        }
        public async Task<Uri> CreatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                requestUri, player);
            response.EnsureSuccessStatusCode();

            Player player2 = await response.Content.ReadAsAsync<Player>();
            if (player2 != null)
            {
                ShowPlayer(player2);
            }

            return response.Headers.Location;
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

    }
}
