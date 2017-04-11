using MediaLibrary.Interfaces;
using System;

namespace MediaLibrary.Classes
{
    public class ImageUrl : IImageItem
    {
        public string Name { get; set; }

        public ImageUrl() { }

        public ImageUrl(string name)
        {
            Name = name;
        }

        public void Play()
        {
            Console.WriteLine("Downloading and showing image {0}.png", Name);
        }
    }
}
