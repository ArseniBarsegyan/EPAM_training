using MediaLibrary.Creator;
using MediaLibrary.Interfaces;
using System.Collections.Generic;

namespace MediaLibrary.MediaCollections
{
    //
    //this class can only contain video, videoUrl, photo, photoUrl elements
    //
    public class MediaEvent : MediaSet
    {
        public MediaEvent(string name)
        {
            Name = name;
            Items = new List<IMediaItem>();
        }

        public MediaEvent(string name, List<IImageItem> images, List<IVideoItem> videos)
        {
            Name = name;
            Items.AddRange(images);
            Items.AddRange(videos);
        }

        public void AddVideo(string name)
        {
            _itemCreator = new VideoCreator();
            Items.Add(_itemCreator.CreateItem(name));
        }

        public void AddVideoUrl(string path)
        {
            _itemCreator = new VideoUrlCreator();
            Items.Add(_itemCreator.CreateItem(path));
        }

        public void AddImage(string name)
        {
            _itemCreator = new ImageCreator();
            Items.Add(_itemCreator.CreateItem(name));
        }

        public void AddImageUrl(string path)
        {
            _itemCreator = new ImageUrlCreator();
            Items.Add(_itemCreator.CreateItem(path));
        }
    }
}
