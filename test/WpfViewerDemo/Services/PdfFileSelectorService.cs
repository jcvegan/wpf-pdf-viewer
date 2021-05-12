using Microsoft.Win32;
using WpfViewerDemo.Models;

namespace WpfViewerDemo.Services
{
    public class PdfFileSelectorService : IPdfFileSelectorService
    {
        public PdfItem SelectPdf()
        {
            var selectedFile = new PdfItem()
            {
                IsFileSelected = false
            };

            var openDialog = new OpenFileDialog()
            {
                DefaultExt = ".pdf",
                Filter = "Pdf Files (*.pdf)|*.pdf"
            };

            var result = openDialog.ShowDialog();

            if(result != true)
                return selectedFile;

            selectedFile.IsFileSelected = true;
            selectedFile.PdfSelectedPath = openDialog.FileName;

            return selectedFile;
        }
    }
}
