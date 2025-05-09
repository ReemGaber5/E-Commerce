using Domain.Exceptions;
using Shared.ErrorModels;
using System.Text.Json;

namespace E_Commerce.CustomMiddleWare
{
    public class CustomExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionMiddleWare> logger;

        public CustomExceptionMiddleWare(RequestDelegate Next,ILogger<CustomExceptionMiddleWare> Logger)
        {
            next = Next;
            logger = Logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
                if(httpContext.Response.StatusCode==StatusCodes.Status404NotFound)
                {
                    var Response = new ErrorToReturn()
                    {
                        StatusCode = httpContext.Response.StatusCode,
                        ErrorMessage = $"End Point {httpContext.Request.Path} is Not Found!"
                    };
                    var ResponseTOReturn = JsonSerializer.Serialize(Response);
                    await httpContext.Response.WriteAsync(ResponseTOReturn);
                }
            }
            catch (Exception Ex)
            {
                logger.LogError(Ex, "SomeThing Wrong");

                //Handle error from api 
                //3-Response Objecxt
                var Response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                };
                //1-set status code For Response
                Response.StatusCode=Ex switch
                {
                    NotFoundException=>StatusCodes.Status404NotFound,
                    UnAuthorizedException=>StatusCodes.Status401Unauthorized,
                    BadRequestException badRequestException => GetErrors(badRequestException,Response),
                    _=>StatusCodes.Status500InternalServerError
                };
                //2-Set Content Type for Response 
                httpContext.Response.ContentType="application/json";
              
                //4-Return Object As Json
                var ResponseTOReturn=JsonSerializer.Serialize(Response);
                await httpContext.Response.WriteAsync(ResponseTOReturn);

            }

        }
       private static int GetErrors(BadRequestException exception, ErrorToReturn response)
       {
            response.Errors = exception.Errors;
            return StatusCodes.Status400BadRequest;
            


       }
    }
}
