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
using System.Linq;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls;

namespace NewsAlarm2.Util
{
    public class TileDisplay 
    {
        private Const.DISPLAY_TILES tileToDisplay;
        private Boolean isSecTilePined;
        private ShellTile applicationTile;
        private ShellTile secondaryTile;

        public TileDisplay()
        {
            isSecTilePined = false;
        }

        public void DisplayApplicationTile(String title)
        {
            applicationTile = ShellTile.ActiveTiles.First();

            if (applicationTile != null)
            {
                StandardTileData NewTileData = new StandardTileData
                {
                    Title = title,
                    BackgroundImage = new Uri(Const.APP_TILE_FOR_IMG, UriKind.Relative),
                    Count = 0,
                    BackTitle = Const.APP_TITL_BACK_TITLE,
                    BackBackgroundImage = new Uri(Const.APP_TILE_BACK_IMG, UriKind.Relative),
                    BackContent = Const.APP_TITE_BACK_CONTENT
                };
                applicationTile.Update(NewTileData);
            }

        }

        private void CheckSecondaryTile(String pagePath)
        {
            secondaryTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(pagePath));

            isSecTilePined = (secondaryTile != null);
        }

        public void DisplaySecondaryTile(StandardTileData stdTileData,String pagePath)
        {
             CheckSecondaryTile(pagePath);

            if (!isSecTilePined)
            {
                ShellTile.Create(new Uri(pagePath, UriKind.Relative),stdTileData);
            }

        }

        public void UpdateSecondaryTile(String pagePath,StandardTileData stdTileData)
        {
            CheckSecondaryTile(pagePath);

            if (isSecTilePined)
            {
                secondaryTile.Update(stdTileData);
            }
        }
    }
}
