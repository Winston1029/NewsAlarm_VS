using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.IO;
using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.Phone.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

namespace MyAudioPlaybackAgent
{
    public class RSSub
    {
        public  List<RSSItemDetail> rssListItems;

        private AudioPlayer audioPlayer;
        private Boolean loaded = false;

        public RSSub(AudioPlayer audioPlayer)
        {
            rssListItems = new List<RSSItemDetail>();
            this.audioPlayer = audioPlayer;
        }

        public void RSSSubStart()
        {
            WebClient webClient = new WebClient();

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);

            webClient.DownloadStringAsync(new System.Uri("http://www.marketwatch.com/feeds/podcast/podcast.asp?count=10&doctype=116&column=The+Wall+Street+Journal+This+Morning"));
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    // Showing the exact error message is useful for debugging. In a finalized application, 
                    // output a friendly and applicable string to the user instead. 
                    MessageBox.Show(e.Error.Message);
                });
            }
            else
            {
                // Save the feed into the State property in case the application is tombstoned. 
                //this.State["feed"] = e.Result;

                UpdateFeedList(e.Result);
            }

            if (loaded == false)
            {
                audioPlayer.loadTracks();
                AudioPlayer.playState = true;
                loaded = true;
            }
            //audioPlayer.loadTracks();
           // if(audioPlayer.)
        }

        private void UpdateFeedList(string feedXML)
        {

            // Load the feed into a SyndicationFeed instance
            StringReader stringReader = new StringReader(feedXML);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

            // In Windows Phone OS 7.1, WebClient events are raised on the same type of thread they were called upon. 
            // For example, if WebClient was run on a background thread, the event would be raised on the background thread. 
            // While WebClient can raise an event on the UI thread if called from the UI thread, a best practice is to always 
            // use the Dispatcher to update the UI. This keeps the UI thread free from heavy processing.
            //Deployment.Current.Dispatcher.BeginInvoke(() =>
            //{

            //    // Bind the list of SyndicationItems to our ListBox
            //   // feedListBox.ItemsSource = feed.Items;

            //    //loadFeedButton.Content = "Refresh Feed";

            //});

            getAudioURL(feed);
        }

        private void getAudioURL(SyndicationFeed feed)
        {
            for (int i = 0; i < feed.Items.Count(); i++)
            {
                Debug.WriteLine("SyndicationItem " + feed.Items.ElementAt<SyndicationItem>(i).Links.First<SyndicationLink>().Uri.AbsoluteUri);
                Debug.WriteLine("SyndicationTitle " + feed.Items.ElementAt<SyndicationItem>(i).Title.Text);

                rssListItems.Add(new RSSItemDetail() { Status=0,
                                                       URI = feed.Items.ElementAt<SyndicationItem>(i).Links.First<SyndicationLink>().Uri.AbsoluteUri,
                                                       Title = feed.Items.ElementAt<SyndicationItem>(i).Title.Text});
            }
        }

    }
}
