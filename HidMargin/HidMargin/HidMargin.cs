using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace HidMargin
{
	[Export(typeof(IWpfTextViewMarginProvider))]
	[Name(nameof(HidMargin))]
	[Order(After = PredefinedMarginNames.ZoomControl, Before = PredefinedMarginNames.HorizontalScrollBar)]
	[MarginContainer(PredefinedMarginNames.Bottom)]
	[ContentType("hid")]
	[TextViewRole(PredefinedTextViewRoles.Interactive)]
	internal class HidMarginProvider : IWpfTextViewMarginProvider
	{

		public IWpfTextViewMargin CreateMargin(IWpfTextViewHost wpfTextViewHost, IWpfTextViewMargin marginContainer)
		{
			return new HidMargin(wpfTextViewHost.TextView);
		}
	}

	internal class HidMargin : StackPanel, IWpfTextViewMargin
	{
		public bool Enabled
		{
			get
			{
				ThrowIfDisposed();
				return true;
			}
		}

		public double MarginSize
		{
			get
			{
				ThrowIfDisposed();
				return this.ActualHeight;
			}
		}

		public FrameworkElement VisualElement
		{
			get
			{
				ThrowIfDisposed();
				return this;
			}
		}

		public HidMargin(IWpfTextView textView)
		{
			Height = 24;
			ClipToBounds = true;
			Background = Brushes.Red;
			Orientation = Orientation.Horizontal;

			Children.Add(new Label { Foreground = Brushes.White, Content = "Hid File" });
			var checkBox = new CheckBox { IsChecked = true };
			checkBox.Click += (sender, e) =>
			{
				Background = checkBox.IsChecked.Value ? Brushes.Red : Brushes.Black;
			};
			Children.Add(checkBox);
		}

		public ITextViewMargin GetTextViewMargin(string marginName)
		{
			return marginName == nameof(HidMargin) ? this : null;
		}

		private bool isDisposed = false;
		public void Dispose()
		{
			if (!isDisposed)
			{
				GC.SuppressFinalize(this);
				isDisposed = true;
			}
		}

		void ThrowIfDisposed()
		{
			if (isDisposed)
			{
				throw new ObjectDisposedException(nameof(HidMargin));
			}
		}
	}
}