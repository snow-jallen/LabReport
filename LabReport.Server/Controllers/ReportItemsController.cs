using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabReport.Server.Models;
using LabReport.Shared;

namespace LabReport.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportItemsController : ControllerBase
    {
        private readonly LabReportServerContext _context;

        public ReportItemsController(LabReportServerContext context)
        {
            _context = context;
        }

        // GET: api/ReportItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportItem>>> GetReportItem()
        {
            return await _context.ReportItem.ToListAsync();
        }

        // GET: api/ReportItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportItem>> GetReportItem(int id)
        {
            var reportItem = await _context.ReportItem.FindAsync(id);

            if (reportItem == null)
            {
                return NotFound();
            }

            return reportItem;
        }

        // PUT: api/ReportItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportItem(int id, ReportItem reportItem)
        {
            if (id != reportItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(reportItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ReportItems
        [HttpPost]
        public async Task<ActionResult<ReportItem>> PostReportItem(ReportItem reportItem)
        {
            _context.ReportItem.Add(reportItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportItem", new { id = reportItem.Id }, reportItem);
        }

        // DELETE: api/ReportItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReportItem>> DeleteReportItem(int id)
        {
            var reportItem = await _context.ReportItem.FindAsync(id);
            if (reportItem == null)
            {
                return NotFound();
            }

            _context.ReportItem.Remove(reportItem);
            await _context.SaveChangesAsync();

            return reportItem;
        }

        private bool ReportItemExists(int id)
        {
            return _context.ReportItem.Any(e => e.Id == id);
        }
    }
}
