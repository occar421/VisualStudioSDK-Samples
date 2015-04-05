using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace HidClassifier
{
	internal static class HidContentTypeDefinitions
	{
		[Export]
		[Name("hid")] // Content Typeの名前
		[BaseDefinition("text")]
		internal static ContentTypeDefinition hidContentTypeDefinition;

		[Export]
		[FileExtension(".hid")]	// 拡張子
		[ContentType("hid")] // Content Typeの名前
		internal static FileExtensionToContentTypeDefinition hidFileExtensionDefinition;
	}
}
