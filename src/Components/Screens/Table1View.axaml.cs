using Avalonia.ReactiveUI;
using ReactiveUI;

namespace OikClientDraft.Components.Screens;

public partial class Table1View : ReactiveUserControl<Table1ViewModel>
{
  public Table1View()
  {
    this.WhenActivated(_ => { });
    
    InitializeComponent();
  }
}
