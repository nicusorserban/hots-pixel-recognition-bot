using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using hots.Bot;

namespace hots.Game
{
    public class Element
    {
        private static readonly ScreenCapture ScreenCapture = new ScreenCapture();
        private readonly Process _process;
        private Bitmap image;

        public Element()
        {
            _process = (from proc in Process.GetProcesses()
                        where proc.ProcessName == "HeroesOfTheStorm_x64"
                        select proc).First();
        }

        public void GetElementsStatus()
        {
            while (true)
            {
                image = new Bitmap(ScreenCapture.CaptureWindow(_process.MainWindowHandle));

                if (Status.Current != "in-game")
                {
                    GetGameBarStatus();
                }

                if (Status.Current == "play")
                {
                    GetGameTypeStatus();

                    if (Config.GameType == "versus-ai")
                    {
                        Mouse.Click(_process.MainWindowHandle, Positions.ChangeHero);
                        //SendMessage(hWndFour, (uint)WMessages.WM_LBUTTONDOWN, IntPtr.Zero, (IntPtr)MAKELPARAM(10, 10));
                    }
                }

                if(Config.Debug)
                    Console.WriteLine(Status.Current + " | " + Status.CurrentPlayType);

                Thread.Sleep(500);
            }
        }

        public void GetGameBarStatus()
        {
            var sw = new Stopwatch();
            sw.Start();

            while (true)
            {
                if (sw.ElapsedMilliseconds > 1000)
                    return;

                if (Config.Debug)
                    Console.WriteLine("Getting Game Bar Status");

                if (Status.Current == "in-game") return;

                var homePixel = image.GetPixel(Coordinates.Home.X, Coordinates.Home.Y);
                if (homePixel.A == 255 && homePixel.B == 246 && homePixel.G == 205 && homePixel.R == 221)
                {
                    Status.Current = "home";
                    Status.CurrentPlayType = string.Empty;
                    return;
                }

                var playPixel = image.GetPixel(Coordinates.Play.X, Coordinates.Play.Y);
                if (playPixel.A == 255 && playPixel.B == 246 && playPixel.G == 208 && playPixel.R == 221)
                {
                    Status.Current = "play";
                    return;
                }

                var collectionPixel = image.GetPixel(Coordinates.Collection.X, Coordinates.Collection.Y);
                if (collectionPixel.A == 255 && collectionPixel.B == 246 && collectionPixel.G == 194 &&
                    collectionPixel.R == 213)
                {
                    Status.Current = "collection";
                    Status.CurrentPlayType = string.Empty;
                    return;
                }

                var lootPixel = image.GetPixel(Coordinates.Loot.X, Coordinates.Loot.Y);
                if (lootPixel.A == 255 && lootPixel.B == 244 && lootPixel.G == 168 && lootPixel.R == 187)
                {
                    Status.Current = "loot";
                    Status.CurrentPlayType = string.Empty;
                    return;
                }

                var watchPixel = image.GetPixel(Coordinates.Watch.X, Coordinates.Watch.Y);
                if (watchPixel.A == 255 && watchPixel.B == 235 && watchPixel.G == 151 && watchPixel.R == 170)
                {
                    Status.Current = "watch";
                    Status.CurrentPlayType = string.Empty;
                    return;
                }

                Thread.Sleep(100);
            }
        }

        public void GetGameTypeStatus()
        {
            var sw = new Stopwatch();
            sw.Start();

            while (true)
            {
                if (sw.ElapsedMilliseconds > 1000)
                    return;

                if (Config.Debug)
                    Console.WriteLine("Getting Game Type Status");

                if (Status.Current != "play") return;

                var versusAiPixel = image.GetPixel(Coordinates.VersusAi.X, Coordinates.VersusAi.Y);
                if (versusAiPixel.A == 255 && versusAiPixel.B == 246 && versusAiPixel.G == 210 &&
                    versusAiPixel.R == 221)
                {
                    Status.CurrentPlayType = "versus-ai";
                    return;
                }

                var quickMatchPixel = image.GetPixel(Coordinates.QuickMatch.X, Coordinates.QuickMatch.Y);
                if (quickMatchPixel.A == 255 && quickMatchPixel.B == 246 && quickMatchPixel.G == 206 &&
                    quickMatchPixel.R == 221)
                {
                    Status.CurrentPlayType = "quick-match";
                    return;

                }

                var unrankedPixel = image.GetPixel(Coordinates.Unranked.X, Coordinates.Unranked.Y);
                if (unrankedPixel.A == 255 && unrankedPixel.B == 246 && unrankedPixel.G == 210 &&
                    unrankedPixel.R == 221)
                {
                    Status.CurrentPlayType = "unrakned";
                    return;
                }

                var rankedPixel = image.GetPixel(Coordinates.Ranked.X, Coordinates.Ranked.Y);
                if (rankedPixel.A == 255 && rankedPixel.B == 246 && rankedPixel.G == 210 &&
                    rankedPixel.R == 221)
                {
                    Status.CurrentPlayType = "ranked";
                    return;
                }

                var brawlPixel = image.GetPixel(Coordinates.Brawl.X, Coordinates.Brawl.Y);
                if (brawlPixel.A == 255 && brawlPixel.B == 235 && brawlPixel.G == 139 &&
                    brawlPixel.R == 162)
                {
                    Status.CurrentPlayType = "brawl";
                    return;
                }

                var customGamesPixel = image.GetPixel(Coordinates.CustomGames.X, Coordinates.CustomGames.Y);
                if (customGamesPixel.A == 255 && customGamesPixel.B == 246 && customGamesPixel.G == 202 &&
                    customGamesPixel.R == 213)
                {
                    Status.CurrentPlayType = "custom-games";
                    return;
                }

                Thread.Sleep(100);
            }
        }

    }
}