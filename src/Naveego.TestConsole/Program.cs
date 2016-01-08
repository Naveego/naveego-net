using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naveego.Sync;

namespace Naveego.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var apiClient = new ApiClient();

            apiClient.ApiUrl = "http://deploy.naveegoapi.com/v3";
            apiClient.GetDeploymentSettings("234");

            Console.ReadLine();

        }
    }
}
