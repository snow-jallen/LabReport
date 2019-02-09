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
    public class DetailsModel : PageModel
    {
        private readonly LabReport.Server.Models.LabReportServerContext _context;

        public DetailsModel(LabReport.Server.Models.LabReportServerContext context)
        {
            _context = context;
        }

        public ReportItem ReportItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReportItem = await _context.ReportItem.FirstOrDefaultAsync(m => m.Id == id);

            if (ReportItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
