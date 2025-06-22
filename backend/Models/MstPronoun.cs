using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class MstPronoun
{
    public int PronounId { get; set; }

    public Guid PronounGuid { get; set; }

    public string Pronoun { get; set; } = null!;

    public string? PronounDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrUser> UsrUsers { get; set; } = new List<UsrUser>();
}
