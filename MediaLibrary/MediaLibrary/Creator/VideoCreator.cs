using MediaLibrary.Interfaces;
using MediaLibrary.Classes;

namespace MediaLibrary.Creator
{
    public class VideoCreator : IMediaCreator
    {
        public IMediaItem CreateItem(string nameOrPath)
        {
            return new Video();
        }
    }
}
