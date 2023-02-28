using AutoMapper;
using MovieCharactersApp.Data.DTOs;
using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;
using MovieCharactersApp.Data.DTOs.MoviesDTOs;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.CreateMovieDTOs;
using MovieCharactersApp.Data.DTOs.MoviesDTOs.GetMovieDto;
using WebApplication1.Models;

namespace MovieCharactersApp.Models
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
      CreateMap<CharacterDto, Character>().ReverseMap();


      //fransise map
      CreateMap<Franchise, FranchiseDto>().ReverseMap();



    }
  }
}