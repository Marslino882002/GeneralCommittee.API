using GeneralCommittee.Domain.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Common
{
    public class OperationResult<T>
    {

        public bool Success { get; set; }
        public StateCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; } = default(T);


        public static OperationResult<T> SuccessResult(
            T data,
            string message = "",
            StateCode statusCode = StateCode.Ok)
        {
            return new OperationResult<T>
            {
                Data = data,
                Success = true,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static OperationResult<T> Failure(string message, StateCode statusCode = StateCode.BadRequest)
        {
            return new OperationResult<T>
            {
                Success = false,
                Message = message,
                StatusCode = statusCode
            };
        }















    }
}
