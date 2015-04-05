using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace HidClassifier
{
	internal static class HidClassifierClassificationDefinition
	{
		[Export(typeof(ClassificationTypeDefinition))]
		[Name("hid normal")] // タイプの登録名
		internal static ClassificationTypeDefinition hidClassifierNormalType = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("hid todo")] // タイプの登録名
		internal static ClassificationTypeDefinition hidClassifierTodoCommentType = null;
	}
}
