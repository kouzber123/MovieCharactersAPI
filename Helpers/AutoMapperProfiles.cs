using AutoMapper;
using MovieCharactersAPI.Data.DTOs.FranchiseDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.CreateMovieDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.GetMovieDto;
using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using WebApplication1.Models;

namespace MovieCharactersAPI.Models
{
    public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {

      //-----------------------maps for movie SECTION
      CreateMap<MovieDto, Movie>().ReverseMap()
      .ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters.Select(c => new Character())));

      //create movie
      CreateMap<CreateMovieDto, Movie>().ReverseMap()
         .ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters.Select(c => new Character())));


      CreateMap<CreateMovieCharacterDto, Character>().ReverseMap();


      CreateMap<CharacterWithoutMoviesDTO, Character>().ReverseMap();


      CreateMap<GetMovieDto, Movie>().ReverseMap();


      CreateMap<FranchiseWithoutMoviesDTO, Franchise>().ReverseMap();

      //-----------------------------------------------------------------------

      //character map
      CreateMap<CharacterReadDto, Character>().ReverseMap();
      CreateMap<CharacterUpdateDto, Character>().ReverseMap();
      CreateMap<CharacterCreateDto, Character>().ReverseMap();


      //fransise map
      CreateMap<Franchise, FranchiseDto>().ReverseMap();



    }
  }
}