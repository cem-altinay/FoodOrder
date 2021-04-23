using System;

namespace Shared.CustomException
{
    public class ApiException: Exception
    {
        public ApiException(string message, Exception ex):base(message,ex)
        {
            
        }

        public ApiException(string message):base(message)
        {
            
        }
    }
}