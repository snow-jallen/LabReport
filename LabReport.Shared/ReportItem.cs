using System;

namespace LabReport.Shared
{
    public class ReportItem
    {
        public int Id { get; set; }
        public string HostName { get; set; }
        public string SerialNumber { get; set; }
        public string MacNumber { get; set; }
        public string ImageVersionContent { get; set; }
        public DateTime ReportTime { get; set; }
    }
}
