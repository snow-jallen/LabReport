using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LabReport.Server.Models;
using LabReport.Shared;

namespace LabReport.Server.Pages
{
    public class CreateModel : PageModel
    {
        private readonly LabReport.Server.Models.LabReportServerContext _context;

        public CreateModel(LabReport.Server.Models.LabReportServerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ReportItem ReportItem { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ReportItem.Add(ReportItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}