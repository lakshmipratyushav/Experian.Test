using Experian.Test.ApiClient;
using Experian.Test.ApiClient.Cache;
using Experian.Test.ApiClient.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace Experian.Test.UnitTest
{
    [TestClass]
    public class CacheStoreTests
    {
        private Mock<IAlbumsRestClient> _albumsRestClientMock;        

        public CacheStoreTests()
        {
            _albumsRestClientMock = new Mock<IAlbumsRestClient>();
        }

        [TestMethod]
        public async Task Verify_Method_GetAlbumsByUserIdAsync_Gets_Albums_By_UserId()
        {
            //DATA
            var albums = TestHelper.GetTestAlbums();
            //BEHAVIOUR
             _albumsRestClientMock.Setup(a => a.GetAlbumsAsync()).ReturnsAsync(albums);
            //TEST
            ICacheStore cacheStore = new CacheStore(_albumsRestClientMock.Object);

            List<Albums> actuAlbums = await cacheStore.GetAlbumsByUserIdAsync(21);

            Assert.AreEqual(1, actuAlbums.Count);
        }

        [TestMethod]
        public async Task Verify_GetPhotosByAlbumIdAsync_Method_Gets_Photos_By_AlbumId()
        {
            //DATA
            var photos = TestHelper.GetTestPhotos();
            //BEHAVIOUR
            _albumsRestClientMock.Setup(a => a.GetPhotos()).ReturnsAsync(photos);
            //TEST
            ICacheStore cacheStore = new CacheStore(_albumsRestClientMock.Object);

            List<Photos> actuPhotos = await cacheStore.GetPhotosByAlbumIdAsync(1);

            Assert.AreEqual(2, actuPhotos.Count);
            Assert.AreEqual(1, actuPhotos[1].AlbumId);
        }
    }
}
