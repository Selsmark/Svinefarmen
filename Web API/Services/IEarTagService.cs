using Web_API.Data;

namespace Web_API.Services
{
    public interface IEarTagService
    {
        public Task<int> AddEarTagAsync(EarTag earTag);
        public Task<EarTag> GetEarTagByID(int ID);
    }
}
