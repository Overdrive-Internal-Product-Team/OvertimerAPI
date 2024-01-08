namespace ClockifyCloneAPI.Models.Company
{
    public class GetCompanyResponse : BaseModel
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
    }
}
