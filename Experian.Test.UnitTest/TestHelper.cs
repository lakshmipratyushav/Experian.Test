using System;
using System.Collections.Generic;
using System.Text;
using Experian.Test.ApiClient.Json;

namespace Experian.Test.UnitTest
{
    class TestHelper
    {
        public static List<Albums> GetTestAlbums()
        {
            List<Albums> albums = new List<Albums>
            {
                new Albums
                {
                    Id = 1,
                    UserId = 21
                },

                  new Albums
                {
                    Id = 1,
                    UserId = 22
                }
            };
            return albums;
        }

        public static List<Photos> GetTestPhotos()
        {
            List<Photos> photos = new List<Photos>
            {
                new Photos
                {
                    Id = 1,
                    AlbumId = 1,
                    Url= "https://via.placeholder.com/600/771796",
                    ThumbnailUrl= "https://via.placeholder.com/150/771796"
                },

                  new Photos
                {
                    Id = 1,
                    AlbumId = 1,
                    Url= "https://via.placeholder.com/600/771796",
                    ThumbnailUrl= "https://via.placeholder.com/150/771796"
                }
            };
            return photos;
        }
    }
}
