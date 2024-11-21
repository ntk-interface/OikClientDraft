using System.Reactive.Disposables;
using ReactiveUI;

namespace OikClientDraft.Components;

public abstract class ViewModelBase : ReactiveObject,
                                      IActivatableViewModel
{
  public ViewModelActivator Activator { get; } = new();


  protected ViewModelBase()
  {
    this.WhenActivated(disposables =>
    {
      OnActivated(disposables);
      Disposable.Create(OnDeactivated).DisposeWith(disposables);
    });
  }


  protected virtual void OnActivated(CompositeDisposable disposables)
  {
  }


  protected virtual void OnDeactivated()
  {
  }
}
