using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace WpfViewerDemo.ViewModel {
  public class MainViewModel : INotifyPropertyChanged, INotifyPropertyChanging {
    private string _pdfFilePath;

    public string PdfFilePath {
      get => _pdfFilePath;
      set => SetValue(ref _pdfFilePath, value);
    }

    public MainViewModel() { PdfFilePath = Path.GetFullPath("sample.pdf"); }

    public event PropertyChangingEventHandler PropertyChanging;
    public event PropertyChangedEventHandler PropertyChanged;

    protected void
    SetValue<TProperty>(ref TProperty field, TProperty newValue,
                        [ CallerMemberName ] string propertyName = null) {
      if (!EqualityComparer<TProperty>.Default.Equals(field, newValue)) {
        OnPropertyChanging(propertyName);

        field = newValue;

        OnPropertyChanged(propertyName);
      }
    }

    protected virtual void
    OnPropertyChanging([ CallerMemberName ] string name = null) {
      PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
    }
    protected virtual void
    OnPropertyChanged([ CallerMemberName ] string name = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}
