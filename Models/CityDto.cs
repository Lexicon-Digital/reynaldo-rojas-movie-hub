namespace MoviesAPI.Models;

public class CityDto
{
  public int Id { get; set; }
  public string Name { get; set; } = String.Empty;
  public string Description { get; set; } = String.Empty;

  public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();
  public int NumberOfPointsOfInterest
  {
    get
    {
      return PointsOfInterest.Count;
    }
  }
}
