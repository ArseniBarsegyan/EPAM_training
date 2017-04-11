using MediaLibrary.Interfaces;
using System;

namespace MediaLibrary.Classes
{
    public class Image : IImageItem
    {
        public string Name { get; set; }

        public Image() { }

        public Image(string name)
        {
            Name = name;
        }

        public void Play()
        {
            Console.WriteLine("Showing image {0}.png", Name);
        }
    }
}
