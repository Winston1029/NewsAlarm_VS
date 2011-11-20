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
using Microsoft.Phone.Shell;

namespace NewsAlarm2.Util
{
    public class Tiles
    {
        ShellTileSchedule SampleTileSchedule;

        public Tiles()
        {
            SampleTileSchedule = new ShellTileSchedule();
        }

        private void oneTimeTile()
        {
            SampleTileSchedule.Recurrence = UpdateRecurrence.Onetime;
            SampleTileSchedule.StartTime = DateTime.Now;
            SampleTileSchedule.RemoteImageUri = new Uri("/Resource/Tiles/mostly_cloudy.png");
            SampleTileSchedule.Start();
        }
    }
}
