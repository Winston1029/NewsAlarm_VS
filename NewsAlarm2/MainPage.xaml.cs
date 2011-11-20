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
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls;
using System.Windows.Threading;

using NewsAlarm2.Util;
using NewsAlarm2.Sound;

namespace NewsAlarm2
{
    public partial class MainPage : PhoneApplicationPage
    {
        private TileDisplay tileDisplay;
        public static String remindTxt;

        public static string snoozeChecked = "100";
        public static string reminderTypeSelected = "0;0;0";
        public static string soundChecked = "100";
        public static string clockTimeSetting = "";

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //initCkbAlarmTime();
            tileDisplay = new TileDisplay();
            tileDisplay.DisplayApplicationTile(Const.APP_TILE_FOR_TITLE);

            initTimer();
            initSettings();
        }

        private void initSettings()
        {
            //case 000, 001, 010, 100, 110, 101, 011, 111
            int snoozeCheckedInt = Convert.ToInt32(snoozeChecked);
            int soundCheckedInt = Convert.ToInt32(soundChecked);

            //snooze setting page
            switch (snoozeCheckedInt)
            {
                case 0:
                    ckb_snooze1.IsChecked = false;
                    ckb_snooze2.IsChecked = false;
                    ckb_snooze3.IsChecked = false;
                    break;
                case 1:
                    ckb_snooze1.IsChecked = true;
                    ckb_snooze2.IsChecked = false;
                    ckb_snooze3.IsChecked = false;
                    break;
                case 10:
                    ckb_snooze1.IsChecked = false;
                    ckb_snooze2.IsChecked = true;
                    ckb_snooze3.IsChecked = false;
                    break;
                case 100:
                    ckb_snooze1.IsChecked = false;
                    ckb_snooze2.IsChecked = false;
                    ckb_snooze3.IsChecked = true;
                    break;
            }

            //sound setting page
            switch (soundCheckedInt)
            {
                case 0:
                    ckb_sound1.IsChecked = false;
                    ckb_sound2.IsChecked = false;
                    ckb_sound3.IsChecked = false;
                    break;
                case 1:
                    ckb_sound1.IsChecked = true;
                    ckb_sound2.IsChecked = false;
                    ckb_sound3.IsChecked = false;
                    break;
                case 10:
                    ckb_sound1.IsChecked = false;
                    ckb_sound2.IsChecked = true;
                    ckb_sound3.IsChecked = false;
                    break;
                case 100:
                    ckb_sound1.IsChecked = false;
                    ckb_sound2.IsChecked = false;
                    ckb_sound3.IsChecked = true;
                    break;
            }

            // time setting page
            // time value
            char[] delimiterChars = { ';' };
            string[] reminderTypeArray = reminderTypeSelected.Split(delimiterChars);

            if ( !clockTimeSetting.Equals("")) {
                
                string[] timevalueString = clockTimeSetting.Split(delimiterChars);
                timePicker1.Value = DateTime.Parse(timevalueString[0]);
                timePicker2.Value = DateTime.Parse(timevalueString[1]);
                timePicker3.Value = DateTime.Parse(timevalueString[2]);
            }
            // time setting
            lstPicker_time1.SelectedIndex = Convert.ToInt32(reminderTypeArray[0]);
            lstPicker_time2.SelectedIndex = Convert.ToInt32(reminderTypeArray[1]);
            lstPicker_time3.SelectedIndex = Convert.ToInt32(reminderTypeArray[2]);
        }


        private void initTimer()
        {
            // timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private void updatePanoTitle()
        {
            this.PanoTitle.Text = String.Format("{0:T}", DateTime.Now) + " " + String.Format("{0:D}", DateTime.Now);
        }

        private void OnCkb_Sound_Radio_Tap(object sender, RoutedEventArgs e)
        {
            Navigation.GoToPage(this, ApplicationPages.SetSound, Const.SELECT_RADIO);
        }

        private void OnCkb_Sound_Music_Tap(object sender, RoutedEventArgs e)
        {
            Navigation.GoToPage(this, ApplicationPages.SetSound, Const.SELECT_MUSIC);
        }

        private void OnCkb_Sound_Agenda_Tap(object sender, RoutedEventArgs e)
        {
            Navigation.GoToPage(this, ApplicationPages.SetSound, Const.SELECT_AGENDA);
        }

        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            StreamingAudio.playAudio();
        }

