using MediaLibrary.Classes;
using MediaLibrary.Creator;
using MediaLibrary.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MediaLibrary.MediaCollections
{
    //
    //this can only contain audio and photo items with ChangeElement functionality
    //
    public class Compilation : Disk
    {
        public Compilation(string name) : base(name)
        {

        }

        public Compilation(string name, IList<AudioTrack> audios, IList<Image> images) 
            : base (name, audios, images)
        {

        }

        //
        //Create
        public void AddAudio(string name)
        {
            _itemCreator = new AudioCreator();
            Items.Add(_itemCreator.CreateItem(name));
        }

        public void AddImage(string name)
        {
            _itemCreator = new ImageCreator();
            Items.Add(_itemCreator.CreateItem(name));
        }

        //
        //Update
        public void SwapItem(string oldItemName, IMediaItem newItem)
        {
            var oldItem = Items.FirstOrDefault(x => x.Name.Equals(oldItemName));
            Items.Remove(oldItem);
            Items.Add(newItem);
        }

        //
        //Delete
        public void RemoveItem(string itemName)
        {
            var item = Items.FirstOrDefault(x => x.Name.Equals(itemName));
            Items.Remove(item);
        }

        public void RemoveAll()
        {
            Items = new List<IMediaItem>();
        }
    }
}
