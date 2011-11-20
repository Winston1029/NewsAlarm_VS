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

namespace MyAudioPlaybackAgent
{
    public class RSSItemDetail
    {
        private String title;

        private String uri;

        private int status;

        public String Title
        {
            get { return title; }
            set { title = value; }
        }

        public String URI
        {
            get { return uri; }
            set { uri = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
