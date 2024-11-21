using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls.Skia;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace OikClientDraft.Components.Screens;

public partial class Svg1View : ReactiveUserControl<Svg1ViewModel>
{
  private Svg1ViewModel? _viewModel;
  
  public Svg1View()
  {
    this.WhenActivated(disposables =>
    {
      _viewModel = ViewModel!;
      
      // подписка на запрос перерисовки от модели представления
      Observable.FromEventPattern(ev => _viewModel.InvalidateRequested += ev,
                                  ev => _viewModel.InvalidateRequested -= ev)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(_ => InvalidateCanvas())
                .DisposeWith(disposables);

    });
    
    InitializeComponent();
  }


  private void InvalidateCanvas()
  {
    Canvas.InvalidateVisual();
  }


  private void Canvas_OnDraw(object? sender, SKCanvasEventArgs e)
  {
    _viewModel?.Draw(e.Canvas);
  }
}
