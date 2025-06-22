namespace LinkwayAPI.DTOs.BusinessUnit
{
    public class BusinessUnitListDTO
    {
        public Guid BusinessUnitGuid { get; set; }

        public string BusinessUnitName { get; set; }

        public string? BusinessUnitLogoName { get; set; }

        public string? BusinessUnitDescription { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
