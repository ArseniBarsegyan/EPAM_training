using MediaLibrary.Classes;
using MediaLibrary.Creator;
using MediaLibrary.Interfaces;
using System.Collections.Generic;

namespace MediaLibrary.MediaCollections
{
    //
    //this class can only contain video and image elements
    //
    public class Serial : MediaSet
    {
        public Serial(string name)
        {
            Name = name;
            Items = new List<IMediaItem>();
        }

        public Serial(string name, IList<Video> videos, IList<Image> images)
        {
            Items = new List<IMediaItem>();
            Items.AddRange(videos);
            Items.AddRange(images);
        }

        public void AddVideo(string name)
        {
            _itemCreator = new VideoCreator();
            Items.Add(_itemCreator.CreateItem(name));
        }

        public void AddImage(string name)
        {
            _itemCreator = new ImageCreator();
            Items.Add(_itemCreator.CreateItem(name));
        }
    }
}
