namespace Facets.Core.Settings.Common;

public sealed class TemplatePlaceHolder
{
    public Guid Id { get; private set; }

    public string DisplayName { get; private set; } = null!;

    public string Token { get; private set; } = null!;

    private TemplatePlaceHolder() { }

    public static IReadOnlyList<TemplatePlaceHolder> GetDefaultData()
    {
        IReadOnlyList<TemplatePlaceHolder> defaultData = new[]
        {
           new TemplatePlaceHolder()
           {
              Id=Guid.Parse("33d753ba-4083-4d02-8160-69caca42bde6"),
              DisplayName= "Event Name",
              Token="#EventName#"
           },

           new TemplatePlaceHolder ()
           {
              Id=Guid.Parse("774099f5-2421-4400-abac-715b65f491b8"),
              DisplayName = "Profile Image",
              Token = "#ProfileImage#"
           },

           new TemplatePlaceHolder ()
           {
              Id=Guid.Parse("f7789f28-5d94-4cb5-877b-0e91c4fda2e8"),
              DisplayName = "First Name",
              Token="#FirstName#"
           },

           new TemplatePlaceHolder ()
           {
              Id=Guid.Parse("37ab3da2-a533-4c8d-ac10-765455324861"),
              DisplayName = "Last Name",
              Token="#LastName#"
           },

           new TemplatePlaceHolder ()
           {
              Id=Guid.Parse("48032361-473f-4c78-aa63-b5e0ef240746"),
              DisplayName = "NIC Number/ Passport Number",
              Token="#NICNumberPassportNumber#"
           },

           new TemplatePlaceHolder ()
           {
              Id=Guid.Parse("af2e3eff-4969-49cb-b872-0b3958785df3"),
              DisplayName = "Mobile Number",
              Token="#MobileNumber#"
           },

           new TemplatePlaceHolder ()
           {
              Id=Guid.Parse("4487c879-3837-47f1-906c-2620a71a4622"),
              DisplayName = "Pass Category",
              Token="#PassCategory#"
           },

           new TemplatePlaceHolder ()
           {
              Id=Guid.Parse("b7632b78-73c6-4e20-a8e0-889f71a7958a"),
              DisplayName = "Generated QR",
              Token="#GeneratedQR#"
           },

           new TemplatePlaceHolder ()
           {
              Id=Guid.Parse("01682e6d-a927-4737-af7d-6d7a24b892af"),
              DisplayName = "Pass Generated Date & Time",
              Token="#PassGeneratedDate&Time#"
           },

           new TemplatePlaceHolder ()
           {
              Id=Guid.Parse("07ba8b87-54ca-40b3-b63e-1de8eb76ea8a"),
              DisplayName = "Pass Date",
              Token="#PassDate#"
           }
        };

        return defaultData;
    }
}
