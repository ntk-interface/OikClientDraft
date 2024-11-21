using Avalonia.Media;

namespace OikClientDraft.Styles;

public static class ImportanceColors
{
  private const byte Imp0Red   = 0x4c;
  private const byte Imp0Green = 0xaf;
  private const byte Imp0Blue  = 0x50;
  private const byte Imp1Red   = 0x21;
  private const byte Imp1Green = 0x96;
  private const byte Imp1Blue  = 0xf3;
  private const byte Imp2Red   = 0xff;
  private const byte Imp2Green = 0xa7;
  private const byte Imp2Blue  = 0x26;
  private const byte Imp3Red   = 0xf4;
  private const byte Imp3Green = 0x43;
  private const byte Imp3Blue  = 0x36;

  public static readonly Color[] OpaqueColors =
  {
    Color.FromRgb(Imp0Red, Imp0Green, Imp0Blue), // Green 500
    Color.FromRgb(Imp1Red, Imp1Green, Imp1Blue), // Blue 500
    Color.FromRgb(Imp2Red, Imp2Green, Imp2Blue), // Orange 400
    Color.FromRgb(Imp3Red, Imp3Green, Imp3Blue), // Red 500
  };

  public static readonly Color[] SemiOpaqueColors =
  {
    Color.FromArgb(0xcc, Imp0Red, Imp0Green, Imp0Blue), 
    Color.FromArgb(0xcc, Imp1Red, Imp1Green, Imp1Blue),
    Color.FromArgb(0xcc, Imp2Red, Imp2Green, Imp2Blue), 
    Color.FromArgb(0xcc, Imp3Red, Imp3Green, Imp3Blue),
  };

  public static readonly Color[] SemiTransparentColors =
  {
    Color.FromArgb(0x44, Imp0Red, Imp0Green, Imp0Blue), 
    Color.FromArgb(0x44, Imp1Red, Imp1Green, Imp1Blue),
    Color.FromArgb(0x44, Imp2Red, Imp2Green, Imp2Blue), 
    Color.FromArgb(0x44, Imp3Red, Imp3Green, Imp3Blue),
  };

  public static readonly Color[] TransparentColors =
  {
    Color.FromArgb(0x22, Imp0Red, Imp0Green, Imp0Blue), 
    Color.FromArgb(0x22, Imp1Red, Imp1Green, Imp1Blue),
    Color.FromArgb(0x22, Imp2Red, Imp2Green, Imp2Blue), 
    Color.FromArgb(0x22, Imp3Red, Imp3Green, Imp3Blue),
  };
}
