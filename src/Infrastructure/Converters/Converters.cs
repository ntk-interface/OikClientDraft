using Avalonia.Data.Converters;

namespace OikClientDraft.Infrastructure.Converters;

public static class Converters
{
  public static readonly IValueConverter ImportanceToOpaqueColorConverter =
    new ImportanceToOpaqueColorConverter();
  
  public static readonly IValueConverter ImportanceToSemiOpaqueColorConverter =
    new ImportanceToSemiOpaqueColorConverter();
  
  public static readonly IValueConverter ImportanceToSemiTransparentColorConverter =
    new ImportanceToSemiTransparentColorConverter();
  
  public static readonly IValueConverter ImportanceToTransparentColorConverter =
    new ImportanceToTransparentColorConverter();

}
