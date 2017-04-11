using MediaLibrary.MediaCollections;

namespace MediaLibrary.Player
{
    //
    //this class describes common idea of MediaPlayer
    //
    public abstract class MediaPlayer
    {
        protected string Name { get; set; }
        public MediaSet Set { get; protected set; }

        public void Play()
        {
            Set.Play();
        }

        public void LoadCollection(MediaSet set)
        {
            Set = set;
        }

        public override string ToString()
        {
            return string.Format("Player model: {0}", Name);
        }
    }
}
