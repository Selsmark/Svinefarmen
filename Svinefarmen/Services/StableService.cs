using Svinefarmen.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Svinefarmen.Services;

public class StableService : IStableService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    public StableService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<List<Stable>> GetAllStablesAsync()
    {
        List<Stable> stables = new List<Stable>();

        Uri uri = new Uri(string.Format(Constants.RestUrl, "GetAllStables"));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                stables = JsonSerializer.Deserialize<List<Stable>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return stables;
    }

    public async Task<List<EarTag>> GetEarTagsByStableIDAsync(int stableID)
    {
        List<EarTag> earTags = new List<EarTag>();

        Uri uri = new Uri(string.Format(Constants.RestUrl, $"GetEarTagsByStableID?stableID={stableID}"));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                earTags = JsonSerializer.Deserialize<List<EarTag>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return earTags;
    }

    public async Task<List<int>> GetHerdNumbersByStableIDAsync(int stableID)
    {
        List<int> herdNumbers = new List<int>();

        Uri uri = new Uri(string.Format(Constants.RestUrl, $"GetHerdsByStableID?stableID={stableID}"));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                herdNumbers = JsonSerializer.Deserialize<List<int>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return herdNumbers;
    }

    public async Task<List<StableInfo>> GetAllStableInfosAsync()
    {
        List<StableInfo> stableInfos = new List<StableInfo>();

        Uri uri = new Uri(string.Format(Constants.RestUrl, "GetAllStableInfos"));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                stableInfos = JsonSerializer.Deserialize<List<StableInfo>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return stableInfos;
    }

    /*
    public async Task<List<EarTag>> GetDummyEarTagsAsync(int count)
    {
        List<EarTag> Items = new List<EarTag>();

        Uri uri = new Uri(string.Format(Constants.RestUrl + "?count=" + count, "GetDummyEarTags"));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<List<EarTag>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }

    public async Task<List<EarTag>> GetEarTagsAsync(string pigstyName)
    {
        List<EarTag> Items = new List<EarTag>();

        Uri uri = new Uri(string.Format(Constants.RestUrl + "?pigstyName=" + pigstyName, "GetDummyEarTags"));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<List<EarTag>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }

    public async Task<List<EarTag>> GetEarTagsAsync()
    {
        List<EarTag> Items = new List<EarTag>();

        Uri uri = new Uri(string.Format(Constants.RestUrl, "GetEarTags"));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<List<EarTag>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }

        return Items;
    }
    */

}
