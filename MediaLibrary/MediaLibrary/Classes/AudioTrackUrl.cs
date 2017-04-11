using MediaLibrary.Interfaces;
using System;

namespace MediaLibrary.Classes
{
    public class AudioTrackUrl : IAudioItem
    {
        public string Name { get; set; }

        public AudioTrackUrl() { }

        public AudioTrackUrl(string name)
        {
            Name = name;
        }

        public void Play()
        {
            Console.WriteLine("Downloading and playing audio {0}.mp3", Name);
        }
    }
}
