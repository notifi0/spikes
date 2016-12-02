using GitHubPoller.Domain;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubPoller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var client = new RestClient("https://api.github.com");
            client.AddDefaultHeader("Accept", "application/vnd.github.v3+json");
            client.AddDefaultHeader("User-Agent", "gretro");

            var request = new RestRequest("/users/{userid}", Method.GET);
            request.AddUrlSegment("userid", "gretro");

            var response = client.Execute<User>(request);
            var user = response.Data;
            Console.WriteLine(response.Content);

            var asyncResponse = client.ExecuteWithTaskAsync(request);
            asyncResponse.Wait();
            Console.WriteLine("Async response received");
            Console.ReadKey();
        }
    }
}
