using ServiceReference1;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace CprBrokerPartIntegrationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string inputCprNo = args[0];
                string inputAppToken = args[1];
                string endpointAddress = args[2];

                PartSoap12Client client = new PartSoap12Client(
                    Program.GetCustomHttpsBinding(),
                    new EndpointAddress(endpointAddress)
                    );

                // Conf. SOAP header
                ApplicationHeader appHeader = new ApplicationHeader();
                appHeader.ApplicationToken = inputAppToken;
                appHeader.UserToken = Environment.UserName;

                // Call Part.GetUuid
                var uuidResult = client.GetUuidAsync(appHeader, inputCprNo);
                string uuid = uuidResult.Result.GetUuidOutput.UUID;

                Console.WriteLine(string.Format("Part.GetUuid output: {0}", uuid));

                // Additional configuration of SOAP Envelope header for Part.Read
                SourceUsageOrderHeader suoh = new SourceUsageOrderHeader();
                suoh.SourceUsageOrder = SourceUsageOrder.LocalThenExternal;

                LaesInputType lit = new LaesInputType();
                lit.UUID = uuid;

                // Call Part.Read
                var readResult = client.ReadAsync(appHeader, suoh, lit);

                // Serializing response, and extracting name element...?
                var regType1 = readResult.Result.LaesOutput.LaesResultat.Item as RegistreringType1;
                var personNameStructure = regType1.AttributListe.Egenskab[0].NavnStruktur.PersonNameStructure;

                string person = string.Format("Part.Read output: {0} {1}",
                    personNameStructure.PersonGivenName,
                    //personNameStructure.PersonMiddleName,
                    personNameStructure.PersonSurnameName
                );

                Console.WriteLine(person);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("\n{0}\n", ex.ToString()));
            }
        }

        public static CustomBinding GetCustomHttpsBinding()
        {
            CustomBinding binding = new CustomBinding();

            TextMessageEncodingBindingElement textBindingElement = new TextMessageEncodingBindingElement();
            textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
            binding.Elements.Add(textBindingElement);

            HttpsTransportBindingElement httpsBindingElement = new HttpsTransportBindingElement
            {
                AllowCookies = true,
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
                AuthenticationScheme = System.Net.AuthenticationSchemes.Negotiate
            };
            binding.Elements.Add(httpsBindingElement);

            return binding;
        }
    }
}
