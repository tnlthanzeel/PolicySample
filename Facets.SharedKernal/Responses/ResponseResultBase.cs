using FluentValidation.Results;
using Facets.SharedKernal.Exceptions;
using System.Net;
using System.Text.Json.Serialization;

namespace Facets.SharedKernal.Responses;

public class ResponseResult<T> : BaseResponse
{
    [JsonIgnore]
    public HttpStatusCode HttpStatusCode { get; protected init; }

    public ResponseResult(T? value, int totalRecordCount = 1) : base()
    {
        Success = value is not null;
        Data = value;
        _totalRecordCount = totalRecordCount;
    }

    public ResponseResult(IList<ValidationFailure> validationFailures) : base()
    {
        HttpStatusCode = HttpStatusCode.BadRequest;
        Success = false;
        Data = default;

        if (validationFailures.Count is not 0)
        {
            Errors.AddRange(validationFailures.Select(s => new KeyValuePair<string, IEnumerable<string>>(s.PropertyName, new[] { s.ErrorMessage })).ToList());
        }
    }

    public ResponseResult(IList<KeyValuePair<string, IEnumerable<string>>> validationFailures) : base()
    {
        HttpStatusCode = HttpStatusCode.BadRequest;
        Success = false;
        Data = default;

        if (validationFailures.Count is not 0)
        {
            Errors.AddRange(validationFailures);
        }
    }

    public ResponseResult(ApplicationException ex) : base()
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

    private int _totalRecordCount = 1;
    public int TotalRecordCount
    {
        get
        {
            if (Data is null)
            {
                _totalRecordCount = 0;
            }

            return _totalRecordCount;
        }
    }
    public T? Data { get; private set; }

    [JsonIgnore]
    public override List<KeyValuePair<string, IEnumerable<string>>> Errors { get; init; } = new();

}
