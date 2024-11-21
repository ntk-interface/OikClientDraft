using System;
using Avalonia.Media;
using Svg;
using Svg.Transforms;

namespace OikClientDraft.Infrastructure.Util;

public static class SvgDocumentExtensions
{
  // делает элемент видимым
  public static void SetElementIsVisible(this SvgDocument svg, string elementId)
  {
    var element = svg.GetElementById(elementId);
    element.Visibility = "visible";
  }


  // делает элемент невидимым
  public static void SetElementIsHidden(this SvgDocument svg, string elementId)
  {
    var element = svg.GetElementById(elementId);
    element.Visibility = "hidden";
  }


  // изменяет цвет обводки элемента
  public static void SetElementStroke(this SvgDocument svg, string elementId, Color value)
  {
    var element = svg.GetElementById(elementId);
    element.Stroke = new SvgColourServer(System.Drawing.Color.FromArgb((int)value.ToUInt32()));
  }


  // изменяет цвет заливки элемента
  public static void SetElementFill(this SvgDocument svg, string elementId, Color value)
  {
    var element = svg.GetElementById(elementId);
    element.Fill = new SvgColourServer(System.Drawing.Color.FromArgb((int)value.ToUInt32()));
  }


  // изменяет текст внутри текстового элемента
  public static void SetElementText(this SvgDocument svg, string elementId, string value)
  {
    var element = svg.GetElementById(elementId);
    if (element is not SvgTextBase textElement)
    {
      throw new ArgumentException($"Невозможно установить текст для элемента {elementId}");
    }
    textElement.Text = value;
  }


  // изменяет трансформацию элемента
  // например: SetElementTransform("test", "scale(2,2)")
  public static void SetElementTransform(this SvgDocument svg, string elementId, string value)
  {
    var element = svg.GetElementById(elementId);
    element.Transforms = SvgTransformConverter.Parse(value);
  }


  // изменяет координату X1 линии 
  public static void SetElementX1(this SvgDocument svg, string elementId, float value)
  {
    var element = svg.GetElementById(elementId);
    if (element is not SvgLine lineElement)
    {
      throw new ArgumentException($"Невозможно установить X1 для элемента {elementId}");
    }
    lineElement.StartX = new SvgUnit(SvgUnitType.Pixel, value);
  }


  // изменяет координату X2 линии 
  public static void SetElementX2(this SvgDocument svg, string elementId, float value)
  {
    var element = svg.GetElementById(elementId);
    if (element is not SvgLine lineElement)
    {
      throw new ArgumentException($"Невозможно установить X1 для элемента {elementId}");
    }
    lineElement.EndX = new SvgUnit(SvgUnitType.Pixel, value);
  }


  // изменяет координату Y1 линии 
  public static void SetElementY1(this SvgDocument svg, string elementId, float value)
  {
    var element = svg.GetElementById(elementId);
    if (element is not SvgLine lineElement)
    {
      throw new ArgumentException($"Невозможно установить X1 для элемента {elementId}");
    }
    lineElement.StartY = new SvgUnit(SvgUnitType.Pixel, value);
  }


  // изменяет координату Y2 линии 
  public static void SetElementY2(this SvgDocument svg, string elementId, float value)
  {
    var element = svg.GetElementById(elementId);
    if (element is not SvgLine lineElement)
    {
      throw new ArgumentException($"Невозможно установить X1 для элемента {elementId}");
    }
    lineElement.EndY = new SvgUnit(SvgUnitType.Pixel, value);
  }
}
