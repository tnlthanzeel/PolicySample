using FluentValidation.Results;
using Facets.SharedKernal.Exceptions;
using Facets.SharedKernal.Models;
using System.Net;
using System.Text.Json.Serialization;

namespace Facets.SharedKernal.Responses;

public class KeySetResponseResult<T> : BaseResponse
{
    [JsonIgnore]
    public HttpStatusCode HttpStatusCode { get; protected init; }

    public KeySetResponseResult(T? value, KeysetPageInfo? paginator = null) : base()
    {
        Success = value is not null;
        Data = value;
        PageInfo = paginator;
    }

    public KeySetResponseResult(IList<ValidationFailure> validationFailures) : base()
    {
        HttpStatusCode = HttpStatusCode.BadRequest;
        Success = false;
        Data = default;

        if (validationFailures.Count is not 0)
        {
            Errors.AddRange(validationFailures.Select(s => new KeyValuePair<string, IEnumerable<string>>(s.PropertyName, new[] { s.ErrorMessage })).ToList());
        }
    }

    public KeySetResponseResult(IList<KeyValuePair<string, IEnumerable<string>>> validationFailures) : base()
    {
        HttpStatusCode = HttpStatusCode.BadRequest;
        Success = false;
        Data = default;

        if (validationFailures.Count is not 0)
        {
            Errors.AddRange(validationFailures);
        }
    }

    public KeySetResponseResult(ApplicationException ex) : base()
    {
        Success = false;
        Data = default;

        var errorMsg = new[] { ex.Message };

        switch (ex)
        {
            case BadRequestException e:
                HttpStatusCode = HttpStatusCode.BadRequest;
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(e.PropertyName, errorMsg));
                break;

            case ValidationException e:
                HttpStatusCode = HttpStatusCode.BadRequest;
                Errors.AddRange(e.ValdationErrors);
                break;

            case NotFoundException e:
                HttpStatusCode = HttpStatusCode.NotFound;
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(e.PropertyName, errorMsg));
                break;

            case OperationFailedException e:
                HttpStatusCode = HttpStatusCode.BadRequest;
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(e.PropertyName, errorMsg));
                break;

            case UnauthorizedException:
                HttpStatusCode = HttpStatusCode.Unauthorized;
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(nameof(HttpStatusCode.Unauthorized), errorMsg));
                break;

        };
    }

    public KeysetPageInfo? PageInfo { get; set; }

    public T? Data { get; private set; }

    [JsonIgnore]
    public override List<KeyValuePair<string, IEnumerable<string>>> Errors { get; init; } = new();

}
