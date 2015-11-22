using System.IO;

namespace BL
{
    public struct FileInformation
    {
        public string FullPath;
        public WatcherChangeTypes ChangeType;

        public override string ToString()
        {
            return FullPath + " " + ChangeType;
        }
    }
}
