using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace MinimumVsix
{
	[Export(typeof(IClassifierProvider))]
	[ContentType("text")]
	internal class ClassifierProvider : IClassifierProvider
	{
		[Import]
		private IClassificationTypeRegistryService classificationRegistry;

		public IClassifier GetClassifier(ITextBuffer textBuffer)
		{
			return textBuffer.Properties.GetOrCreateSingletonProperty(() => new Classifier(classificationRegistry));
		}
	}

	internal class Classifier : IClassifier
	{
		private readonly IClassificationType commentType;

		internal Classifier(IClassificationTypeRegistryService registry)
		{
			commentType = registry.GetClassificationType("comment");
		}

		public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

		public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
		{
			return new List<ClassificationSpan>
			{
				new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), commentType)
			};
		}
	}
}
