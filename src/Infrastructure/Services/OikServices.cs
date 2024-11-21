using System;
using System.Linq;
using System.Threading.Tasks;
using Iface.Oik.Tm.Helpers;
using Iface.Oik.Tm.Interfaces;
using OikClientDraft.Infrastructure.Util;

namespace OikClientDraft.Infrastructure.Services;

public class ServerService : CommonServerService
{
}


public class Infrastructure : CommonInfrastructure
{
  public Infrastructure(IOikDataApi          api,
                        ITmsApi              tms,
                        IOikSqlApi           sql,
                        ICommonServerService serverService)
    : base(api, tms, sql, serverService)
  {
  }


  public override ICommonOikSqlConnection CreateOikSqlConnection()
  {
    return new CommonOikSqlConnection(
      $"Host=127.0.0.1;Port={RbPort};Database=oikdb;Timeout=10;Username=postgres;SSL Mode=Disable;");
  }
}


public static class OikHelper
{
  public static async Task ConnectToServer()
  {
    var commandLineArgs = Environment.GetCommandLineArgs();

    var host     = commandLineArgs.ElementAtOrDefault(1) ?? Vars.DefaultOikHost;
    var user     = commandLineArgs.ElementAtOrDefault(2) ?? Vars.DefaultOikUser;
    var password = commandLineArgs.ElementAtOrDefault(3) ?? Vars.DefaultOikPassword;
    
    Tms.InitNativeLibraryAndIgnoreLinuxSignals();

    Tms.SetUserCredentials(user, password);

    var tmCid = Tms.Connect(host, "TMS", Vars.TraceName, null, IntPtr.Zero, returnCidAnyway: true);
    if (!Tms.IsConnected(tmCid))
    {
      var errorMessage = Tms.GetConnectionErrorText(tmCid);
      Tms.Disconnect(tmCid);
      throw new Exception($"Ошибка связи с TM-сервером: {errorMessage}");
    }
    if (Tms.IsServerInPassiveMode(tmCid))
    {
      Tms.Disconnect(tmCid);
      throw new Exception("Сервер находится в пассивном режиме");
    }

    var rbCid = Tms.Connect(host, "RBS", Vars.TraceName, Tms.EmptyTmCallbackDelegate, IntPtr.Zero);
    if (rbCid == 0)
    {
      Tms.Disconnect(tmCid);
      throw new Exception($"Ошибка связи с RB-сервером: {Tms.GetLastError().ToString()}");
    }

    var rbPort = Tms.OpenSqlRedirector(rbCid);
    if (rbPort == 0)
    {
      Tms.Disconnect(tmCid);
      Tms.Disconnect(rbCid);
      throw new Exception("Редиректор на базу данных не открыт");
    }
    
    var userInfo       = Tms.GetUserInfo(tmCid, "TMS");
    var serverFeatures = Tms.GetTmServerFeatures(tmCid);

    var infrastructure = IoC.Get<ICommonInfrastructure>();
    
    infrastructure.InitializeTm(tmCid, rbCid, rbPort, userInfo, serverFeatures);
    await infrastructure.ServerService.StartAsync();
  }


  public static async Task DisconnectFromServer()
  {
    var infrastructure = IoC.Get<ICommonInfrastructure>();
    
    await infrastructure.ServerService.StopAsync();

    Tms.Terminate(infrastructure.TmCid, infrastructure.RbCid);
    infrastructure.TerminateTm();
  }
}
