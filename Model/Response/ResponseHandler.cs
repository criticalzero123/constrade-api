namespace ConstradeApi.Model.Response
{
    public class ResponseHandler
    {
        public static ApiResponse GetExceptionResponse(Exception exception)
        {
            ApiResponse response = new ApiResponse();
            response.Code = "1";
            response.Message = exception.Message;
            return response;
        }

        public static ApiResponse GetApiResponse(ResponseType type, object? contract)
        {
            ApiResponse response = new ApiResponse { ResponseData = contract };
            switch(type)
            {
                case ResponseType.Success:
                    response.Code = "0";
                    response.Message = "Success";
                    break;

                case ResponseType.NotFound:
                    response.Code = "2";
                    response.Message = "No Record Found/Available";
                    break;
            }

            return response;

        }
    }
}
