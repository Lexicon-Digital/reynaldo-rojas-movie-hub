using AutoMapper;
using MoviesAPI.Entities;

namespace MoviesAPI;

public class MovieProfile : Profile
{

  public MovieProfile()
  {
    CreateMap<Movie, MovieWithoutCinemasDto>();

    CreateMap<MovieCinema, CinemaDto>()
          .ForMember(dto => dto.Name, opt => opt.MapFrom(m => m.Cinema.Name))
          .ForMember(dto => dto.Location, opt => opt.MapFrom(m => m.Cinema.Location));

    CreateMap<Movie, MovieDto>()
      .ForMember(dto => dto.Cinemas, opt => opt.MapFrom(m => m.MovieCinema));
  }
}
