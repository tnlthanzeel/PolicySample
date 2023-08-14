using Facets.Core.Events.Entities;
using Facets.SharedKernal.Interfaces;
using Facets.SharedKernal.Models;

namespace Facets.Core.Passes.Entities;

public sealed class PassCategory : EntityBase, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    private static PassCategorySettings _defualtPassCategorySetting => new(isChargeable: false,
                                                                           rate: 0,
                                                                           discountedRate: 0,
                                                                           applyEarlyRegistrationDiscountedRate: false,
                                                                           applyOnlineRegistrationDiscountedRate: false,
                                                                           applyEntireEventDiscountedRate: false,
                                                                           earlyRegistrationDiscountedRateValidUntil: null);

    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public Guid PassTypeId { get; private set; }
    public PassType? PassType { get; private set; }
    public bool IsDefault { get; private set; }

    public Guid EventId { get; private set; }
    public Event Event { get; private set; } = null!;

    private readonly List<PassCategorySettings> _passCategorySettings = new();

    public IReadOnlyCollection<PassCategorySettings> PassCategorySettings => _passCategorySettings.AsReadOnly();

    public static IReadOnlyList<PassCategory> GetDefaultData(Guid eventId)
    {
        IReadOnlyList<PassCategory> defaultPassTypes = new[]
        {
           new PassCategory()
           {
              Name = "Member",
              Description="Association Member",
              PassTypeId = Guid.Parse(PassType.VisitorPassTypeId),
              EventId = eventId,
              IsDefault = true,
           },

           new PassCategory ()
           {
              Name = "Foreign - Buyer",
              Description="Foreigner & Buyer",
              PassTypeId = Guid.Parse(PassType.VisitorPassTypeId),
              EventId = eventId,
              IsDefault = true
           },

           new PassCategory ()
           {
              Name = "Foreign - Visitor",
              Description="Foreigner & Visitor",
              PassTypeId = Guid.Parse(PassType.VisitorPassTypeId),
              EventId = eventId,
              IsDefault = true
           },

           new PassCategory ()
           {
              Name = "Local - Visitor",
              Description="Sri Lankan visitor",
              PassTypeId = Guid.Parse(PassType.VisitorPassTypeId),
              EventId = eventId,
              IsDefault = true
           }
        };

        foreach (var defaultPassType in defaultPassTypes) defaultPassType._passCategorySettings.Add(_defualtPassCategorySetting);

        return defaultPassTypes;
    }

    private PassCategory() { }

    public PassCategory(Guid eventId, string name, string? description)
    {
        EventId = eventId;
        Name = name;
        Description = description;
        PassTypeId = Guid.Parse(PassType.VisitorPassTypeId);
        _passCategorySettings.Add(_defualtPassCategorySetting);
    }
}
