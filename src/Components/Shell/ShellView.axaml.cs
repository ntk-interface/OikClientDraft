using Avalonia.ReactiveUI;
using ReactiveUI;

namespace OikClientDraft.Components.Shell;

public partial class ShellView : ReactiveWindow<ShellViewModel>
{
  public ShellView()
  {
    this.WhenActivated(_ => { });
    
    InitializeComponent();
  }
}
