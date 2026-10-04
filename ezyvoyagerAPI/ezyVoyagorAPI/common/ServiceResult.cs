using System;

namespace ezyvoyagerAPI.common
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        // ✅ Unified Success method
        public static ServiceResult Success(object data = null, string message = "Success")
        {
            return new ServiceResult
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

        // ✅ Failure method
        public static ServiceResult Failure(string message)
        {
            return new ServiceResult
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
