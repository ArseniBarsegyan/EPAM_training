using MediaLibrary.Interfaces;
using MediaLibrary.Classes;

namespace MediaLibrary.Creator
{
    public class AudioUrlCreator : IMediaCreator
    {
        public IMediaItem CreateItem(string nameOrPath)
        {
            return new AudioTrackUrl();
        }
    }
}
