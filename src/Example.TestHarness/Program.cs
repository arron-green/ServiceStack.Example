using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Api.Messaging;
using ServiceStack.ServiceClient.Web;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new JsonServiceClient("http://localhost:53353"){UserName = "user1", Password = "password"};
            client.SendAsync(new AwardRequest(), 
                r => Console.WriteLine("Response: " + r.IsSuccess), 
                (r, ex) => Console.WriteLine(ex.Message));

            Console.ReadLine();
        }
    }
}
