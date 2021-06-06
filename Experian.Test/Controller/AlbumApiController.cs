using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Experian.Test.API.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Experian.Test.ApiClient.Cache;
using Experian.Test.ApiClient.Json;

namespace Experian.Test.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/albums")]
    [ApiController]
    public class AlbumApiController : ControllerBase
    {
        private readonly ILogger<AlbumApiController> _logger;

        private readonly ICacheStore _cacheStore;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>        
        /// <param name="cacheStore"></param>
        public AlbumApiController(ILogger<AlbumApiController> logger, ICacheStore cacheStore)
        {
            _logger = logger;
            _cacheStore = cacheStore;
        }

        /// <summary>
        /// Get Albums by userId
        /// </summary>
        /// <param name="userId">The userId of the Albums you want to get</param>
        /// <response code="200">Success</response>
        /// <response code="500">Server Error</response>
        [HttpGet("{userId}")]
        [SwaggerOperation("GetAlbumsAndPhotosByUser")]
        [SwaggerResponse(statusCode: 200, type: typeof(Albums), description: "Success")]
        [SwaggerResponse(statusCode: 500, type: typeof(void), description: "Server Error")]
        public virtual async Task<IActionResult> GetAlbumsAndPhotosByUserAsync(int userId)
        {
            try
            {
                IEnumerable<Albums> userAlbums = await _cacheStore.GetAlbumsByUserIdAsync(userId);
                               
                var albumsList = new List<AlbumDto>();
              
                List<UserAlbumPhoto> userAlbumPhotoList = new List<UserAlbumPhoto>();

                var userAlbumDto = new UserAlbumPhoto
                {
                    UserId = userId,
                    AlbumList = new List<AlbumDto>()
                };

                var albumList = new List<AlbumDto>();
                              
                foreach (var userAlbum in userAlbums)
                {
                    var albumDto = new AlbumDto
                    {
                        UserId = userAlbum.UserId,
                        Id = userAlbum.Id,
                        Title = userAlbum.Title,
                        PhotosList = new List<PhotoDto>()
                    };

                    var photolistByAlbumId = await _cacheStore.GetPhotosByAlbumIdAsync(userAlbum.Id);
                    List<PhotoDto> photosList = (from photo in photolistByAlbumId
                                                 let photoDto = new PhotoDto
                                                 {
                                                     Id = photo.Id,
                                                     AlbumId = photo.AlbumId,
                                                     ThumbnailUrl = photo.ThumbnailUrl,
                                                     Url = photo.Url
                                                 }
                                                 select photoDto).ToList();
                    albumDto.PhotosList.AddRange(photosList);
                    userAlbumDto.AlbumList.Add(albumDto);                    
                }

                userAlbumPhotoList.Add(userAlbumDto);
                
                return Ok(userAlbumPhotoList);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting {nameof(GetAlbumsAndPhotosByUserAsync)}", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
    }
}
