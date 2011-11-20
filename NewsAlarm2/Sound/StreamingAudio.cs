using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Microsoft.Phone.BackgroundAudio;
using System.Collections.Generic;

namespace NewsAlarm2.Sound
{
    public class StreamingAudio
    {
        private void StreamAudio()
        {
            BackgroundAudioPlayer.Instance.PlayStateChanged += new EventHandler(Instance_PlayStateChanged);
        }

        void Instance_PlayStateChanged(object sender, EventArgs e)
        {
            MainPage.remindTxt = BackgroundAudioPlayer.Instance.Track.Title;
        }



        public static void playAudio()
        {
            // Tell the backgound audio agent to play the current track.

            BackgroundAudioPlayer.Instance.Play();
        }

        public static void pauseAudio()
        {
            // Tell the backgound audio agent to play the current track.
            BackgroundAudioPlayer.Instance.Pause();
        }

        public static void nextAudio()
        {
            BackgroundAudioPlayer.Instance.SkipNext();
        }
    }
}
