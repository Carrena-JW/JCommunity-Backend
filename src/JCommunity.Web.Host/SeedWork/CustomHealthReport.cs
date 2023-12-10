namespace JCommunity.Web.Host.SeedWork
{
    public class CustomHealthReport
    {
        public CustomHealthReport(
            IDictionary<string, CustomHealthReportEntry> entries,
            string status,
            TimeSpan totalDuration)
        {
            Entries = entries;
            Status = status;
            TotalDuration = totalDuration;
        }
        public IDictionary<string, CustomHealthReportEntry> Entries { get; set; }
        public string Status { get; set; }
        public TimeSpan TotalDuration { get; set; }

        public static CustomHealthReport Create(HealthReport report)
        {
            var rewriteEntries = report.Entries.ToDictionary(
                                         x => x.Key,
                                         x => new CustomHealthReportEntry(
                                             x.Value.Data,
                                             x.Value.Description,
                                             x.Value.Duration,
                                             x.Value.Exception == null ? null : x.Value.Exception!.Message,
                                             x.Value.Status.ToString(),
                                             x.Value.Tags));

            return new CustomHealthReport(rewriteEntries, report.Status.ToString(), report.TotalDuration);
        }
    }

    public struct CustomHealthReportEntry
    {
        public CustomHealthReportEntry(
            IReadOnlyDictionary<string, object> data,
            string? description,
            TimeSpan duration,
            string? exception,
            string status,
            IEnumerable<string> tags)
        {
            Data = data;
            Description = description;
            Duration = duration;
            Exception = exception;
            Status = status;
            Tags = tags;
        }

        public IReadOnlyDictionary<string, object> Data { get; set; }
        public string? Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string? Exception { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }


}


