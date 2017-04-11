using MediaLibrary.Interfaces;
using MediaLibrary.Classes;

namespace MediaLibrary.Creator
{
    class VideoUrlCreator : IMediaCreator
    {
        public IMediaItem CreateItem(string nameOrPath)
        {
            return new VideoUrl(nameOrPath);
        }
    }
}
