using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI;

[Route("/api/cities/{cityId}/pointsofinterest")]
[ApiController]
public class PointsOfInterestController : ControllerBase
{
  private IMapper _mapper;
  private IMovieRepository _movieRepository;

  public PointsOfInterestController(
    IMovieRepository movieRepository,
    IMapper mapper
  )
  {
    _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
  {
    if (!await _movieRepository.CityExistsAsync(cityId))
    {
      return NotFound($"The city with id {cityId} was not found");
    }

    var pointsOfInterest = await _movieRepository.GetPointsOfInterestASync(cityId);
    if (pointsOfInterest == null)
    {
      return BadRequest($"The city with id {cityId} does not contain points of interest");
    }
    return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest));
  }

  [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
  public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterestById(int cityId, int pointOfInterestId)
  {
    if (!await _movieRepository.CityExistsAsync(cityId))
    {
      return NotFound($"The city with id {cityId} was not found");
    }

    var pointOfInterest = await _movieRepository.GetPointOfInterestASync(cityId, pointOfInterestId);
    if (pointOfInterest == null)
    {
      return NotFound($"The point of interest with id {pointOfInterestId} was not found for the city with id {cityId}");
    }
    return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
  }



  [HttpPost]
  public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
    int cityId,
    [FromBody] PointOfInterestForCreationDto pointOfInterest)
  {
    if (!await _movieRepository.CityExistsAsync(cityId))
    {
      return NotFound($"The city with id {cityId} was not found");
    }

    var finalPoi = _mapper.Map<PointOfInterest>(pointOfInterest);
    await _movieRepository.AddPointOfInterestToCity(cityId, finalPoi);
    await _movieRepository.SaveChangesAsync();
    var createdPoi = _mapper.Map<PointOfInterestDto>(finalPoi);

    return CreatedAtRoute(
      "GetPointOfInterest",
      new
      {
        cityId = cityId,
        pointOfInterestId = createdPoi.Id
      },
      createdPoi);
  }

  [HttpPut("{pointOfInterestId}")]
  public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
  {
    if (!await _movieRepository.CityExistsAsync(cityId))
    {
      return NotFound($"The city with id {cityId} was not found");
    }

    var pointOfInterestEntity = await _movieRepository.GetPointOfInterestASync(cityId, pointOfInterestId);
    if (pointOfInterestEntity == null)
    {
      return NotFound($"The point of interest with id {pointOfInterestId} was not found");
    }

    _mapper.Map(pointOfInterest, pointOfInterestEntity);
    await _movieRepository.SaveChangesAsync();
    return NoContent();
  }


  [HttpDelete("{pointOfInterestId}")]
  public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
  {
    if (!await _movieRepository.CityExistsAsync(cityId))
    {
      return NotFound($"The city with id {cityId} was not found");
    }

    var pointOfInterestEntity = await _movieRepository.GetPointOfInterestASync(cityId, pointOfInterestId);
    if (pointOfInterestEntity == null)
    {
      return NotFound($"The point of interest with id {pointOfInterestId} was not found");
    }

    _movieRepository.DeletePointOfInterest(pointOfInterestEntity);
    await _movieRepository.SaveChangesAsync();
    return NoContent();
  }




}
