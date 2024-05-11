using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Models
{
    public class ResponseHandler
    {
        public static ApiResponse GetExceptionResponse(Exception ex)
        {
            return new ApiResponse() { Code = "1", ResponseData = ex.Message };
        }

        public static ApiResponse HandleDbOperations(Action action, object? obj)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                if (obj == null)
                {
                    type = ResponseType.NotFound;
                }
                action();

                return GetApiResponse(type, obj);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex);
            }
        }
        public static async Task<ApiResponse> HandleDbOperationsAsync(Func<Task> action, object? obj)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                if (obj == null)
                {
                    type = ResponseType.NotFound;
                }
                await action();

                return GetApiResponse(type, obj);
            }
            catch (Exception ex)
            {
                return GetExceptionResponse(ex);
            }
        }

        public static ApiResponse GetApiResponse(ResponseType type, object? contract)
        {
            var response = new ApiResponse() { ResponseData = contract };
            switch (type)
            {
                case ResponseType.Success:
                    response.Code = "0";
                    response.Message = "Success";
                    break;
                case ResponseType.NotFound:
                    response.Code = "2";
                    response.Message = "Not found";
                    break;
                default:
                    break;
            }
            return response;
        }
    }
}
