using System.Threading.Tasks;

namespace ShowMeMetro.ViewModels
{
    public class ViewModelLocator
    {
        private MainViewModel _main = new MainViewModel();

        public MainViewModel Main
        {
            get
            {
                return _main;
            }
        }

        public async Task Load()
        {
            await _main.LoadShows();
        }
    }
}
