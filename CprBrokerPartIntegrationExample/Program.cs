using ServiceReference1;
using System;

namespace CprBrokerPartIntegrationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string input = args[0];
                PartSoap12Client client = new PartSoap12Client();

                ApplicationHeader appHeader = new ApplicationHeader();
                appHeader.ApplicationToken = "656d476b-6af4-43c8-955a-a498e8903aff";
                appHeader.UserToken = Environment.UserName;

                var uuidResult = client.GetUuidAsync(appHeader, input);
                string uuid = uuidResult.Result.GetUuidOutput.UUID;
                Console.WriteLine(uuid);
                Console.WriteLine(appHeader.UserToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}
