using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using CooldownTrackerLib.Contracts;

namespace CooldownTrackerLib.Services
{
    public class ProfileService
    {
        private const string ProfilesDirectory = "Profiles";
        private List<Profile> _loadedProfiles;

        public ProfileService()
        {
            _loadedProfiles = new List<Profile>();
            LoadProfiles();
        }

        public List<Profile> GetLoadedProfiles()
        {
            return _loadedProfiles;
        }

        private void LoadProfiles()
        {
            if (!Directory.Exists(ProfilesDirectory))
            {
                Directory.CreateDirectory(ProfilesDirectory);
            }

            _loadedProfiles.Clear();

            var profileFiles = Directory.GetFiles(ProfilesDirectory, "*.json");
            foreach (var filename in profileFiles)
            {
                string json = File.ReadAllText(filename);
                var profile = JsonConvert.DeserializeObject<Profile>(json);
                _loadedProfiles.Add(profile);
            }
        }

        public void AddProfile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var profile = JsonConvert.DeserializeObject<Profile>(json);

            // Save the profile to the profiles directory
            string destinationPath = Path.Combine(ProfilesDirectory, Path.GetFileName(filePath));
            File.Copy(filePath, destinationPath, true);

            _loadedProfiles.Add(profile);
        }

        public void DeleteProfile(string profileName)
        {
            var profile = _loadedProfiles.FirstOrDefault(p => p.Name == profileName);

            if (profile != null)
            {
                _loadedProfiles.Remove(profile);

                // Remove the profile from the profiles directory
                string profilePath = Path.Combine(ProfilesDirectory, $"{profile.Name}.json");
                if (File.Exists(profilePath))
                {
                    File.Delete(profilePath);
                }
            }
        }
    }
}