        private void btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            StreamingAudio.pauseAudio();
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            StreamingAudio.nextAudio();
        }


        public void OnTimerTick (object sender, EventArgs args)
        {
            updatePanoTitle();
        }

		private void HomeBtn_Click(object sender, EventArgs e)
        {
            panorama1.DefaultItem = PanoItem_Home;
        }

        private void SnoozeBtn_Click(object sender, EventArgs e)
        {
            panorama1.DefaultItem = PanoItem_Snooze;
        }

        private void ClockBtn_Click(object sender, EventArgs e)
        {
            panorama1.DefaultItem = PanoItem_Clock;
        }

        private void SoundBtn_Click(object sender, EventArgs e)
        {
            panorama1.DefaultItem = PanoItem_Sound;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            if (parameters.ContainsKey("tab"))
            {
                panorama1.DefaultItem = pano_wakeup;
            }
            setAlarm();
            base.OnNavigatedTo(e);
        }

        private void setAlarm()
        {
            clockTimeSetting = String.Format("{0:t}", timePicker1.Value) + ";"
                 + String.Format("{0:t}", timePicker2.Value) + ";"
                 + String.Format("{0:t}", timePicker3.Value) + ";";

            AlarmSetting alarmSetting = new AlarmSetting();
            if (lstPicker_time1.SelectedIndex != 0)
            {
                DateTime time = (DateTime)timePicker1.Value;
                if (lstPicker_time1.SelectedIndex == 2)
                {
                    alarmSetting.setReminder(time, 1);
                }
                if (lstPicker_time1.SelectedIndex == 1)
                {
                    alarmSetting.setAlarm(time);
                }
                
            }
            if (lstPicker_time2.SelectedIndex != 0)
            {
                DateTime time = (DateTime)timePicker2.Value;
                if (lstPicker_time2.SelectedIndex == 2)
                {
                    alarmSetting.setReminder(time, 2);
                }
                if (lstPicker_time2.SelectedIndex == 1)
                {
                    alarmSetting.setAlarm(time);
                }

            }
            if (lstPicker_time3.SelectedIndex != 0)
            {
                DateTime time = (DateTime)timePicker3.Value;
                if (lstPicker_time3.SelectedIndex == 2)
                {
                    alarmSetting.setReminder(time, 3);
                }
                if (lstPicker_time3.SelectedIndex == 1)
                {
                    alarmSetting.setAlarm(time);
                }

            }

            tileDisplay.DisplayApplicationTile(getNearestTime().ToString());
        }

        public DateTime getNearestTime()
        {
            DateTime nv = System.DateTime.Now;
            DateTime rt = nv;

            if (lstPicker_time1.SelectedIndex != 0 && (DateTime)timePicker1.Value > rt)
                rt = (DateTime)timePicker1.Value;

            if (lstPicker_time2.SelectedIndex != 0 && (DateTime)timePicker2.Value < rt && (DateTime)timePicker2.Value > nv)
                rt = (DateTime)timePicker2.Value;

            if (lstPicker_time3.SelectedIndex != 0 && (DateTime)timePicker3.Value < rt && (DateTime)timePicker3.Value > nv)
                rt = (DateTime)timePicker3.Value;

            return rt;
        }

        private void ckb_snooze_checked(object sender, RoutedEventArgs e)
        {
            string sender_name = (sender as CheckBox).Name;
            switch (sender_name)
            {
                case "ckb_snooze1":
                    ckb_snooze2.IsChecked = false;
                    ckb_snooze3.IsChecked = false;
                    snoozeChecked = "001";
                    break;
                case "ckb_snooze2":
                    ckb_snooze1.IsChecked = false;
                    ckb_snooze3.IsChecked = false;
                    snoozeChecked = "010";
                    break;
                case "ckb_snooze3":
                    ckb_snooze1.IsChecked = false;
                    ckb_snooze2.IsChecked = false;
                    snoozeChecked = "100";
                    break;
            }
        }

        private void ckb_snooze_unchecked(object sender, RoutedEventArgs e)
        {
            snoozeChecked = "000";
        }

        private void ckb_sound_unchecked(object sender, RoutedEventArgs e)
        {
            soundChecked = "000";
        }

        private void ckb_sound_checked(object sender, RoutedEventArgs e)
        {
            string sender_name = (sender as CheckBox).Name;
            switch (sender_name)
            {
                case "ckb_sound1":
                    ckb_sound2.IsChecked = false;
                    ckb_sound3.IsChecked = false;
                    soundChecked = "001";
                    break;
                case "ckb_sound2":
                    ckb_sound1.IsChecked = false;
                    ckb_sound3.IsChecked = false;
                    soundChecked = "010";
                    break;
                case "ckb_sound3":
                    ckb_sound1.IsChecked = false;
                    ckb_sound2.IsChecked = false;
                    soundChecked = "100";
                    break;
            }
        }

        //private void ckb_AlarmTime_Checked(object sender, RoutedEventArgs e)
        //{
        //    string sender_name = (sender as CheckBox).Name;
        //    DateTime? time = DateTime.Now.AddSeconds(-10);
        //    int clockCheckedInt = Convert.ToInt32(reminderTypeSelected);
        //    switch (sender_name)
        //    {
        //        case "ckb_AlarmTime1":
        //            clockCheckedInt += 1;
        //            time = timePicker1.Value;
        //            break;
        //        case "ckb_AlarmTime2":
        //            clockCheckedInt += 10;
        //            time = timePicker2.Value;
        //            break;
        //        case "ckb_AlarmTime3":
        //            clockCheckedInt += 100;
        //            time = timePicker3.Value;
        //            break;
        //    }

        //    AlarmSetting alarmSetting = new AlarmSetting();
        //    alarmSetting.setAlarm((DateTime)time);
        //    tileDisplay.DisplayApplicationTile(getNearestTime().ToString());
        //}

        //private void ckb_AlarmTime_UnChecked(object sender, RoutedEventArgs e)
        //{
        //    string sender_name = (sender as CheckBox).Name;
        //    int clockCheckedInt = Convert.ToInt32(reminderTypeSelected);
        //    switch (sender_name)
        //    {
        //        case "ckb_AlarmTime1":
        //            clockCheckedInt -= 1;
        //            break;
        //        case "ckb_AlarmTime2":
        //            clockCheckedInt -= 10;
        //            break;
        //        case "ckb_AlarmTime3":
        //            clockCheckedInt -= 100;
        //            break;
        //    }
        //    reminderTypeSelected = reminderTypeSelected as string;
        //}

        private void lstPicker_time_changed(object sender, SelectionChangedEventArgs e)
        {
            if (lstPicker_ValueChangedByUser) 
            {
                string sender_name = (sender as ListPicker).Name;
                int selectedIndex = (sender as ListPicker).SelectedIndex;
                char[] delimiterChars = { ';' };
                string[] reminderTypeArray = reminderTypeSelected.Split(delimiterChars);
                switch (sender_name)
                {
                    case "lstPicker_time1":
                        reminderTypeArray[0] = selectedIndex + "";
                        break;
                    case "lstPicker_time2":
                        reminderTypeArray[1] = selectedIndex + "";
                        break;
                    case "lstPicker_time3":
                        reminderTypeArray[2] = selectedIndex + "";
                        break;
                }
                reminderTypeSelected = string.Join(";", reminderTypeArray);
            }
            
        }

        private bool lstPicker_ValueChangedByUser = false;
        private void lstPicker_onTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            lstPicker_ValueChangedByUser = !lstPicker_ValueChangedByUser;
        }
    }
}