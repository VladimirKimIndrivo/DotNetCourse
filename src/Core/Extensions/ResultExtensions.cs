using FluentResults;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToProblemDetails(this FluentResults.Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException("Cannot transform a success result");
            }

            return CreateProblemDetails(result.Errors);
        }

        public static IResult ToProblemDetails<T>(this FluentResults.Result<T> result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException("Cannot transform a success result");
            }

            return CreateProblemDetails(result.Errors);
        }

        private static IResult CreateProblemDetails(List<IError> errors)
        {
            var errorMessages = errors.Select(x => x.Message);

            var titles = errorMessages
                .Aggregate((i, j) => $"{i}, {Environment.NewLine} {j}");

            var reasonDetails = string.Empty;

            foreach (var error in errors)
            {
                reasonDetails = error
                    .Reasons
                    .Aggregate(reasonDetails, (current, reason) => current + $"{Environment.NewLine} {reason.Message}");
            }

            return Results.Problem(title: titles, detail: reasonDetails);
        }
    }
}
