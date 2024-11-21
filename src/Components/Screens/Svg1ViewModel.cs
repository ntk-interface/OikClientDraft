using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Platform;
using Iface.Oik.Tm.Interfaces;
using OikClientDraft.Infrastructure.Util;
using SkiaSharp;
using Svg;
using Svg.Skia;

namespace OikClientDraft.Components.Screens;

public partial class Svg1ViewModel : ViewModelBaseScreen
{
  // TODO именованный набор ТС на странице
  private readonly Dictionary<string, TmStatus> _statuses = new()
  {
    { "V1", new TmStatus(20, 1, 1) }, 
    { "V2", new TmStatus(20, 2, 1) }, 
    { "V3", new TmStatus(20, 3, 1) },
  };

  // TODO именованный набор ТИТ на странице
  private readonly Dictionary<string, TmAnalog> _analogs = new()
  {
    { "P1", new TmAnalog(20, 1, 1) }, 
    { "Q1", new TmAnalog(20, 1, 2) },
  };

  public event EventHandler InvalidateRequested = delegate { };

  private readonly SvgDocument _svg;


  public Svg1ViewModel()
  {
    // TODO ссылка па SVG-документ из каталога Assets
    _svg = SvgDocument.Open<SvgDocument>(AssetLoader.Open(new Uri("avares://OikClientDraft/Assets/svg1.svg")));
  }


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
    await Api.UpdateTagsPropertiesAndClassData(_statuses.Values.ToList());
    await Api.UpdateTagsPropertiesAndClassData(_analogs.Values.ToList());
  }


  private async Task UpdateTelemetry()
  {
    await Api.UpdateStatuses(_statuses.Values.ToList());
    await Api.UpdateAnalogs(_analogs.Values.ToList());

    UpdateSvgWithTelemetry();
    InvalidateCanvas();
  }


  private void InvalidateCanvas()
  {
    InvalidateRequested.Invoke(this, EventArgs.Empty);
  }


  // TODO изменение свойств SVG-документа в зависимости от текущей телеметрии
  // см. методы внутри класса SvgDocumentExtensions
  private void UpdateSvgWithTelemetry()
  {
    _svg.SetElementFill("V1", _statuses["V1"].IsOn ? Colors.Blue : Colors.Transparent);
    _svg.SetElementFill("V2", _statuses["V2"].IsOn ? Colors.Red : Colors.Transparent);

    if (Shell.IsShowAnalogs)
    {
      _svg.SetElementText("P1", _analogs["P1"].ValueToDisplay);
      _svg.SetElementText("Q1", _analogs["Q1"].ValueToDisplay);
    }
    else
    {
      _svg.SetElementText("P1", string.Empty);
      _svg.SetElementText("Q1", string.Empty);
    }
  }


  // TODO отрисовка страницы
  public void Draw(SKCanvas canvas)
  {
    canvas.Clear(SKColors.White); // цвет фона страницы

    canvas.DrawPicture(SKSvg.CreateFromSvgDocument(_svg).Picture); // выводим на экран SVG
  }
}
