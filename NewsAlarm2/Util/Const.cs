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

namespace NewsAlarm2.Util
{
    public class Const
    {
        public enum DISPLAY_TILES { ApplicationTile, SecondaryTile };

        //Application Tile
        public static String APP_TILE_FOR_TITLE = "News Alarm";
        public static String APP_TITL_BACK_TITLE = "Life Style";

        public static String APP_TILE_FOR_IMG = "Resource/Image/Tile/tile.jpg";
        public static String APP_TILE_BACK_IMG = "Resource/Image/Tile/tileflip.jpg";

        public static String APP_TITE_BACK_CONTENT = "Wonderful Day Starts from Here!";

        public static int SELECT_RADIO = 0;
        public static int SELECT_MUSIC = 1;
        public static int SELECT_AGENDA = 2;
        public static int SELECT_RSS = 3;

        public static string PLAY_AUDIO = "PLAY_AUDIO";
        
        public static String WSJ_URL = "http://feeds.wsjonline.com/~r/wsj/podcast_wall_street_journal_this_morning/~3/bxT1azerDJI/pod-wsjtmit.mp3";

        
    }
}
