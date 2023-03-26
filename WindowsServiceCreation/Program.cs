// See https://aka.ms/new-console-template for more information

using System.ServiceProcess;
using WindowsServiceCreation;

using (var service = new FileWriteService())
{
    ServiceBase.Run(service);
    //service.OnDebug();
}