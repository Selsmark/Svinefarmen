namespace Web_API.Services
{
    public interface IEarTagService
    {
        public int InsertEarTag(string countryCode, int centraleHusdyrbrugsRegisterNumber, int herdNumber);
    }
}
