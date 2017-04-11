using MediaLibrary.Classes;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Creator
{
    public class AudioCreator : IMediaCreator
    {
        public IMediaItem CreateItem(string nameOrPath)
        {
            return new AudioTrack(nameOrPath);
        }
    }
}
