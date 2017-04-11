using MediaLibrary.Interfaces;
using System;

namespace MediaLibrary.Classes
{
    public class VideoUrl : IVideoItem
    {
        public string Name { get; set; }

        public VideoUrl() { }

        public VideoUrl(string name)
        {
            Name = name;
        }

        public void Play()
        {
            Console.WriteLine("Downloading and playing video {0}.mp4", Name);
        }
    }
}
