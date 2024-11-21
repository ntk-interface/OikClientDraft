using System.Text;
using Iface.Oik.Tm.Api;
using Iface.Oik.Tm.Interfaces;
using Iface.Oik.Tm.Native.Api;
using Iface.Oik.Tm.Native.Interfaces;
using OikClientDraft.Infrastructure.Services;
using OikClientDraft.Infrastructure.Util;
using Splat;

namespace OikClientDraft;

public class Bootstrapper
{
  public static void Load()
  {
    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // требуется для работы с кодировкой Win-1251

    RegisterServices();
  }


  private static void RegisterServices()
  {
    IoC.Initialize();
    IoC.RegisterAllViewModels();
    IoC.RegisterAllViews();
    
    RegisterOikServices();
  }


  private static void RegisterOikServices()
  {
    SplatRegistrations.RegisterLazySingleton<ITmNative, TmNative>();
    SplatRegistrations.RegisterLazySingleton<ITmsApi, TmsApi>();
    SplatRegistrations.RegisterLazySingleton<IOikSqlApi, OikSqlApi>();
    SplatRegistrations.RegisterLazySingleton<IOikDataApi, OikDataApi>();
    SplatRegistrations.RegisterLazySingleton<ICommonInfrastructure, Infrastructure.Services.Infrastructure>();
    SplatRegistrations.RegisterLazySingleton<ICommonServerService, ServerService>();
  }
}
