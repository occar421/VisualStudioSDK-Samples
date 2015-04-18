using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace ReturnGlyph
{
	[Export(typeof(IGlyphFactoryProvider))]
	[Name(nameof(ReturnGlyphFactory))]
	[Order(After = "VsTextMarker")]
	[ContentType("csharp")]
	[TagType(typeof(ReturnTag))]
	internal sealed class ReturnGlyphFactoryProvider : IGlyphFactoryProvider
	{
		public IGlyphFactory GetGlyphFactory(IWpfTextView view, IWpfTextViewMargin margin)
		{
			return new ReturnGlyphFactory();
		}
	}

	internal class ReturnGlyphFactory : IGlyphFactory
	{
		public UIElement GenerateGlyph(IWpfTextViewLine line, IGlyphTag tag)
		{
			return (tag == null || !(tag is ReturnTag)) ? null
				: new Ellipse
				{
					Fill = Brushes.Transparent,
					StrokeThickness = 1.5,
					Stroke = Brushes.DarkBlue,
					Height = 14,
					Width = 14
				};
		}
	}
}
