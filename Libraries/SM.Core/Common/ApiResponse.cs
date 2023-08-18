using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Common
{
    public class ApiResponse<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object? Errors { get; set; }
        public T Data { get; set; }


        public static ApiResponse<T> Fill(T data, bool success, string message, object errors)
        {
            return new()
            {
                Data = data,
                Success = success,
                Errors = errors,
                Message = message
            };
        }

        public static ApiResponse<T> Successful(T data, string message)
        {
            return new()
            {
                Success = true,
                Data = data,
                Message = message,
                Errors = null

            };
        }

        public static ApiResponse<T> Error(object errors, string message)
        {
            return new ApiResponse<T>()
            {
                Success = false,
                Data = null,
                Message = message,
                Errors = errors

            };
        }
    }
}
