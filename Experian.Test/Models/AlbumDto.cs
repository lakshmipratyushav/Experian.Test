using System.Collections.Generic;

namespace Experian.Test.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AlbumDto
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }

        public List<PhotoDto> PhotosList { get; set; }
    }

    public class UserAlbumPhoto
    {
        public int UserId { get; set; }

        public List<AlbumDto> AlbumList {get;set;}
    }
}