using System;
using Iface.Oik.Tm.Interfaces;
using OikClientDraft.Components.Shell;
using OikClientDraft.Infrastructure.Util;
using ReactiveUI;

namespace OikClientDraft.Components.Screens;

public abstract class ViewModelBaseScreen : ViewModelBase,
                                            IRoutableViewModel
{
  public IScreen HostScreen { get; } = default!;

  public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..10]; // случайный идентификатор

  protected readonly ShellViewModel Shell;
  protected readonly IOikDataApi    Api;


  public ViewModelBaseScreen()
  {
    Shell = IoC.Get<ShellViewModel>();
    Api   = IoC.Get<IOikDataApi>();
  }
}
