using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace NewsAlarm2.Sound
{
    public partial class SoundSelectPage : PhoneApplicationPage
    {
        public class Channel
        {
            public String Name { get; set; }
            public String StationIconPath { get; set; }
            public String Description { get; set; }
        }

        public class LocalMusic
        {
            public String Album { get; set; }
            public String Singer { get; set; }
            public String Description { get; set; }
            public StreamingAudio AlbumIconPath { get; set; }
        }

        public SoundSelectPage()
        {
            InitializeComponent();

            initBroadCastChannelsList();
            initMusicList();
            initRSSList();
            initAgendaList();
        }

        private void initBroadCastChannelsList() 
        {
            List<Channel> channelList = new List<Channel>();
            channelList.Add(new Channel() { Name = "938 LIVE", Description = "Gets You Talking", StationIconPath = "/Resource/Image/StationIcon/938.png" });
            channelList.Add(new Channel() { Name = "987"     , Description = "Only The Hits",    StationIconPath = "/Resource/Image/StationIcon/987.png" });
            channelList.Add(new Channel() { Name = "Class 95", Description = "Best Mix Of Music", StationIconPath = "/Resource/Image/StationIcon/class 95.png" });
            channelList.Add(new Channel() { Name = "Gold 90.5", Description = "Classics Hits All Day", StationIconPath = "/Resource/Image/StationIcon/gold 90.5.png" });
            channelList.Add(new Channel() { Name = "Lush 99.5", Description = "Sexy. Sensual. Smooth", StationIconPath = "/Resource/Image/StationIcon/lush 99.5.png" });
            channelList.Add(new Channel() { Name = "Symphony 92.4", Description = "Good Music. Good Life", StationIconPath = "/Resource/Image/StationIcon/symphony 92.4.png" });
            channelList.Add(new Channel() { Name = "Capital 95.8", Description = "第一资讯台", StationIconPath = "/Resource/Image/StationIcon/capical 95.8.png" });
            channelList.Add(new Channel() { Name = "Love97.2", Description = "有你的感觉真好", StationIconPath = "/Resource/Image/StationIcon/Love_97.2FM.png" });
            this.lst_broadcast.ItemsSource = channelList;
        }

        private void initMusicList()
        {
            List<LocalMusic> musicList = new List<LocalMusic>();
            musicList.Add(new LocalMusic() { Album = "Album 1", Singer = "Michael Jackson" });
            this.lst_music.ItemsSource = musicList;
        }

        private void initRSSList()
        {
            // to do
        }

        private void initAgendaList()
        {
            // to do 
        }
 
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            MessageBox.Show("Due to time limitation, online radio streaming and local music scanning are not available yet. \nPlease you our RSS feature to get your auto new reminder", "Sorry, We are still working on this.", MessageBoxButton.OK);
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;
            int tabIndex = 0;
            if (parameters.ContainsKey("tab"))
            {
                tabIndex = Convert.ToInt32(parameters["tab"]);
            }

            this.pivot1.SelectedIndex = tabIndex;

            base.OnNavigatedTo(e);
        }

    }
}