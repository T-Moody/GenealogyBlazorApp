using GenealogyBlazorApp.Shared.DTOs;

namespace GenealogyBlazorApp.Shared.Services;

// County API client interface
public interface ICountyApiClient
{
    Task<ApiResponse<List<CountyDto>>> GetCountiesAsync(bool activeOnly = true);
    Task<ApiResponse<CountyDto>> GetCountyAsync(int id);
    Task<ApiResponse<CountyDto>> CreateCountyAsync(CreateCountyRequest request);
    Task<ApiResponse<CountyDto>> UpdateCountyAsync(UpdateCountyRequest request);
    Task<ApiResponse> DeleteCountyAsync(int id);
    Task<ApiResponse> ReorderCountiesAsync(List<int> countyIds);
}

// Resource API client interface
public interface IResourceApiClient
{
    Task<ApiResponse<ResourceSearchResult>> SearchResourcesAsync(ResourceSearchRequest request);
    Task<ApiResponse<List<ResourceDto>>> GetResourcesByCountyAsync(int countyId, ResourceType? type = null, bool activeOnly = true);
    Task<ApiResponse<ResourceDto>> GetResourceAsync(int id);
    Task<ApiResponse<ResourceDto>> CreateResourceAsync(CreateResourceRequest request);
    Task<ApiResponse<ResourceDto>> UpdateResourceAsync(UpdateResourceRequest request);
    Task<ApiResponse> DeleteResourceAsync(int id);
    Task<ApiResponse> ReorderResourcesAsync(int countyId, List<int> resourceIds);
}

// Tag API client interface
public interface ITagApiClient
{
    Task<ApiResponse<List<TagDto>>> GetTagsAsync(bool activeOnly = true);
    Task<ApiResponse<TagDto>> GetTagAsync(int id);
    Task<ApiResponse<TagDto>> CreateTagAsync(CreateTagRequest request);
    Task<ApiResponse<TagDto>> UpdateTagAsync(UpdateTagRequest request);
    Task<ApiResponse> DeleteTagAsync(int id);
}

// Site Settings API client interface
public interface ISiteSettingsApiClient
{
    Task<ApiResponse<List<SiteSettingsDto>>> GetAllSettingsAsync();
    Task<ApiResponse<SiteSettingsDto>> GetSettingAsync(string key);
    Task<ApiResponse<SiteSettingsDto>> UpdateSettingAsync(UpdateSiteSettingsRequest request);
}

// Combined admin API client for ease of use
public interface IAdminApiClient : ICountyApiClient, IResourceApiClient, ITagApiClient, ISiteSettingsApiClient
{
    // Dashboard summary data
    Task<ApiResponse<AdminDashboardData>> GetDashboardDataAsync();
}

// Dashboard summary DTO
public class AdminDashboardData
{
    public int TotalCounties { get; set; }
    public int TotalResources { get; set; }
    public int TotalTags { get; set; }
    public List<CountyDto> RecentCounties { get; set; } = new();
    public List<ResourceDto> RecentResources { get; set; } = new();
    
    // Resource type breakdown
    public Dictionary<ResourceType, int> ResourcesByType { get; set; } = new();
    
    // Activity summary
    public int ResourcesAddedThisWeek { get; set; }
    public int CountiesAddedThisWeek { get; set; }
}