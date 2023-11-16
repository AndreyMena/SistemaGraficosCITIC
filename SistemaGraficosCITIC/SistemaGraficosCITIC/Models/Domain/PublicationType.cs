namespace SistemaGraficosCITIC.Models.Domain
{
    public class PublicationType
    {
        public int PublicationTypeId { get; set; }
        public string? PublicationTypeName { get; set; }
        public PublicationType() { }
        public PublicationType(string? publicationTypeName)
        {
            PublicationTypeId = 0;
            PublicationTypeName = publicationTypeName;
        }
    }
}