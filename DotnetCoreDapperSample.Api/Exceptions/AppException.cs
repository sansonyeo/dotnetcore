using System;
using System.Globalization;

namespace DotnetCoreDapperSample.Api.Exceptions
{
    /// <summary>
    /// アプリケーション固有の例外実装例です。
    /// 애플리케이션 고유의 예외 구현 예입니다.
    /// </summary>
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
