// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

using System;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;

namespace NewsAlarm2.Util
{
    public enum ApplicationPages
    {
        SetSound,
        SetClock
    }

    public static class Navigation
    {
        /// <summary>
        /// Goes to page.
        /// </summary>
        /// <param name="applicationPage">The application page.</param>
        public static void GoToPage(this PhoneApplicationPage phoneApplicationPage, ApplicationPages applicationPage, int tabIndex)
        {
            String destination = "";
            switch (applicationPage)
            {
                case ApplicationPages.SetSound:
                    //phoneApplicationPage.NavigationService.Navigate(new Uri("/Sound/SetSoundPage.xaml", UriKind.Relative));
                    destination = "/Sound/SetSoundPage.xaml";
                    destination += "?tab=" + tabIndex;
                    break;
                case ApplicationPages.SetClock:
                    //phoneApplicationPage.NavigationService.Navigate(new Uri("/Clock/SetTimePage.xaml", UriKind.Relative));
                    destination = "/Clock/SetTimePage.xaml";
                    break;
            }
            phoneApplicationPage.NavigationService.Navigate(new Uri(destination, UriKind.Relative));
        }

        public static void LoadSettings()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            if (settings.Contains("gestureSelected"))
            {
                MainPage.snoozeChecked = (settings["gestureSelected"] as string);
            }
            if (settings.Contains("soundSelected"))
            {
                MainPage.soundChecked = (settings["soundSelected"] as string);
            }
            if (settings.Contains("clockSelected"))
            {
                MainPage.reminderTypeSelected = (settings["clockSelected"] as string);
            }
            if (settings.Contains("clockTimeSetting"))
            {
                MainPage.clockTimeSetting = (settings["clockTimeSetting"] as string);
            }
        }

        public static void SaveSettings()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            settings["gestureSelected"] = MainPage.snoozeChecked;
            settings["soundSelected"] = MainPage.soundChecked;
            settings["clockSelected"] = MainPage.reminderTypeSelected;
            settings["clockTimeSetting"] = MainPage.clockTimeSetting;

            settings.Save();
        }

    }
}