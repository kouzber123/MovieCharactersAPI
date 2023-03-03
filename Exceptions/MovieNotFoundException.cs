namespace MovieCharactersAPI.Exceptions
{
  public class MovieNotFoundException : Exception
  {
    public MovieNotFoundException(int id) : base($"Movie with id {id} was not found")
    {
    }
  }
}