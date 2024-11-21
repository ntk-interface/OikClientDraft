using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Iface.Oik.Tm.Interfaces;
using SkiaSharp;

namespace OikClientDraft.Components.Screens;

public partial class Canvas1ViewModel : ViewModelBaseScreen
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
    canvas.Clear(SKColors.White); // цвет фона страницы
    canvas.Translate(0.5f, 0.5f); // для отрисовки линий без антиалиасинга

    using (var paint = new SKPaint())
    using (var paintText = new SKPaint())
    {
      paintText.IsAntialias = true;
      paintText.Typeface    = SKTypeface.FromFamilyName("Inter");
      
      // подпись
      canvas.DrawText("Это ручная рисовка", 250, 15, paintText);

      // присоединение 1
      paint.Color = SKColors.Blue;
      canvas.DrawLine(50, 10, 50, 30, paint);
      if (_statuses["V1"].IsOn)
      {
        paint.Style = SKPaintStyle.Fill;
      }
      else
      {
        paint.Style = SKPaintStyle.Stroke;
      }
      canvas.DrawRect(50 - 5, 30, 10, 10, paint);
      canvas.DrawLine(50, 40, 50, 60, paint);

      // измерения присоединения 1 (если зажата кнопка отображения)
      if (Shell.IsShowAnalogs)
      {
        paintText.Color = SKColors.Green;
        canvas.DrawText(_analogs["P1"].ValueToDisplay, 65, 40 - 10, paintText);
        canvas.DrawText(_analogs["Q1"].ValueToDisplay, 65, 40 + 10, paintText);
      }

      // присоединение 2
      paint.Color = SKColors.Red;
      canvas.DrawLine(150, 10, 150, 30, paint);
      if (_statuses["V2"].IsOn)
      {
        paint.Style = SKPaintStyle.Fill;
      }
      else
      {
        paint.Style = SKPaintStyle.Stroke;
      }
      canvas.DrawRect(150 - 5, 30, 10, 10, paint);
      canvas.DrawLine(150, 40, 150, 60, paint);
    }
  }
}
