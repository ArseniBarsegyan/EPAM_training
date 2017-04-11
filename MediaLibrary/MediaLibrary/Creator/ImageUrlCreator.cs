using MediaLibrary.Interfaces;
using MediaLibrary.Classes;

namespace MediaLibrary.Creator
{
    class ImageUrlCreator : IMediaCreator
    {
        public IMediaItem CreateItem(string nameOrPath)
        {
            return new ImageUrl(nameOrPath);
        }
    }
}
