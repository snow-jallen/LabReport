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
    public class DeleteModel : PageModel
    {
        private readonly LabReport.Server.Models.LabReportServerContext _context;

        public DeleteModel(LabReport.Server.Models.LabReportServerContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReportItem = await _context.ReportItem.FindAsync(id);

            if (ReportItem != null)
            {
                _context.ReportItem.Remove(ReportItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
