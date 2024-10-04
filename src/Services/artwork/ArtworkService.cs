using AutoMapper;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Repository;
using Backend_Teamwork.src.Utils;
using static Backend_Teamwork.src.DTO.ArtworkDTO;
using static Backend_Teamwork.src.DTO.UserDTO;
using static Backend_Teamwork.src.Entities.User;

namespace Backend_Teamwork.src.Services.artwork
{
    public class ArtworkService : IArtworkService
    {
        private readonly ArtworkRepository _artworkRepo;
        private readonly UserRepository _userRepo;
        private readonly IMapper _mapper;

        public ArtworkService(ArtworkRepository artworkRepo, UserRepository userRepo, IMapper mapper)
        {
            _artworkRepo = artworkRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ArtworkReadDto> CreateOneAsync(Guid artistId, ArtworkCreateDto createDto)
        {
            if (createDto == null)
            {
                throw CustomException.BadRequest("Artwork creation data cannot be null.");
            }
            var artwork = _mapper.Map<ArtworkCreateDto, Artwork>(createDto);
            artwork.ArtistId = artistId;
            var createdArtwork = await _artworkRepo.CreateOneAsync(artwork);
            return _mapper.Map<Artwork, ArtworkReadDto>(createdArtwork);
        }

        public async Task<List<ArtworkReadDto>> GetAllAsync(PaginationOptions paginationOptions)
        {
            // Validate pagination options
            if (paginationOptions.PageSize <= 0)
            {
                throw CustomException.BadRequest("PageSize should be greater than 0.");
            }

            if (paginationOptions.PageNumber <= 0)
            {
                throw CustomException.BadRequest("Offset should be greater than 0.");
            }

            var artworkList = await _artworkRepo.GetAllAsync(paginationOptions);
            if (artworkList == null || !artworkList.Any())
            {
                throw CustomException.NotFound("No artworks found.");
            }
            return _mapper.Map<List<Artwork>, List<ArtworkReadDto>>(artworkList);
        }

        public async Task<ArtworkReadDto> GetByIdAsync(Guid id)
        {
            var artwork = await _artworkRepo.GetByIdAsync(id);
            if (artwork == null)
            {
                throw CustomException.NotFound($"Artwork with id: {id} not found");
            }
            return _mapper.Map<Artwork, ArtworkReadDto>(artwork);
        }

        public async Task<List<ArtworkReadDto>> GetByArtistIdAsync(Guid id)
        {
            // check if user exist
            var user = await _userRepo.GetByIdAsync(id) ?? throw CustomException.NotFound($"User with id: {id} not found");
            // check the role of user
            if (user.Role.ToString() != UserRole.Artist.ToString())
            {
                throw CustomException.BadRequest($"User with id: {id} is not an Artist");
            }
            // check if user(artist) has artwork
            var artworks = await _artworkRepo.GetByArtistIdAsync(id) ?? throw CustomException.NotFound($"Artist with id: {id} has no artworks");
            var artworkList = _mapper.Map<List<Artwork>, List<ArtworkReadDto>>(artworks);
            return artworkList;
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundArtwork = await _artworkRepo.GetByIdAsync(id);
            if (foundArtwork == null)
            {
                throw CustomException.NotFound($"Artwork with id: {id} not found");
            }
            bool isDeleted = await _artworkRepo.DeleteOneAsync(foundArtwork);

            return isDeleted;
        }

        public async Task<ArtworkReadDto> UpdateOneAsync(Guid id, ArtworkUpdateDTO updateDto)
        {
            var foundArtwork = await _artworkRepo.GetByIdAsync(id);
            if (foundArtwork == null)
            {
                throw CustomException.NotFound($"Artwork with id: {id} not found");
            }

            _mapper.Map(updateDto, foundArtwork);
            var updatedArtwork = await _artworkRepo.UpdateOneAsync(foundArtwork);
            return _mapper.Map<Artwork, ArtworkReadDto>(updatedArtwork);
        }
    }
}
