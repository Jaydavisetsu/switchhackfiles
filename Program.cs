using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
//using ConsumingAPIs;


namespace Lab4_API_Jayden
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            await ZeldaCall();

            using var httpClient = new HttpClient();

            string apiUrl = $"https://zelda.fanapis.com/api/games?name";


            // Display a menu to look up games
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Search for a game by name");
                Console.WriteLine("2. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        Console.Write("Enter the name of the game: ");
                        string gameName = Console.ReadLine();

                        try
                        {
                            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                            if (response.IsSuccessStatusCode)
                            {
                                string content = await response.Content.ReadAsStringAsync();
                                Console.WriteLine(content);

                                Console.WriteLine("\n\n\n");

                                dynamic item = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                                // Console.WriteLine($"Game Searched: {item.name}");
                            }

                            /*   string jsonSerializedData = "{\"name\": \"Value1\", \"developer\": \"Value2\"}";
                               ZeldaData zeldaData = ZeldaData.DeserializeFromJson(jsonSerializedData);

                               if (zeldaData != null)
                               {
                                   // Now, 'zeldaData' contains the deserialized object
                                   Console.WriteLine("Title: " + zeldaData.Name);
                                   Console.WriteLine("Dev(s): " + zeldaData.Developer);
                               }*/
                            else
                            {
                                Console.WriteLine($"Error : {response.StatusCode}");
                            }
                        }
                        catch (HttpRequestException ex)
                        {
                            Console.WriteLine($"HTTP Request error: {ex.Message}");
                        }
                        break;

                    case "2":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(content);

                        Console.WriteLine("\n\n\n");

                        dynamic item = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                        //Console.WriteLine($"https://zelda.fanapis.com/api/games?name");
                    }
                    else
                    {
                        Console.WriteLine($"Error : {response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP Request error: {ex.Message}");
                }


            }
        }

        /*     static void SearchGameByName(List<ZeldaData> games, string name)
             {
                 foreach (var game in games)
                 {
                     if (game.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                     {
                         Console.WriteLine("Game Name: " + game.Name);
                         Console.WriteLine("Description: " + game.Description);
                         Console.WriteLine("Developer: " + game.Developer);
                         Console.WriteLine("Publisher: " + game.Publisher);
                         Console.WriteLine("Released Date: " + game.ReleasedDate.ToShortDateString());
                         Console.WriteLine();
                         return;
                     }
                 }

                 Console.WriteLine("Game not found.");
             }*/

        public static async Task ZeldaCall()
        {
            /*        https://zelda.fanapis.com/api/games?name

                        var client = new HttpClient();

                        HttpResponseMessage response = await client.GetAsync("https://zelda.fanapis.com/api/games?name");

                        string json = await response.Content.ReadAsStringAsync();

                        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };


                        ZeldaData? p = JsonSerializer.Deserialize<ZeldaData?>(options);
                        ZeldaData? p = JsonSerializer.Deserialize<ZeldaData?>(json);
                        Console.WriteLine(json + "\n"); */

            var client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://zelda.fanapis.com/api/games?name");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                    // Deserialize the JSON string into a ZeldaData object
                    ZeldaData? zeldaData = JsonSerializer.Deserialize<ZeldaData?>(json, options);

                    if (zeldaData != null)
                    {
                        Console.WriteLine($"Name: {zeldaData.Name}");
                        // Access other properties as needed
                    }
                }
                else
                {
                    Console.WriteLine("Failed to retrieve data from the API.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
    
}

           