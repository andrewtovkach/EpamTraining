using Mediateka.Model;
using Mediateka.Collections;
using System;

namespace Mediateka
{
    class Program
    {
        static void Main(string[] args)
        {
            MediatekaClass mediateka = new MediatekaClass("MyMediateka", DateTime.Now, false)
            {
                new Folder("Music", new DateTime(2015, 3, 5), false),
                new Folder("Video and pictures", new DateTime(2015, 3, 6), false)
            };
            mediateka[0].Add(new AudioItem("WT - Fire and Ice", 12.5, 
               new DateTime(2014, 10, 15), Popularity.Five, new TimeSpan(0, 5, 3), 320));
            Album album = new Album("Bundle", new DateTime(2011, 3, 2), "System of a down")
            {
                new AudioItem("SOAD - Suite-Pee", 13.6, new DateTime(2015, 1, 4), Popularity.Five,
                    new TimeSpan(0, 3, 20), 320),
                new AudioItem("SOAD - Know", 3.56, new DateTime(2015, 2, 1), Popularity.Four,
                    new TimeSpan(0, 4, 21), 320),
                new AudioItem("SOAD - Sugar", 1.26, new DateTime(2013, 6, 4), Popularity.Five,
                    new TimeSpan(0, 3, 24), 320)
            };
            mediateka["Music"].Add(album);
            Serial serial = new Serial("Game of Thrones", new DateTime(2010, 2, 6), "HBO", "USA")
            {
                {
                    5, new VideoItem("GameOfThrones s5e3", 1009.08, new DateTime(2015, 6, 9),
                        Popularity.Three, new TimeSpan(1, 7, 0), new ScreenResolution {Height = 1280, Width = 720})
                },
                {
                    5, new VideoItem("GameOfThrones s5e2", 1230.02, new DateTime(2015, 6, 5),
                        Popularity.Four, new TimeSpan(1, 6, 8), new ScreenResolution {Height = 1280, Width = 720})
                },
                {
                    4, new VideoItem("GameOfThrones s4e1", 1125.01, new DateTime(2014, 6, 5),
                        Popularity.Five, new TimeSpan(1, 7, 10), new ScreenResolution {Height = 1280, Width = 720})
                }
            };
            mediateka[1].Add(serial);
            ImageItem item1 = new ImageItem("waterfall 1_1280x720", 0.124, new DateTime(2015, 1, 4), 
                Popularity.One, new ScreenResolution { Height = 1280, Width = 720 });
            mediateka[1].Add(item1);
            ImageItem item2 = new ImageItem("waterfall 2_1280x720", 0.129, new DateTime(2015, 1, 4), 
                Popularity.One, new ScreenResolution { Height = 1280, Width = 720 });
            mediateka.Add(serial);
            mediateka.Add(item2);
            ImageItem item3 = new ImageItem("waterfall 3_1280x720", 0.245, new DateTime(2015, 1, 4), 
                Popularity.Two, new ScreenResolution { Height = 1280, Width = 720 });
            mediateka.Add(item3);
            Console.SetWindowSize(120, 50);
            Console.WriteLine(mediateka.Print());
            Console.WriteLine("\n->Наиболее популярные<- \n");
            foreach (var item in mediateka.GetMostPopularMediaItems(2))
                Console.WriteLine(item);
            Console.WriteLine("\n->Самые новые<- \n");
            foreach (var item in mediateka.GetNewMediaItems(2))
                Console.WriteLine(item);
        }
    }
}
