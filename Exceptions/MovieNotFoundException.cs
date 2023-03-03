namespace MovieCharactersAPI.Exceptions
{
  public class MovieNotFoundException : Exception
  {
    public MovieNotFoundException(int id) : base($"Movie with id {id} was not found")
    {
    }
    public MovieNotFoundException(string problem) : base($"Bad movie {problem} request try again")
    {

    }
  }
}