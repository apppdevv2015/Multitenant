using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenant.Data.Model
{
    public class Tenant
    {

        public int Id { get; set; }
        public string Host { get; set; }
        public bool IsActive { get; set; }
        public string ThemeName { get; set; }

        public Tenant(int id, string host, bool isActive, string themeName)
        {
            Id = id;
            Host = host;
            IsActive = isActive;
            ThemeName = themeName;
        }
    }
}
