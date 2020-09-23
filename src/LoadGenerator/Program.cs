using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoadGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Load generator for WebApi Starting");

            string apiBaseUrl = Environment.GetEnvironmentVariable("APIBASEURL") ?? "http://localhost:5000";
            int delay = 100;
            
            while(true)
            {
                var httpClientHandler = new HttpClientHandler();
                
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    try
                    {
                        var response = await client.GetAsync($"{apiBaseUrl}/weatherforecast");

                        if(response.IsSuccessStatusCode){
                            Console.WriteLine($"Response from API: {response}");
                        }
                        else{
                            Console.WriteLine("Something went wrong with the API");
                        }
                            
                    }
                    catch(Exception e){
                        Console.WriteLine("Something went wrong with the API");
                    }

                    await Task.Delay(delay);
                }
                
            }
        }
    }
}
