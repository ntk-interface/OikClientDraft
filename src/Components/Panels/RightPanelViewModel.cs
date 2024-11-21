using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using Iface.Oik.Tm.Interfaces;
using Iface.Oik.Tm.Utils;
using OikClientDraft.Infrastructure.Util;

namespace OikClientDraft.Components.Panels;

// TODO что вообще нужно в правой панели
public partial class RightPanelViewModel : ViewModelBase
{
  private readonly IOikDataApi _api;

  public ObservableCollection<TmAlert> Alerts { get; } = new();
  
  
  public RightPanelViewModel()
  {
    _api = IoC.Get<IOikDataApi>();
  }


  protected override async void OnActivated(CompositeDisposable disposables)
  {
    var alerts = await _api.GetAlerts();
    
    Alerts.AddRange(alerts);
  }
}
