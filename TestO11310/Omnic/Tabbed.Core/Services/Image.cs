
using System;

namespace OmnicTabs.Core.Services
{
   public class Image
    {
        public string Url { get; set; }
    }

   public class ImageLoader : IImageService
   {
       public Image ImageFactory()
       {
           try
           {
               return new Image {Url = string.Format("http://lorempixel.com/{0}/{0}", Random(20) + 300)};
           }
           catch (Exception e)
           {
               //something
               return  new Image();
           }
          
       }
       readonly System.Random _random = new System.Random();
       protected int Random(int count)
       {
           return _random.Next(count);
       }
   }
}
