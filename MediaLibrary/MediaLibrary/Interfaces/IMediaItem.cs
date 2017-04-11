namespace MediaLibrary.Interfaces
{
    public interface IMediaItem
    {
        string Name { get; set; }
        void Play();
    }
}
