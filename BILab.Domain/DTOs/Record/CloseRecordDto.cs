namespace BILab.Domain.DTOs.Record {
    public class CloseRecordDto {
        public Guid RecordId { get; init; }
        public bool IsCanceled { get; init; } = false;
        public string? CancelingReason { get; init; } = string.Empty;
    }
}
