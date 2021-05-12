using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using WpfViewerDemo.Services;
using WpfViewerDemo.Views;

namespace WpfViewerDemo
{
/// <summary>
/// Lógica de interacción para App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<Main>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<IPdfFileSelectorService, PdfFileSelectorService>();
    }
}
}
