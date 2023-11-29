namespace SalesWebMVC.Services.Exceptions
{
    public class DbConcirrencyException : ApplicationException
    {

        public DbConcirrencyException(string message) : base(message)
        {

        }
    }
}
