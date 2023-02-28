using AutoMapper;
using MovieCharactersAPI.Data.DTOs.FranchiseDTOs;
using WebApplication1.Models;

namespace MovieCharactersAPI.Profiles
{
    public class FranchiseProfile: Profile
    {
        public FranchiseProfile() 
        {
            CreateMap<CreateFranchiseDto, Franchise>();
            CreateMap<Franchise, FranchiseDto>()
                .ForMember(dto => dto.Movies, options =>
                options.MapFrom(franchiseDomain => franchiseDomain.Movies.Select(movie => movie.Id).ToList()));
            CreateMap<Franchise, EditFranchiseDto>()
                .ReverseMap();

        }
    }
}
