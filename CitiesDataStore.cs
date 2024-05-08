using MoviesAPI.Models;

namespace MoviesAPI;

public class CitiesDataStore
{
  public List<CityDto> Cities { get; set; }

  public CitiesDataStore()
  {
    Cities = new List<CityDto>() {

      new CityDto() {
        Id = 1,
        Name = "New York City",
        Description = "NYC Description",
        PointsOfInterest = new List<PointOfInterestDto>() {
          new PointOfInterestDto() {
            Id = 1,
            Name = "Central Park",
            Description = "Central Park Description",
          },
          new PointOfInterestDto() {
            Id = 2,
            Name = "Central Square",
            Description = "Central Square Description",
          },
        }
      },
      new CityDto() {
        Id = 2,
        Name = "Sydney",
        Description = "Sydney Description",
        PointsOfInterest = new List<PointOfInterestDto>() {
          new PointOfInterestDto() {
            Id = 3,
            Name = "Opera House",
            Description = "Opera House Description"
          }
        }
      },
      new CityDto() {
        Id = 3,
        Name = "Melbourne",
        Description = "Melb Description",
        PointsOfInterest = new List<PointOfInterestDto>() {
          new PointOfInterestDto() {
            Id = 4,
            Name = "Melbourne Park",
            Description = "Melbourne Park Description"
          }
        }
      },
      new CityDto() {
        Id = 4,
        Name = "Random City",
        Description = "Random City Description",
        PointsOfInterest = new List<PointOfInterestDto>()
      },
    };
  }

  public CityDto? GetCityById(int cityId)
  {
    return Cities.FirstOrDefault(c => c.Id == cityId);
  }
}



/*
DB INSERT

// Cities
INSERT INTO Cities (Name, Description) VALUES 
  ("New York City", "New York City Description"), 
  ("Sydney", "Sydney Description"),
  ("Melbourne", "Melbourne Description");

// Points of interest
INSERT INTO PointsOfInterest (CityId, Name, Description) VALUES 
  (1, "Central Park", "Central Park Description"), 
  (1, "Central Square", "Central Square Description"),
  (2, "Opera House", "Opera House Description"),
  (2, "Harbour Bridge", "Harbour Bridge Description"),
  (3, "Melbourne Park", "Melbourne Park Description"),
  (3, "Melbourne Museum", "Melbourne Museum Description");

  SELECT c.Name as "City",
         GROUP_CONCAT(poi.Name, ", ") as "Points Of Interest"
    FROM Cities c
    JOIN PointsOfInterest poi ON poi.CityId = c.Id
GROUP BY c.Name;

*/