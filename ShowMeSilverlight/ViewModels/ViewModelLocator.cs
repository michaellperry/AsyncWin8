﻿
namespace ShowMeSilverlight.ViewModels
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
    }
}
