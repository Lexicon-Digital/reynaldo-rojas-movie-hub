using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI;

[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
  private IMovieRepository _movieRepository;
  private IMapper _mapper;

  const int maxPageSize = 20;

  public CitiesController(
    IMovieRepository movieRepository,
    IMapper mapper)
  {
    _movieRepository = movieRepository ??
        throw new ArgumentNullException(nameof(movieRepository));

    _mapper = mapper ??
        throw new ArgumentNullException(nameof(movieRepository));
  }


  [HttpGet]
  public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities([FromQuery] string? name, [FromQuery] string? searchQuery, int pageNumber = 1, int pageSize = 10)
  {
    if (pageSize > maxPageSize)
    {
      pageSize = maxPageSize;
    }

    var (cityEntities, paginationMetadata) = await _movieRepository.GetCitiesASync(name, searchQuery, pageNumber, pageSize);

    Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
    return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntities));
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetCityById(int id, [FromQuery] bool includePointsOfInterest = false)
  {
    var city = await _movieRepository.GetCityAsync(id, includePointsOfInterest);
    if (city == null)
    {
      return NotFound();
    }

    if (includePointsOfInterest)
    {
      return Ok(_mapper.Map<CityDto>(city));
    }

    return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));
  }
}
