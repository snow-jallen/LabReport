using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabReport.Server.Models;
using LabReport.Shared;

namespace LabReport.Server.Pages
{
    public class EditModel : PageModel
    {
        private readonly LabReport.Server.Models.LabReportServerContext _context;

        public EditModel(LabReport.Server.Models.LabReportServerContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ReportItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportItemExists(ReportItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReportItemExists(int id)
        {
            return _context.ReportItem.Any(e => e.Id == id);
        }
    }
}
