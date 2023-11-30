using Svinefarmen.Models;

namespace Svinefarmen.Services
{
    public interface IStableService
    {
        public Task<List<Stable>> GetAllStablesAsync();
        public Task<List<EarTag>> GetEarTagsByStableIDAsync(int stableID);
        public Task<List<int>> GetHerdNumbersByStableIDAsync(int stableID);
        public Task<List<StableInfo>> GetAllStableInfosAsync();
        /*
        public Task<EarTag[]> GetRandomDummyEarTags(int count);
        public Task<List<EarTag>> GetDummyEarTagsAsync(int count);
        public Task<List<EarTag>> GetEarTagsAsync(string pigstyName);
        public Task<List<EarTag>> GetEarTagsAsync();
        */
    }
}
