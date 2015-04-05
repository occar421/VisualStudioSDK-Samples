using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace HidClassifier
{
	[Export(typeof(IClassifierProvider))]
	[ContentType("hid")] // Content Type が "hid" のものだけに適応
	internal class HidClassifierProvider : IClassifierProvider
	{
		[Import]
		internal IClassificationTypeRegistryService ClassificationRegistry = null;

		public IClassifier GetClassifier(ITextBuffer buffer)
		{
			return buffer.Properties.GetOrCreateSingletonProperty(() => new HidClassifier(ClassificationRegistry));
		}
	}

	class HidClassifier : IClassifier
	{
		private IClassificationType normalType;
		private IClassificationType commentType;
		private IClassificationType todoCommentType;

		public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

		internal HidClassifier(IClassificationTypeRegistryService registry)
		{
			normalType = registry.GetClassificationType("hid normal");
			// デフォルトや他の拡張で用意されているものを使用できる
			commentType = registry.GetClassificationType("comment");
			todoCommentType = registry.GetClassificationType("hid todo");
		}

		public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
		{
			var classifications = new List<ClassificationSpan>();

			var text = span.GetText();
			// '#'が行頭にあれば
			if (text.StartsWith("#"))
			{
				classifications.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), commentType));
			}
			// '$TODO'が行頭にあれば
			else if (text.StartsWith("$TODO"))
			{
				classifications.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), todoCommentType));
			}
			else
			{
				classifications.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), normalType));
			}
			return classifications;
		}
	}
}
