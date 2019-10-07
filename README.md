# CprBrokerPartIntegrationExample

> Autogenerated .NET Core PartSoap12Client using CPRBroker's Part WSDL located in:  "CprBrokerPartIntegrationExample\CprBrokerPartIntegrationExample\wsdl\".

### How to
1) Download project
2) Open project in Visual Studio.
3) Build project.
3.1) From here you can run the program locally from project from "CprBrokerPartIntegrationExample\CprBrokerPartIntegrationExample\bin\Debug\netcoreapp2.1\". 
3.2) Publish project if you need to move the program elsewhere, e.g. to a remote server. Remember that you need to install the respective version of .NET Core runtime. output folder: "CprBrokerPartIntegrationExample\CprBrokerPartIntegrationExample\bin\Debug\netcoreapp2.1\publish\".
4) Executing the program, by changing dir to the publish folder, and then run:
> dotnet .\CprBrokerPartIntegrationExample.dll \<CprNo\> \<CprBrokerApplicationToken\>

Example:
> dotnet .\CprBrokerPartIntegrationExample.dll 0123456789 a5c11e37-2b00-4368-a58b-55c540aa00a9
