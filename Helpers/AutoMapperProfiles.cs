using AutoMapper;
using MovieCharactersAPI.Data.DTOs;
using MovieCharactersAPI.Data.DTOs.CharacterDTOs;
using MovieCharactersAPI.Data.DTOs.FranchiseDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs;
using MovieCharactersAPI.Data.DTOs.MoviesDTOs.GetMovieDto;
using WebApplication1.Models;

namespace MovieCharactersAPI.Models
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {

      //-----------------------maps for movie SECTION
      CreateMap<MovieDto, Movie>();
      CreateMap<Movie, MovieDto>();

      CreateMap<CharacterWithoutMoviesDTO, Character>();
      CreateMap<Character, CharacterWithoutMoviesDTO>();

      CreateMap<FranchiseWithoutMoviesDTO, Franchise>();
      CreateMap<Franchise, FranchiseWithoutMoviesDTO>();
      //-----------------------------------------------------------------------




    }
  }
}