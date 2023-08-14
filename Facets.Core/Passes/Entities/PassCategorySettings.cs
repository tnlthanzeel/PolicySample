using Facets.SharedKernal.Interfaces;
using Facets.SharedKernal.Models;

namespace Facets.Core.Passes.Entities;

public sealed class PassCategorySettings : EntityBase, ICreatedAudit, IUpdatedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }


    public Guid PassCategoryId { get; private set; }
    public PassCategory? PassCategory { get; private set; }

    public bool IsChargeable { get; private set; }

    public decimal Rate { get; private set; }

    public decimal DiscountedRate { get; private set; }

    public bool ApplyEarlyRegistrationDiscountedRate { get; private set; }
    public bool ApplyOnlineRegistrationDiscountedRate { get; private set; }
    public bool ApplyEntireEventDiscountedRate { get; private set; }

    public DateTimeOffset? EarlyRegistrationDiscountedRateValidUntil { get; private set; }

    private PassCategorySettings() { }

    public PassCategorySettings(bool isChargeable,
                                decimal rate,
                                decimal discountedRate,
                                bool applyEarlyRegistrationDiscountedRate,
                                bool applyOnlineRegistrationDiscountedRate,
                                bool applyEntireEventDiscountedRate,
                                DateTimeOffset? earlyRegistrationDiscountedRateValidUntil)
    {
        IsChargeable = isChargeable;
        Rate = rate;
        DiscountedRate = discountedRate;
        ApplyEarlyRegistrationDiscountedRate = applyEarlyRegistrationDiscountedRate;
        ApplyOnlineRegistrationDiscountedRate = applyOnlineRegistrationDiscountedRate;
        ApplyEntireEventDiscountedRate = applyEntireEventDiscountedRate;

        if (ApplyEarlyRegistrationDiscountedRate is true) EarlyRegistrationDiscountedRateValidUntil = earlyRegistrationDiscountedRateValidUntil;
        else EarlyRegistrationDiscountedRateValidUntil = null;

        if (ApplyEarlyRegistrationDiscountedRate ||
            ApplyOnlineRegistrationDiscountedRate ||
            ApplyEntireEventDiscountedRate)
        {
            DiscountedRate = discountedRate;
        }
        else DiscountedRate = 0M;
    }
}
