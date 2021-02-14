using System;
using System.Data;

namespace DotnetCoreDapperSample.Api.Databases
{
    /// <summary>
    /// IDbConnection を提供するためのクラスです。(을 제공하기 위한 구현)
    /// (※) 複数のDbConnectionをDIコンテナで管理することを想定したクラスになります。
    /// (※) 복수의 DbConnection를 DI컨테이너로 관리하는 것을 상정한 클래스입니다.
    /// </summary>
    public class AppDbConnectionProvider : IDisposable
    {
        private readonly IDbConnection _dbConnection;

        public AppDbConnectionProvider(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IDbConnection GetDbConnection() => _dbConnection;

        public void Dispose()
        {
            _dbConnection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
