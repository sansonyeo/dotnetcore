using System.Data;
using DotnetCoreDapperSample.Api.Databases;

namespace DotnetCoreDapperSample.Api.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IDbConnection DbConnection;

        protected BaseRepository(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        protected BaseRepository(AppDbConnectionProvider provider)
        {
            DbConnection = provider.GetDbConnection();
        }

        public IDbTransaction BeginTransaction()
        {
            if (DbConnection.State == ConnectionState.Closed) DbConnection.Open();
            return DbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }
    }
}
