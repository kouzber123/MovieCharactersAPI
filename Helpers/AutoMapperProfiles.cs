using AutoMapper;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.UpdateMovie;
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
      CreateMap<MovieDto, Movie>().ReverseMap();

      //create movie
      CreateMap<CreateMovieDto, Movie>().ReverseMap()
       .ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters))
       .ForMember(dest => dest.Franchise, opt => opt.MapFrom(src => src.Franchise));


      CreateMap<Character, CreateMovieCharacterDto>().ReverseMap();

      CreateMap<CharacterWithoutMoviesDTO, Character>().ReverseMap();
      CreateMap<List<CharacterWithoutMoviesDTO>, Character>().ReverseMap();


      CreateMap<GetMovieDto, Movie>().ReverseMap()
      .ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters));

      CreateMap<GetMovieDto, Character>().ReverseMap();

      CreateMap<UpdateMovieDto, GetMovieDto>().ReverseMap();

      CreateMap<UpdateMovieDto, Movie>().ReverseMap();

      CreateMap<UpdateMovieCharactersDto, Movie>().ReverseMap()
      .ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters));



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