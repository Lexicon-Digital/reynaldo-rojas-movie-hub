namespace MoviesAPI;

public class MovieReviewDto
{
  public int Id { get; set; }
  public decimal Score { get; set; }
  public string Comment { get; set; } = String.Empty;
  public DateTime ReviewDate { get; set; }
}
