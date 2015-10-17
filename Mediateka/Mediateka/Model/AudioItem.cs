using System;

namespace Mediateka.Model
{
    class AudioItem : MediaItem
    {
        public TimeSpan Duration { get; set; }
        public int Bitrate { get; set; }

        public AudioItem(string name, double sizeFile, DateTime creationDate,  
            Popularity popularity, TimeSpan duration, int bitrate)
            : base(name, sizeFile, creationDate, popularity)
        {
            this.Duration = duration;
            this.Bitrate = bitrate;
        }

        public override string ToString()
        {
            return String.Format((char)14 + " {0} Продолж.: {1}, {2} бит/с", base.ToString(),
                Duration, Bitrate);
        }

        public override string Print(int indent)
        {
            indent += 3;
            return new string(' ', indent) + this;
        }

        public override double TotalSize
        {
            get { return SizeFile; }
        }

        public override int AvgPopularity
        {
            get { return (int)Popularity; }
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
