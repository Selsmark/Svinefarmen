using Web_API.Data;

namespace Web_API.Services
{
    public interface IEarTagService
    {
        public Task<EarTag> AddEarTagAsync(EarTagInsertModel earTag);
        public Task<EarTag> GetEarTagByIDAsync(int id);
    }
}
