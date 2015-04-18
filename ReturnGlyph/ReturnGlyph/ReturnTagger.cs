using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ReturnGlyph
{
	[Export(typeof(ITaggerProvider))]
	[ContentType("csharp")]
	[TagType(typeof(ReturnTag))]
	class ReturnTaggerProvider : ITaggerProvider
	{
		[Import]
		internal IClassifierAggregatorService AggregatorService;

		public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
		{
			if (buffer == null)
			{
				throw new ArgumentNullException(nameof(buffer));
			}

			return new ReturnTagger(AggregatorService.GetClassifier(buffer)) as ITagger<T>;
		}
	}

	internal class ReturnTagger : ITagger<ReturnTag>
	{
		private IClassifier classifier;

		public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

		internal ReturnTagger(IClassifier classifier)
		{
			this.classifier = classifier;
		}

		public IEnumerable<ITagSpan<ReturnTag>> GetTags(NormalizedSnapshotSpanCollection spans)
		{
			foreach (var aSpan in spans)
			{
				foreach (var returnSpan in classifier.GetClassificationSpans(aSpan)
					.Where(x => x.ClassificationType.Classification == "keyword" && x.Span.GetText() == "return"))
				{
					yield return new TagSpan<ReturnTag>(new SnapshotSpan(returnSpan.Span.Start, returnSpan.Span.Length), new ReturnTag());
				}
			}
		}
	}
}
