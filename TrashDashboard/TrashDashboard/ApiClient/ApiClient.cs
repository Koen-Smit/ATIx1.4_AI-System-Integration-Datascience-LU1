using System.Net.Http.Headers;
using System.Text.Json;
using TrashDashboard.Models;

namespace TrashDashboard.ApiClient
{
    public class ApiClient 
    {
        private static string apiBaseUrl = "https://localhost:7237/trash";
        private static string bearerToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0aW5nQHRlc3RpbmcubmwiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdGluZyIsImV4cCI6MTc0OTg5NTE2OCwiaXNzIjoiTW9uaXRvcmluZ0FQSSIsImF1ZCI6Ik1vbml0b3JpbmdBUEkifQ.if0DSV-gbt715wDdaW9EavH0E-XoE6Eu1MMr046-eJY";

        public static async Task<Trash> ApiCall(string endpoint)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", bearerToken);

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiBaseUrl + endpoint);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize into Trash object
                Trash trash = JsonSerializer.Deserialize<Trash>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Console.WriteLine($"ID: {trash.Id}, Name: {trash.CameraId}, Date: {trash.DateCollected}");
                return trash;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return new Trash() { DagCategorie = e.Message};
            }
        }

        public static async Task<List<Trash>> GetAllTrash()
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", bearerToken);

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiBaseUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize JSON array into List<Trash>
                List<Trash> trashList = JsonSerializer.Deserialize<List<Trash>>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return trashList;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return new List<Trash>(); // or null if you prefer
            }
        }


    }
}
