using static Facets.SharedKernal.AppEnums;

namespace Facets.Core.Common.Filters;

public sealed record EventLookupFilter(EventStatus? EventStatus);
