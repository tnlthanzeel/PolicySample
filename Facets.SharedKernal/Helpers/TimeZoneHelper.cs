using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;
using TimeZoneConverter;

namespace Facets.SharedKernal.Helpers;

public static class TimeZoneHelper
{
    private static readonly IReadOnlyList<TimeZoneModel> _timeZones = TimeZoneInfo.GetSystemTimeZones()
                                                    .Select(tz => new TimeZoneModel(tz.DisplayName, tz.Id)).ToList();

    public static ResponseResult<IReadOnlyList<TimeZoneModel>> GetAllTimeZone()
    {
        return new ResponseResult<IReadOnlyList<TimeZoneModel>>(_timeZones);
    }

    public static bool IsTimeZoneAvailable(string id)
    {
        bool isTimeZoneAvailable = TZConvert.TryGetTimeZoneInfo(id, out TimeZoneInfo? _);
        return isTimeZoneAvailable;
    }
}
