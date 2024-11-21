using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Iface.Oik.Tm.Interfaces;
using OikClientDraft.Components.Panels;
using OikClientDraft.Components.Screens;
using OikClientDraft.Infrastructure.Services;
using OikClientDraft.Infrastructure.Util;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace OikClientDraft.Components.Shell;

public partial class ShellViewModel : ViewModelBase,
                                      IScreen
{
  private readonly ICommonInfrastructure _infr;

  public RoutingState Router { get; } = new();

  [Reactive] private string _windowTitle  = Vars.WindowTitle;
  [Reactive] private int    _windowWidth  = Vars.WindowWidth;
  [Reactive] private int    _windowHeight = Vars.WindowHeight;

  [Reactive] private RightPanelViewModel? _rightPanel;
  
  [Reactive] private bool   _isBusy;
  [Reactive] private string _busyMessage = string.Empty;

  [Reactive] private bool _isShowAnalogs = true;


  public ShellViewModel()
  {
    _infr = IoC.Get<ICommonInfrastructure>();
  }


  protected override async void OnActivated(CompositeDisposable disposables)
  {
    IsBusy      = true;
    BusyMessage = "Ждите, установка связи с сервером...";
    try
    {
      await OikHelper.ConnectToServer();
    }
    catch (Exception ex)
    {
      BusyMessage = ex.Message;
      return;
    }

    IsBusy = false;
    
    RightPanel = IoC.Get<RightPanelViewModel>();
    ShowStartPage();

    // подписка на службу проверки связи с сервером
    Observable.FromEventPattern(ev => _infr.ServerService.IsConnectedChanged += ev,
                                ev => _infr.ServerService.IsConnectedChanged -= ev)
              .Subscribe(_ => UpdateConnectivityState())
              .DisposeWith(disposables);
  }


  private void ShowStartPage() 
  {
    Router.NavigateAndReset.Execute(IoC.Get<Canvas1ViewModel>()); // TODO стартовая страница
  }


  [ReactiveCommand]
  private void ShowCanvas1() // TODO команда для перехода на страницу
  {
    Router.Navigate.Execute(IoC.Get<Canvas1ViewModel>()); // TODO страница
  }


  [ReactiveCommand]
  private void ShowCanvas2() // TODO команда для перехода на страницу
  {
    Router.Navigate.Execute(IoC.Get<Canvas2ViewModel>()); // TODO страница
  }


  [ReactiveCommand]
  private void ShowSvg1() // TODO команда для перехода на страницу
  {
    Router.Navigate.Execute(IoC.Get<Svg1ViewModel>()); // TODO страница
  }


  [ReactiveCommand]
  private void ShowTable1() // TODO команда для перехода на страницу
  {
    Router.Navigate.Execute(IoC.Get<Table1ViewModel>()); // TODO страница
  }


  protected override async void OnDeactivated()
  {
    await OikHelper.DisconnectFromServer();
  }


  private void UpdateConnectivityState()
  {
    if (_infr.ServerService.IsConnected)
    {
      IsBusy = false;
    }
    else
    {
      IsBusy      = true;
      BusyMessage = "Нет связи с сервером";
    }
  }
}
