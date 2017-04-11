using MediaLibrary.MediaCollections;

namespace MediaLibrary.Player
{
    //
    //this class is a real type of MediaPlayer
    //
    public class SonyPlayer : MediaPlayer
    {
        public SonyPlayer(string name)
        {
            Name = name;
        }

        public SonyPlayer(MediaSet set)
        {
            Set = set;
        }
    }
}
