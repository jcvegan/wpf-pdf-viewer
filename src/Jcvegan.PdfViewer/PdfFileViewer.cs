using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Jcvegan.PdfViewer
{
    [TemplatePart(Name = "PART_Browser", Type = typeof(WebBrowser))]
    public class PdfFileViewer : Control
    {
        public static readonly DependencyProperty FileSourceProperty = DependencyProperty.Register("FileSource", typeof(string), typeof(PdfFileViewer), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnFileSourceChanged));

        private static void OnFileSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var control = (PdfFileViewer)d;

            control.RefreshSource();
        }


        public string FileSource
        {
            get => (string)GetValue(FileSourceProperty);
            set => SetValue(FileSourceProperty, value);
        }

        static PdfFileViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PdfFileViewer), new FrameworkPropertyMetadata(typeof(PdfFileViewer)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RefreshSource();
        }

        public void RefreshSource()
        {
            if (string.IsNullOrEmpty(FileSource))
                return;

            if (!File.Exists(FileSource))
                return;

            LoadDocument();
        }

        private void LoadDocument()
        {
            var browser = GetTemplateChild("PART_Browser") as WebBrowser;

            browser?.Navigate(FileSource);
        }
    }
}
