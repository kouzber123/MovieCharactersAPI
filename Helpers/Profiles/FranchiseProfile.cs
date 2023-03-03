using AutoMapper;
using MovieCharactersApp.Data.DTOs.FranchiseDTOs;
using MovieCharactersApp.Models;

namespace MovieCharactersApp.Profiles
{
  /// <summary>
  /// fransise profile mapper
  /// </summary>
  public class FranchiseProfile: Profile
    {
        public FranchiseProfile() 
        {
            CreateMap<CreateFranchiseDto, Franchise>();
            CreateMap<Franchise, FranchiseDto>()
                .ForMember(dto => dto.Movies, options =>
                options.MapFrom(franchiseDomain => franchiseDomain.Movies.Select(movie => movie.Id).ToList()));
                
            CreateMap<Franchise, EditFranchiseDto>()
                 .ForMember(dto => dto.Movies, options =>
                options.MapFrom(franchiseDomain => franchiseDomain.Movies.Select(movie => movie.Id).ToList()));

            CreateMap<Movie, FranchiseMovieDto>().ReverseMap();

            CreateMap<FranchiseCharacterSingleDto, FranchiseCharacterDto>().ReverseMap();

        }
    }
}
