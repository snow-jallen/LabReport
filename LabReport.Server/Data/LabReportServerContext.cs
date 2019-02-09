using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LabReport.Shared;

namespace LabReport.Server.Models
{
    public class LabReportServerContext : DbContext
    {
        public LabReportServerContext (DbContextOptions<LabReportServerContext> options)
            : base(options)
        {
        }

        public DbSet<LabReport.Shared.ReportItem> ReportItem { get; set; }
    }
}
