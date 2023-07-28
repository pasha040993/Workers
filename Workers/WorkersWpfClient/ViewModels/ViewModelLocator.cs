namespace WorkersWpfClient.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel =>
            App.GetService<MainViewModel>();
    }
}
