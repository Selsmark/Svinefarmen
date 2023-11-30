using Web_API.Data;

namespace Web_API.Services
{
    public interface IStableService
    {
        public Task<List<Stable>> GetAllStablesAsync();
        public Task<List<EarTag>> GetEarTagsByStableIDAsync(int stableID);
        public Task<List<int>> GetHerdNumbersByStableIDAsync(int stableID);
        public Task<List<StableInfo>> GetAllStablesInfosAsync();
        //public Task<List<EarTag>> GetAllEarTagsAsync();
    }
}
