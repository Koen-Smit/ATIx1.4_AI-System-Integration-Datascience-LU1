using System.Net.Http.Headers;
using System.Text.Json;
using TrashDashboard.Models;

namespace TrashDashboard.ApiClient
{
    public class ApiClient
    {
        private static string apiBaseUrl = "https://avansict2227609.azurewebsites.net/trash";

        private readonly Authorization authorization;
        private readonly HttpClient httpClient;

        public ApiClient(HttpClient http, Authorization auth)
        {
            httpClient = http;
            authorization = auth;
        }

        public async Task<Trash> ApiCall(string endpoint)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authorization.Token);

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiBaseUrl + endpoint);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize into Trash object
                Trash? trash = JsonSerializer.Deserialize<Trash>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (trash == null)
                {
                    Console.WriteLine("Deserialization returned null.");
                    return new Trash();
                }

                Console.WriteLine($"ID: {trash.Id}, Name: {trash.CameraId}, Date: {trash.DateCollected}");
                return trash;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return new Trash() { DagCategorie = e.Message };
            }
        }

        public async Task<List<Trash>> GetAllTrash()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization.Token);

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiBaseUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize JSON array into List<Trash>
                List<Trash> trashList = JsonSerializer.Deserialize<List<Trash>>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Trash>();

                return trashList;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return new List<Trash>();
            }
        }


    }
}
