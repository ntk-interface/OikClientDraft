using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using OikClientDraft.Components.Shell;
using OikClientDraft.Infrastructure.Util;

namespace OikClientDraft;

public partial class App : Application
{
  public override void Initialize()
  {
    Bootstrapper.Load();
    AvaloniaXamlLoader.Load(this);
  }


  public override void OnFrameworkInitializationCompleted()
  {
    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
      desktop.MainWindow = new ShellView { DataContext = IoC.Get<ShellViewModel>() };
    }

    base.OnFrameworkInitializationCompleted();
  }

}
