using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateka.Model
{
    class VideoItem : MediaItem
    {
        public TimeSpan Duration { get; set; }
        public ScreenResolution ScreenResolution { get; set; }

        public VideoItem() { }

        public VideoItem(string name, double sizeFile, DateTime creationDate,
            Popularity popularity, TimeSpan duration, ScreenResolution screenResolution)
            : base(name, sizeFile, creationDate, popularity)
        {
            this.Duration = duration;
            this.ScreenResolution = screenResolution;
        }

        public override string ToString()
        {
            return String.Format((char)16 + " {0} Продолж.: {1}, {2}x{3}", base.ToString(), Duration, ScreenResolution.Height,
                ScreenResolution.Width);
        }

        public override string Print(int indent)
        {
            indent += 3;
            return new string(' ', indent) + this.ToString();
        }

        public override double TotalSize
        {
            get { return SizeFile; }
        }

        public override int AvgPopularity
        {
            get { return (int) this.Popularity; }
        }

        public override void Add(MediaItem component)
        {
            throw new InvalidOperationException();
        }

        public override bool Remove(MediaItem component)
        {
            throw new InvalidOperationException();
        }
    }
}
