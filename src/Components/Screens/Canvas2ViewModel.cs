using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Platform;
using Iface.Oik.Tm.Interfaces;
using SkiaSharp;

namespace OikClientDraft.Components.Screens;

public partial class Canvas2ViewModel : ViewModelBaseScreen
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


  private readonly SKBitmap _bitmap;


  public Canvas2ViewModel()
  {
    // TODO ссылка па подложку из каталога Assets
    _bitmap = SKBitmap.Decode(AssetLoader.Open(new Uri("avares://OikClientDraft/Assets/canvas2.png")));
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

    InvalidateCanvas();
  }


  private void InvalidateCanvas()
  {
    InvalidateRequested.Invoke(this, EventArgs.Empty);
  }


  // TODO отрисовка страницы
  public void Draw(SKCanvas canvas)
  {
    canvas.Clear(SKColors.White);     // цвет фона страницы
    canvas.DrawBitmap(_bitmap, 0, 0); // вывод подложки
    canvas.Translate(0.5f, 0.5f);     // для отрисовки линий без антиалиасинга

    using (var paint = new SKPaint())
    using (var paintText = new SKPaint())
    {
      paintText.IsAntialias = true;
      paintText.Typeface    = SKTypeface.FromFamilyName("Inter");

      // присоединение 1
      if (_statuses["V1"].IsOn)
      {
        paint.Color = SKColors.Blue;
        paint.Style = SKPaintStyle.Fill;
        canvas.DrawRect(52, 52, 5, 5, paint);
      }

      // измерения присоединения 1 (если зажата кнопка отображения)
      if (Shell.IsShowAnalogs)
      {
        paintText.Color = SKColors.Green;
        canvas.DrawText(_analogs["P1"].ValueToDisplay, 65, 60 - 10, paintText);
        canvas.DrawText(_analogs["Q1"].ValueToDisplay, 65, 60 + 10, paintText);
      }

      // присоединение 2
      if (_statuses["V2"].IsOn)
      {
        paint.Color = SKColors.Red;
        paint.Style = SKPaintStyle.Fill;
        canvas.DrawRect(152, 52, 5, 5, paint);
      }
    }
  }
}
