using System;

namespace Mediateka.Model
{
    class ImageItem : MediaItem
    {
        public ScreenResolution ScreenResolution { get; set; }

        public ImageItem(string name, double sizeFile, DateTime creationDate,
            Popularity popularity, ScreenResolution screenResolution)
            : base(name, sizeFile, creationDate, popularity)
        {
            this.ScreenResolution = screenResolution;
        }

        public override string ToString()
        {
            return String.Format((char)15  + " {0}, {1}x{2}", base.ToString(), ScreenResolution.Height,
                ScreenResolution.Width);
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
