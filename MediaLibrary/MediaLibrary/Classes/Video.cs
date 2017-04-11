using MediaLibrary.Interfaces;
using System;

namespace MediaLibrary.Classes
{
    public class Video : IVideoItem
    {
        public string Name { get; set; }

        public Video() { }

        public Video(string name)
        {
            Name = name;
        }

        public void Play()
        {
            Console.WriteLine("Playing video {0}.mp4", Name);
        }
    }
}
