using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class MstBusinessUnit
{
    public int BusinessUnitId { get; set; }

    public Guid BusinessUnitGuid { get; set; }

    public string BusinessUnitName { get; set; } = null!;

    public string BusinessUnitLogoName { get; set; } = null!;

    public string? BusinessUnitDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrUser> AspNetUsers { get; set; } = new List<UsrUser>();
}
