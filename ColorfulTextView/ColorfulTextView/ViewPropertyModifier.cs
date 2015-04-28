using System;
using System.Collections;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace ColorfulTextView
{
	[Export(typeof(IWpfTextViewCreationListener))]
	[ContentType("csharp")]
	[TextViewRole(PredefinedTextViewRoles.Document)]
	class ViewPropertyModifier : IWpfTextViewCreationListener
	{
		[Import]
		internal IEditorFormatMapService FormatMapService = null;

		public void TextViewCreated(IWpfTextView textView)
		{
			IEditorFormatMap formatMap = FormatMapService.GetEditorFormatMap(textView);

			// 入力バー
			ResourceDictionary caretProperties = formatMap.GetProperties("Caret");
			// 挿入バー
			ResourceDictionary insertCaretProperties = formatMap.GetProperties("Overwrite Caret");
			// ブレークポイントが置かれるスペース
			ResourceDictionary indicatorMarginProperties = formatMap.GetProperties("Indicator Margin");
			// 空白文字表示をOnにしたときの表示
			ResourceDictionary visibleWhitespaceProperties = formatMap.GetProperties("Visible Whitespace");
			// 範囲選択
			ResourceDictionary selectedTextProperties = formatMap.GetProperties("Selected Text");
			// アクティブでない範囲選択
			ResourceDictionary inactiveSelectedTextProperties = formatMap.GetProperties("Inactive Selected Text");

			// 変更開始
			formatMap.BeginBatchUpdate();

			caretProperties[EditorFormatDefinition.ForegroundColorId] = Colors.Red;
			formatMap.SetProperties("Caret", caretProperties);

			insertCaretProperties[EditorFormatDefinition.ForegroundColorId] = Colors.Violet;
			formatMap.SetProperties("Overwrite Caret", insertCaretProperties);

			indicatorMarginProperties[EditorFormatDefinition.BackgroundColorId] = Colors.Coral;
			formatMap.SetProperties("Indicator Margin", indicatorMarginProperties);

			visibleWhitespaceProperties[EditorFormatDefinition.ForegroundColorId] = Colors.Turquoise;
			formatMap.SetProperties("Visible Whitespace", visibleWhitespaceProperties);

			selectedTextProperties[EditorFormatDefinition.BackgroundBrushId] = new LinearGradientBrush(Colors.Aquamarine, Colors.Azure, 90.0);
			formatMap.SetProperties("Selected Text", selectedTextProperties);

			inactiveSelectedTextProperties[EditorFormatDefinition.BackgroundBrushId] = new RadialGradientBrush(Colors.Gray, Colors.LightGray);
			formatMap.SetProperties("Inactive Selected Text", inactiveSelectedTextProperties);

			// 変更終了
			formatMap.EndBatchUpdate();
		}
	}
}
