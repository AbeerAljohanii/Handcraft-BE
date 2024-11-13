using System.Security.Claims;
using Backend_Teamwork.src.Services.artwork;
using Backend_Teamwork.src.Utils;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.ArtworkDTO;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArtworksController : ControllerBase
    {
        private readonly IArtworkService _artworkService;
        private readonly Cloudinary _cloudinary;

        // Constructor
        public ArtworksController(IArtworkService service, Cloudinary cloudinary)
        {
            _artworkService = service;
            _cloudinary = cloudinary;
        }

        // Create
        // End-Point: api/v1/artworks
        [HttpPost]
        [Authorize(Roles = "Artist")]
        public async Task<ActionResult<ArtworkReadDto>> CreateOne(
            [FromForm] ArtworkCreateDto createDto,
            [FromForm] IFormFile ArtworkUrl
        )
        {
            // extract user information
            var authenticateClaims = HttpContext.User;
            // get user id from claim
            var userId = authenticateClaims
                .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!
                .Value;
            // string => guid
            var userGuid = new Guid(userId);

            // Handle image upload
            string imageUrl = null;
            if (ArtworkUrl != null)
            {
                imageUrl = await UploadImageAsync(ArtworkUrl, "artworks");
            }
            else
            {
                Console.WriteLine("No image received");
            }

            // Create artwork with the image URL if image is uploaded
            var createdArtwork = await _artworkService.CreateOneAsync(
                userGuid,
                createDto,
                imageUrl
            );
            //return Created(url, createdArtwork);
            return Ok(createdArtwork);
        }

        private async Task<string> UploadImageAsync(IFormFile image, string folder)
        {
            var uploadResult = await _cloudinary.UploadAsync(
                new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                    Folder = folder,
                }
            );
            return uploadResult?.SecureUrl?.ToString();
        }

        // Get all
        // End-Point: api/v1/artworks
        [HttpGet]
        public async Task<ActionResult<List<ArtworksListDto>>> GetAll(
            [FromQuery] PaginationOptions paginationOptions
        )
        {
            var artworkList = await _artworkService.GetAllAsync(paginationOptions);
            var totalCount = await _artworkService.GetTotalArtworksCountAsync();
            var response = new ArtworksListDto { Artworks = artworkList, TotalCount = totalCount };
            return Ok(response);
        }

        // Get all artworks for the currently logged-in artist
        // End-Point: api/v1/artworks/my-artworks
        [HttpGet("my-artworks")]
        [Authorize(Roles = "Artist")]
        public async Task<ActionResult<List<ArtworkReadDto>>> GetMyArtworks()
        {
            // extract user information
            var authenticateClaims = HttpContext.User;
            // get user id from claim
            var userId = authenticateClaims
                .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!
                .Value;
            // string => guid
            var userGuid = new Guid(userId);

            var artworkList = await _artworkService.GetByArtistIdAsync(userGuid);
            return Ok(artworkList);
        }

        // Get by artwork id
        // End-Point: api/v1/artworks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtworkReadDto>> GetById([FromRoute] Guid id)
        {
            var artwork = await _artworkService.GetByIdAsync(id);
            return Ok(artwork);
        }

        // Get by artist Id
        // End-Point: api/v1/artworks/artist/{id}
        [HttpGet("artist/{artistId}")]
        public async Task<ActionResult<List<ArtworkReadDto>>> GetByArtistId(
            [FromRoute] Guid artistId
        )
        {
            var artwork = await _artworkService.GetByArtistIdAsync(artistId);
            return Ok(artwork);
        }

        // Update
        // End-Point: api/v1/artworks/{id}
        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin,Artist")]
        public async Task<ActionResult> UpdateOne(
            [FromRoute] Guid id,
            [FromBody] ArtworkUpdateDTO updateDTO
        )
        {
            var artwork = await _artworkService.UpdateOneAsync(id, updateDTO);
            return Ok(artwork);
        }

        // Delete
        // End-Point: api/v1/artworks/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Artist")]
        public async Task<ActionResult> DeleteOne([FromRoute] Guid id)
        {
            await _artworkService.DeleteOneAsync(id);
            return NoContent();
        }
    }
}
