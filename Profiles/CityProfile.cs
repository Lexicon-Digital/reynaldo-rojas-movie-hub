using AutoMapper;
using MoviesAPI.Entities;
using MoviesAPI.Models;

namespace MoviesAPI;

public class CityProfile : Profile
{

  public CityProfile()
  {

    CreateMap<City, CityWithoutPointOfInterestDto>();
    CreateMap<City, CityDto>();
  }
}
