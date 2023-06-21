using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Common.Exceptions
{
    public class ResponseExceptions<T>
    {
        public ResponseExceptions()
        {

        }
        public ResponseExceptions(T data, string message)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public ResponseExceptions(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
        public T? Data { get; set; }
    }
}
