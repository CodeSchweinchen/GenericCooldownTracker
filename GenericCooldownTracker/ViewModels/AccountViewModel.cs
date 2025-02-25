﻿using System.Collections.ObjectModel;

namespace GenericCooldownTrackerWpf
{
    public class AccountViewModel
    {
        public string Name { get; set; }
        public ObservableCollection<ActivityBaseViewModel> Activities { get; set; }

        public AccountViewModel()
        {
            Activities = new ObservableCollection<ActivityBaseViewModel>();
        }
    }
}