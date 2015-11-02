using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naveego.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var apiClient = new ApiClient();

            apiClient.ApiUrl = "https://sbti.naveegoapi.com/v3";
            apiClient.Login("admin", "sbti123");
            var connections = apiClient.GetConnections();

            Console.ReadLine();

        }
    }
}
