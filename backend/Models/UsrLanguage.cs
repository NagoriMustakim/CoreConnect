using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class UsrLanguage
{
    public int LanguageId { get; set; }

    public Guid LanguageGuid { get; set; }

    public int UserId { get; set; }

    public int? ProficiencyId { get; set; }

    public string LanguageName { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual MstProficiency? Proficiency { get; set; }

    public virtual UsrUser User { get; set; } = null!;
}
