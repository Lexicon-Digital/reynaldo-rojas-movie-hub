using AutoMapper;

namespace MoviesAPI;

public class MovieReviewProfile : Profile
{
  public MovieReviewProfile()
  {

    CreateMap<MovieReview, MovieReviewDto>();
    CreateMap<MovieReviewDto, MovieReview>();
    CreateMap<MovieReviewForCreationDto, MovieReview>();
  }
}
