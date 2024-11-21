using Avalonia.ReactiveUI;
using ReactiveUI;

namespace OikClientDraft.Components.Panels;

public partial class RightPanelView : ReactiveUserControl<RightPanelViewModel>
{
  public RightPanelView()
  {
    this.WhenActivated(_ => { });
    
    InitializeComponent();
  }
}
