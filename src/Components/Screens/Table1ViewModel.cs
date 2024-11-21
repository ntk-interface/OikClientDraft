using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Iface.Oik.Tm.Interfaces;

namespace OikClientDraft.Components.Screens;

public partial class Table1ViewModel : ViewModelBaseScreen
{
  // TODO набор ТС на странице
  public ObservableCollection<TmStatus> Statuses { get; } = new()
  {
    new TmStatus(20, 1, 1), 
    new TmStatus(20, 2, 1),
    new TmStatus(20, 3, 1),
    new TmStatus(24, 1, 1),
    new TmStatus(24, 1, 2),
    new TmStatus(24, 1, 3),
    new TmStatus(24, 1, 4),
  };

  // TODO набор ТС на странице
  public ObservableCollection<TmAnalog> Analogs { get; } = new()
  {
    new TmAnalog(20, 1, 1),
    new TmAnalog(20, 1, 2),
    new TmAnalog(20, 1, 3),
    new TmAnalog(20, 1, 4),
    new TmAnalog(20, 1, 5),
  };


  protected override async void OnActivated(CompositeDisposable disposables)
  {
    await UpdateTelemetryInfo();
    await UpdateTelemetry();

    // обновляем телеметрию по таймеру
    Observable.Interval(TimeSpan.FromSeconds(Vars.DefaultUpdateInterval)) // TODO изменить интервал при необходимости
              .Select(async _ => await UpdateTelemetry())
              .Subscribe()
              .DisposeWith(disposables);
  }


  private async Task UpdateTelemetryInfo()
  {
    await Api.UpdateTagsPropertiesAndClassData(Statuses);
    await Api.UpdateTagsPropertiesAndClassData(Analogs);
  }


  private async Task UpdateTelemetry()
  {
    await Api.UpdateStatuses(Statuses);
    await Api.UpdateAnalogs(Analogs);
  }
}
