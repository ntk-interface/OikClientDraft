using System;
using System.Linq;
using System.Reflection;
using OikClientDraft.Components;
using ReactiveUI;
using Splat;

namespace OikClientDraft.Infrastructure.Util;

public static class IoC
{
  public static void Initialize()
  {
    SplatRegistrations.SetupIOC();
  }
  
  
  public static T Get<T>()
  {
    return Locator.Current.GetService<T>() ?? throw new Exception();
  }


  public static void RegisterAllViewModels()
  {
    foreach (var viewModel in Assembly.GetExecutingAssembly()
                                      .GetTypes()
                                      .Where(t => t.IsSubclassOf(typeof(ViewModelBase))))
    {
      Locator.CurrentMutable.RegisterLazySingleton(() => Activator.CreateInstance(viewModel), viewModel);
    }
  }


  public static void RegisterAllViews()
  {
    Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
  }
}
