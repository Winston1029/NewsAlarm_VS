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
using Microsoft.Phone.Scheduler;
using NewsAlarm2.Util;

namespace NewsAlarm2
{
    public class AlarmSetting
    {
        String name = System.Guid.NewGuid().ToString();

        public void setAlarm(DateTime time)
        {
            Alarm alarm = new Alarm(name);
            alarm.Content = "News Alarm";
            alarm.Sound = new Uri("/Sound/Ringtone/Ring01.wma", UriKind.Relative);
            //alarm.Sound = new Uri(@"http://dl.dropbox.com/u/5758134/sara.mp3");
            alarm.BeginTime = time;
            alarm.ExpirationTime = DateTime.MaxValue;
            alarm.RecurrenceType = RecurrenceInterval.Daily;

            ScheduledActionService.Add(alarm);
        }

        public void setReminder(DateTime time, int reminderIndex)
        {
            string reminderName = "reminder" + reminderIndex;
            Reminder reminder = ScheduledActionService.Find(reminderName) as Reminder;
            if (reminder != null)
            {
                try
                {
                    ScheduledActionService.Remove(reminderName);
                }
                catch (Exception)
                {
                }
            }

            reminder = new Reminder(reminderName);
            reminder.Content = "Your reminder for RSS is Triggered. Please Tap the HERE to play your Feed";
            //alarm.Sound = new Uri(@"http://dl.dropbox.com/u/5758134/sara.mp3");
            reminder.BeginTime = time;
            reminder.ExpirationTime = DateTime.MaxValue;
            reminder.RecurrenceType = RecurrenceInterval.Daily;

            string queryString = "?tab=" + Const.PLAY_AUDIO;
            reminder.NavigationUri = new Uri("/MainPage.xaml" + queryString, UriKind.Relative);
            
            ScheduledActionService.Add(reminder);
        }
    }

    
}
