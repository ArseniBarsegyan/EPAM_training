using MediaLibrary.Classes;
using MediaLibrary.Creator;
using MediaLibrary.Interfaces;
using System.Collections.Generic;

namespace MediaLibrary.MediaCollections
{
    //
    //this class can only contain fixed list of audio and image items without any methods
    //
    public class Disk : MediaSet
    {
        public Disk(string name)
        {
            Name = name;
            FillDiskByDefault();
        }

        public Disk(string name, IList<AudioTrack> audios, IList<Image> images)
        {
            Name = name;
            Items = new List<IMediaItem>();
            Items.AddRange(audios);
            Items.AddRange(images);
        }

        private void FillDiskByDefault()
        {
            Items = new List<IMediaItem>();

            _itemCreator = new AudioCreator();
            Items.Add(_itemCreator.CreateItem("AudioTrack01"));

            _itemCreator = new ImageCreator();
            Items.Add(_itemCreator.CreateItem("Image01"));
        }
    }
}
