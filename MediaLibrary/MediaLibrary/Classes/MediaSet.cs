using MediaLibrary.Creator;
using MediaLibrary.Interfaces;
using System;
using System.Collections.Generic;

namespace MediaLibrary.Classes
{
    public abstract class MediaSet
    {
        public string Name { get; set; }
        public List<IMediaItem> Items { get; protected set; }
        protected IMediaCreator _itemCreator;

        public void Play()
        {
            if (Items.Count != 0)
            {
                Console.WriteLine("Playing...");

                foreach (var item in Items)
                {
                    item.Play();
                }

                Console.WriteLine("Playing finished...");
            }
            else
            {
                Console.WriteLine("Playlist is empty...");
            }
        }

        public void ShowPlaylist()
        {
            foreach (var item in Items)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
