namespace MovieCharactersAPI.Exceptions
{
    public class FranchiseNotFoundException : Exception
    {
        public FranchiseNotFoundException(int id) : base($"Brand with id {id} was not found")
        {

        }
    }
}

