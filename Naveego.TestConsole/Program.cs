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
            var connections = apiClient.GetConnections();

            Console.ReadLine();

        }
    }
}
