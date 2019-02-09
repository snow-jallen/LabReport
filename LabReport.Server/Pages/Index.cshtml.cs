using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabReport.Server.Models;
using LabReport.Shared;

namespace LabReport.Server.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LabReport.Server.Models.LabReportServerContext _context;

        public IndexModel(LabReport.Server.Models.LabReportServerContext context)
        {
            _context = context;
        }

        public IList<ReportItem> ReportItem { get;set; }
        
        public async Task OnGetAsync()
        {
            ReportItem = await _context.ReportItem.ToListAsync();
        }
        
        public async Task OnPostAsync(ReportItem newItem)
        {
            _context.ReportItem.Add(newItem);
            await _context.SaveChangesAsync();
        }
    }
}
