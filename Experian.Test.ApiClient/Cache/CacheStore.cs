using Experian.Test.ApiClient.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experian.Test.ApiClient.Cache
{
    public interface ICacheStore
    {
        Task<List<Albums>> GetAlbumsByUserIdAsync(int userId);
        Task<List<Photos>> GetPhotosByAlbumIdAsync(int albumId);
    }

    public class CacheStore : ICacheStore
    {
        private readonly IAlbumsRestClient _albumsRestClient;

        private static readonly List<Albums> Albums = new List<Albums>();
        private static readonly List<Photos> Photos = new List<Photos>();
        public CacheStore(IAlbumsRestClient albumsRestClient)
        {
            _albumsRestClient = albumsRestClient;
        }

        private async Task FillCacheAlbumsStore()
        {
            IEnumerable<Albums> albums = await _albumsRestClient.GetAlbumsAsync();
            Albums.AddRange(albums);
        }

        public async Task<List<Albums>> GetAlbumsByUserIdAsync(int userId)
        {
            if (Albums.Any())
            {
                return Albums.Where(x => x.UserId == userId).ToList();
            }

            await FillCacheAlbumsStore();

            if (Albums.Any())
            {
                return Albums.Where(x => x.UserId == userId).ToList();
            }

            return new List<Albums>();
        }

        public async Task FillCachePhotosStore()
        {
            IEnumerable<Photos> photos = await _albumsRestClient.GetPhotos();
            Photos.AddRange(photos);
        }
        public async Task<List<Photos>> GetPhotosByAlbumIdAsync(int albumId)
        {
            if (Photos.Any())
            {
                return Photos.Where(x => x.AlbumId == albumId).ToList();
            }

            await FillCachePhotosStore();

            if (Photos.Any())
            {
                return Photos.Where(x => x.AlbumId == albumId).ToList();
            }

            return new List<Photos>();
        }
    }
}
