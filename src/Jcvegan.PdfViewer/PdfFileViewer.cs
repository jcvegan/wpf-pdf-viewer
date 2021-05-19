using Jcvegan.PdfViewer.Commanding;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

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

        public bool IsFileLoaded
        {
            get;
            private set;
        }

        public ICommand ResetCommand
        {
            get;
            set;
        }

        static PdfFileViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PdfFileViewer), new FrameworkPropertyMetadata(typeof(PdfFileViewer)));
        }

        public PdfFileViewer()
        {
            ResetCommand = new RelayCommand((parameter) => Unload(), (parameter) => IsFileLoaded);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RefreshSource();
        }

        public void RefreshSource()
        {
            IsFileLoaded = false;

            if (string.IsNullOrEmpty(FileSource))
                return;

            if (!File.Exists(FileSource))
                return;

            LoadDocument();
        }

        private void LoadDocument()
        {
            var browser = GetTemplateChild("PART_Browser") as WebBrowser;

            if (browser == null)
                return;

            browser.LoadCompleted -= OnBrowserLoadCompleted;
            browser.LoadCompleted += OnBrowserLoadCompleted;

            browser.Navigate(FileSource);
        }

        public void Unload()
        {
            if (!IsFileLoaded)
                return;

            var browser = GetTemplateChild("PART_Browser") as WebBrowser;

            browser.Source = null;

            IsFileLoaded = false;
        }

        private void OnBrowserLoadCompleted(object sender, NavigationEventArgs e)
        {
            IsFileLoaded = true;
        }
    }
}