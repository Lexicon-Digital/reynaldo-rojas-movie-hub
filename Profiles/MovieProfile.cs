using AutoMapper;
using MoviesAPI.Entities;

namespace MoviesAPI;

public class MovieProfile : Profile
{

  public MovieProfile()
  {
    CreateMap<Movie, MovieWithoutCinemasDto>();
    CreateMap<Movie, MovieDto>()
      .ForMember(dto => dto.Cinemas, opt => opt.MapFrom(m => m.MovieCinema.Select(mc => new CinemaDto
      {
        Name = mc.Cinema.Name,
        Location = mc.Cinema.Location,
        ShowTime = mc.Showtime,
        TicketPrice = mc.TicketPrice
      }).ToList()));
  }
}
