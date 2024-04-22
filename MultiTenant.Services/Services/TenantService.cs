using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiTenant.Data.Model;
using MultiTenant.Services.IService;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


namespace MultiTenant.Services.Services
{
    public class TenantService : ITenantService
    {
        private readonly List<Tenant> _tenants;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TenantService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _tenants = new List<Tenant>
        {
            new Tenant(1, "foo", true, "default"),
            new Tenant(2, "bar", true, "dark")

        };
        }

        public async Task<Tenant> GetTenantByIdAsync(int id)
        {
            return _tenants.FirstOrDefault(t => t.Id == id);
        }

        public async Task<Tenant> GetTenantByHostAsync(string host)
        {
            return _tenants.FirstOrDefault(t => t.Host.Equals(host, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<List<Tenant>> GetAllTenantsAsync()
        {
            return _tenants;
        }

        public async Task UploadFaviconAsync(int tenantId, IFormFile file)
        {
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "tenants", "Tenants" + tenantId.ToString(), "favicons");
            string filePath = Path.Combine(folderPath, file.FileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public async Task UploadHomeBannerAsync(int tenantId, IFormFile file)
        {
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "tenants", "Tenants" + tenantId.ToString(), "homebanners");
            string filePath = Path.Combine(folderPath, file.FileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public async Task<byte[]> GetFaviconAsync(int tenantId)
        {
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "tenants", "tenants"+ tenantId.ToString(), "favicons");

            if (Directory.Exists(folderPath))
            {
                var files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    return await File.ReadAllBytesAsync(files[0]);
                }
            }

            return null;
        }

        public async Task<byte[]> GetHomeBannerAsync(int tenantId)
        {
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "tenants", "tenants" + tenantId.ToString(), "homebanners");

            if (Directory.Exists(folderPath))
            {
                var files = Directory.GetFiles(folderPath);
                if (files.Length > 0)
                {
                    return await File.ReadAllBytesAsync(files[0]);
                }
            }

            return null;
        }
    }
}
