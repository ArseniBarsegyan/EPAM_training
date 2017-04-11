using MediaLibrary.Interfaces;
using System;

namespace MediaLibrary.Classes
{
    public class AudioTrack : IAudioItem
    {
        public string Name { get; set; }

        public AudioTrack() { }

        public AudioTrack(string name)
        {
            Name = name;
        }

        public void Play()
        {
            Console.WriteLine("Playing audio {0}.mp3", Name);
        }
    }
}
