using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.WrappersModels
{
    public class Response
    {
        public Response(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }

        public static Response Success()
        {
            return new Response(true, string.Empty);
        }

        public static Response Failure(string error)
        {
            return new Response(false, error);
        }
    }
    public class Response<T>
    {
        public Response(bool isSuccess, T? data, string error)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
        }

        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string Error { get; set; }

        public static Response<T> Success(T data)
        {
            return new Response<T>(true, data, string.Empty);
        }

        public static Response<T> Failure(string error, T? data)
        {
            return new Response<T>(false, data, error);
        }

    }
}
