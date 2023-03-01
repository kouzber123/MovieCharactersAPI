using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using MovieCharactersAPI.Data.DTOs;
using MovieCharactersAPI.Data.DTOs.CharacterDTOs;
using MovieCharactersAPI.Data.DTOs.FranchiseDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.CreateMovieDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.GetMovieDto;
using WebApplication1.Models;

namespace MovieCharactersAPI.Models
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {

      //-----------------------maps for movie SECTION
      CreateMap<MovieDto, Movie>().ReverseMap();
      // .ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters.Select(c => new Character())));

      //create movie
      CreateMap<CreateMovieDto, Movie>().ReverseMap()
       .ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters))
       .ForMember(dest => dest.Franchise, opt => opt.MapFrom(src => src.Franchise));


      CreateMap<Character, CreateMovieCharacterDto>().ReverseMap();

      CreateMap<CharacterWithoutMoviesDTO, Character>().ReverseMap();
      CreateMap<List<CharacterWithoutMoviesDTO>, Character>().ReverseMap();

      // Mapping types:
      //     CharacterWithoutMoviesDTO[] -> Character
      //     MovieCharactersAPI.Data.DTOs.MoviesDTOs.GetMovieDto.CharacterWithoutMoviesDTO[] -> WebApplication1.Models.Character

      CreateMap<GetMovieDto, Movie>().ReverseMap()
      .ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters));

      CreateMap<GetMovieDto, Character>().ReverseMap();
      CreateMap<UpdateMovieDto, GetMovieDto>().ReverseMap();
      CreateMap<UpdateMovieDto, Movie>().ReverseMap()
      .ForMember(dest => dest.characterWithoutMoviesDTO, opt => opt.MapFrom(src => src.Characters));


      CreateMap<FranchiseWithoutMoviesDTO, Franchise>().ReverseMap();

      //-----------------------------------------------------------------------

      //character map
      CreateMap<CharacterDto, Character>().ReverseMap();


      //fransise map
      CreateMap<Franchise, FranchiseDto>().ReverseMap();



    }
  }
}