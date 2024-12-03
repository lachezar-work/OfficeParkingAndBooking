using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace OfficeAndParkingAPI.Filters
{
    public class ErrorResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var count = context.ModelState.Count;
                var errors = new Dictionary<String, String[]>(count);
                foreach (var keyModelStatePair in context.ModelState)
                {
                    var key = keyModelStatePair.Key;
                    var modelErrors = keyModelStatePair.Value.Errors;
                    if (modelErrors is not null && modelErrors.Count > 0)
                    {
                        var errorMessages = modelErrors.Select(error => error.ErrorMessage).ToArray();
                        errors.Add(key, errorMessages);
                    }
                }

                var response = new
                {
                    StatusCode = StatusCodes.Status400BadRequest, 
                    IsSuccess = false,
                    Errors = errors
                };
                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
