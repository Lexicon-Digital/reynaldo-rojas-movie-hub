﻿namespace MoviesAPI;

public class CinemaDto
{
  public string Name { get; set; } = String.Empty;
  public string Location { get; set; } = String.Empty;
  public DateOnly ShowTime { get; set; }
  public decimal TicketPrice { get; set; }
}
