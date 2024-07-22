using CooldownTrackerLib.Contracts;
using CooldownTrackerLib.Services;
using GenericCooldownTrackerWpf.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace GenericCooldownTrackerWpf
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ProfileService _profileService;
        private ObservableCollection<AccountViewModel> _accounts;
        public ObservableCollection<AccountViewModel> Accounts
        {
            get => _accounts;
            set
            {
                _accounts = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _profileService = new ProfileService();
            Accounts = new ObservableCollection<AccountViewModel>();
            LoadProfiles();
        }

        private void LoadProfiles()
        {
            ProfileSelectComboBox.Items.Clear();
            var profiles = _profileService.GetLoadedProfiles();

            foreach (var profile in profiles)
            {
                ProfileSelectComboBox.Items.Add(profile.Name);
                LoadProfile(profile);
            }
        }

        private void AddProfileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _profileService.AddProfile(openFileDialog.FileName);
                LoadProfiles();
            }
        }

        private void DeleteProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProfileName = ProfileSelectComboBox.SelectedItem as string;

            if (selectedProfileName != null)
            {
                _profileService.DeleteProfile(selectedProfileName);
                LoadProfiles();
            }
        }

        private void LoadProfile(Profile profile)
        {
            var account = new AccountViewModel
            {
                Name = $"{profile.Name} Account {Accounts.Count + 1}"
            };

            DateTime resetTime = DateTime.UtcNow.Date;

            foreach (var cooldown in profile.Cooldowns)
            {
                var newCooldown = new CooldownActivityViewModel(cooldown.Name, cooldown.MaxAttempts, cooldown.CooldownDuration, cooldown.UserProvidedDuration, cooldown.ResetOnCollection, resetTime);
                account.Activities.Add(newCooldown);
            }

            foreach (var dailyActivity in profile.DailyActivities)
            {
                var newDailyActivity = new DailyActivityViewModel(dailyActivity.Name, dailyActivity.MaxAttempts, resetTime);
                account.Activities.Add(newDailyActivity);
            }

            Accounts.Add(account);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
