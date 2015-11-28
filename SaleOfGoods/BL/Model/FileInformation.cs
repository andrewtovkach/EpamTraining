using System.IO;

namespace BL.Model
{
    public struct FileInformation
    {
        public string FullPath;
        public WatcherChangeTypes ChangeType;

        public override string ToString()
        {
            return string.Format("{0} {1}", FullPath, ChangeType);
        }
    }
}
