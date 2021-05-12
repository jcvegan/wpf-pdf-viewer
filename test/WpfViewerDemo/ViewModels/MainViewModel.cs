using Prism.Commands;
using Prism.Mvvm;
using System.IO;
using WpfViewerDemo.Services;

namespace WpfViewerDemo.ViewModels
{
public class MainViewModel : BindableBase
{
    private string _pdfFilePath;

    private readonly IPdfFileSelectorService _pdfSelectorService;

    public string PdfFilePath
    {
        get => _pdfFilePath;
        set => SetProperty(ref _pdfFilePath, value);
    }

    public DelegateCommand SelectPdfCommand {
        get;
        private set;
    }

    public MainViewModel(IPdfFileSelectorService pdfSelectorService)
    {
        PdfFilePath = Path.GetFullPath("sample.pdf");

        _pdfSelectorService = pdfSelectorService;

        SelectPdfCommand = new DelegateCommand(SelectPdf);
    }

    private void SelectPdf()
    {
        var item = _pdfSelectorService.SelectPdf();

        if (item.IsFileSelected)
            PdfFilePath = item.PdfSelectedPath;
    }
}
}
