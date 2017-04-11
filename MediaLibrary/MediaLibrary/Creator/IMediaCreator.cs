using MediaLibrary.Interfaces;

namespace MediaLibrary.Creator
{
    public interface IMediaCreator
    {
        IMediaItem CreateItem(string nameOrPath);
    }
}
