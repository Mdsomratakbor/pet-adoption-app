using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Shared.Dtos
{
    public record ApiResponseDto(bool IsSuccess, string? Message = null)
    {
        public static ApiResponseDto Success() => new (true, null);
        public static ApiResponseDto Failure() => new (false, null);
    }
    public record ApiResponseDto<TData>(bool IsSuccess, TData Data, string? Message)
    {
        public static ApiResponseDto<TData> Success(TData data) => new(true, data, null);
        public static ApiResponseDto<TData> Fail(string? message) => new(false, default!, message);
    }
}
