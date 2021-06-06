using Experian.Test.ApiClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using System.Threading.Tasks;
using System.Net.Http;
using Experian.Test.ApiClient.Json;
using Castle.Core.Logging;
using Experian.Test.ApiClient.Cache;
using Experian.Test.API.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Experian.Test.UnitTest
{
    [TestClass]
    public class AlbumControllerTests
    {
        private Mock<ILogger<AlbumApiController>> _loggerMock;
        private Mock<ICacheStore> _cacheStoreMock;
        public AlbumControllerTests()
        {
            _loggerMock = new Mock<ILogger<AlbumApiController>>();
            _cacheStoreMock = new Mock<ICacheStore>();
        }

        [TestMethod]
        public async Task Verify_Method_GetAlbumsAndPhotosByUserAsync()
        {
            //DATA
            int userId = 1;
            List<Albums> albums = TestHelper.GetTestAlbums();
            List<Photos> photosByAlbums = TestHelper.GetTestPhotos();

            //BEHAVIOUR
            _cacheStoreMock.Setup(c => c.GetAlbumsByUserIdAsync(1)).ReturnsAsync(albums);
            _cacheStoreMock.Setup(c => c.GetPhotosByAlbumIdAsync(1)).ReturnsAsync(photosByAlbums);

            //TEST
            AlbumApiController albumApiController = new AlbumApiController(_loggerMock.Object, _cacheStoreMock.Object);
            IActionResult actionResult = await albumApiController.GetAlbumsAndPhotosByUserAsync(userId);
            var okResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsTrue(okResult is OkObjectResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}
