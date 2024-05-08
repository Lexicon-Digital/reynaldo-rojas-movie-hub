using AutoMapper;
using MoviesAPI.Models;
using MoviesAPI.Entities;

namespace MoviesAPI.Services;

public class PointOfInterestProfile : Profile
{

  public PointOfInterestProfile()
  {
    CreateMap<PointOfInterest, PointOfInterestDto>();
    CreateMap<PointOfInterestForCreationDto, PointOfInterest>();
    CreateMap<PointOfInterestForUpdateDto, PointOfInterest>();
  }
}
