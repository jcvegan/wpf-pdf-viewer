using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Jcvegan.PdfViewer
{
[TemplatePart(Name = "PART_Browser", Type = typeof(WebBrowser))]
public class PdfFileViewer : Control
{
    public static readonly DependencyProperty FileSourceProperty = DependencyProperty.Register("FileSource", typeof(string), typeof(PdfFileViewer), new UIPropertyMetadata(null, null));

    private WebBrowser _theBrowser;

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

        if (string.IsNullOrEmpty(FileSource))
            return;

        if (!File.Exists(FileSource))
            return;

        _theBrowser = GetTemplateChild("PART_Browser") as WebBrowser;

        _theBrowser.Navigate(FileSource);
    }
}
}
