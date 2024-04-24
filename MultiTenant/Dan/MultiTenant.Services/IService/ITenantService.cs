using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MultiTenant.Data.Model;


namespace MultiTenant.Services.IService
{
    public interface ITenantService
    {
        Task<Tenant> GetTenantByIdAsync(int id);
        Task<Tenant> GetTenantByHostAsync(string host);
        Task<List<Tenant>> GetAllTenantsAsync();
        Task UploadFaviconAsync(int tenantId, IFormFile file);
        Task UploadHomeBannerAsync(int tenantId, IFormFile file);
        Task<byte[]> GetFaviconAsync(int tenantId);
        Task<byte[]> GetHomeBannerAsync(int tenantId);
    }
}
