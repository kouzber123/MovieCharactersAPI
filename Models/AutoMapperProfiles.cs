using AutoMapper;
using MovieCharactersAPI.Data.DTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersApp.Data.DTOs.CharacterDTOs;
using WebApplication1.Models;

namespace MovieCharactersAPI.Models
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {

      CreateMap<MovieDto, Movie>();
      CreateMap<Movie, MovieDto>();

      CreateMap<CreateMovieDto, Movie>();
      CreateMap<Movie, CreateMovieDto>();

      //   CreateMap<Franchise, FranchiseDto>();
      //   CreateMap<FranchiseDto, Franchise>();

      CreateMap<UpdateMovieDto, Movie>();
      CreateMap<Movie, UpdateMovieDto>();
      
      CreateMap<CharacterCreateDto,Character>();
      CreateMap<Character, CharacterCreateDto>();

            CreateMap<CharacterReadDto, Character>();
            CreateMap<Character, CharacterReadDto>();

            CreateMap<CharacterUpdateDto, Character>();
            CreateMap<Character, CharacterUpdateDto>();


        }
  }
}