using MediaLibrary.Creator;
using MediaLibrary.Interfaces;
using MediaLibrary.MediaCollections;
using MediaLibrary.Player;
using System;

namespace MediaLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating a player and load compilation at player
            MediaPlayer player = new SonyPlayer("Sony player");
            Compilation compilation = new Compilation("myCompilation");
            player.LoadCollection(compilation);

            //Playing all collection
            player.Play();
            Console.WriteLine("------");

            //trying to remove one item and play compilation again
            compilation.RemoveItem("Image01");
            player.Play();
            Console.WriteLine("------");

            //trying to remove all items from compilation and play
            compilation.RemoveAll();
            Console.WriteLine("---Filling items list again---");
            player.Play();

            //trying to add image to compilation and swap this image to a new image
            compilation.AddImage("Image01");
            IImageItem newImage = (IImageItem) new ImageCreator().CreateItem("Image02");
            compilation.SwapItem("Image01", newImage);
            player.Play();
        }
    }
}
