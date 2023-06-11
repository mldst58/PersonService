using System.Data;

namespace PersonService.Api.DataAccess
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }
    }
}