using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using OikClientDraft.Styles;

namespace OikClientDraft.Infrastructure.Converters;

public class ImportanceToSemiOpaqueColorConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (TryGetColorFromValue(value, out var color))
    {
      if (targetType == typeof(Color))
      {
        return color;
      }

      if (targetType == typeof(IBrush))
      {
        return new SolidColorBrush(color);
      }
    }

    return value;
  }

  public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    throw new NotSupportedException();
  }

  private static bool TryGetColorFromValue(object? value, out Color color)
  {
    switch (value)
    {
      case int intValue and >= 0 and <= 3:
        color = ImportanceColors.SemiOpaqueColors[intValue];
        return true;

      case short shortValue and >= 0 and <= 3:
        color = ImportanceColors.SemiOpaqueColors[shortValue];
        return true;

      default:
        color = new Color();
        return false;
    }
  }
}

