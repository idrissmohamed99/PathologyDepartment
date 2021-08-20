﻿using System.Collections.Generic;
using System.Linq;

namespace Infra.DTOs
{

    public class ResultOperationDTO<T>
    {
        public List<string> Messages { get; set; }
        public T Result { get; set; }
        public StateResult StateResult { get; set; }

        public static ResultOperationDTO<T> CreateSuccsessOperation(T result = default(T), string[] message = null)
        {
            return new ResultOperationDTO<T> { Result = result, StateResult = StateResult.success, Messages = message == null ? null : message.ToList() };
        }

        public static ResultOperationDTO<T> CreateErrorOperation(string[] messages = null, StateResult stateResult = StateResult.faild)
        {
            return new ResultOperationDTO<T> { Messages = messages.ToList(), StateResult = stateResult };
        }

        public static ResultOperationDTO<T> SendResponseWithData(T result = default(T), string[] masseges = null,
            StateResult stateResult = StateResult.success)
        {
            return new ResultOperationDTO<T> { Result = result, Messages = masseges.ToList(), StateResult = stateResult };
        }
    }
    public enum StateResult
    {
        success,
        faild,
        ex,
        empty
    }

}
