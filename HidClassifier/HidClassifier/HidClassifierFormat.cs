using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace HidClassifier
{
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "hid normal")]
	[Name("hid normal")]
	[DisplayName("Hid Normal")]
	[UserVisible(true)]
	[Order(Before = Priority.Default)]
	internal sealed class HidClassiferNormalFormat : ClassificationFormatDefinition
	{
		public HidClassiferNormalFormat()
		{
			DisplayName = "Hid Normal";	// Fonts and Colorsで表示される名前
			TextDecorations = System.Windows.TextDecorations.Underline;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "hid todo")]
	[Name("hid todo")]
	[DisplayName("Hid Todo Comment")]
	[UserVisible(true)]
	[Order(Before = Priority.Default)]
	internal sealed class HidClassiferTodoCommentFormat : ClassificationFormatDefinition
	{
		public HidClassiferTodoCommentFormat()
		{
			DisplayName = "Hid Todo Comment"; // Fonts and Colorsで表示される名前
			BackgroundColor= Colors.Green;
			ForegroundColor = Colors.White;
		}
	}
}
