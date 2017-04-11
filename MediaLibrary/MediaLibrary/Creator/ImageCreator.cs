using MediaLibrary.Interfaces;
using MediaLibrary.Classes;

namespace MediaLibrary.Creator
{
    public class ImageCreator : IMediaCreator
    {
        public IMediaItem CreateItem(string nameOrPath)
        {
            return new Image();
        }
    }
}
