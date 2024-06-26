﻿namespace BILab.Domain.DTOs.Base {
    public abstract class PageableBaseRequestDto {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string? OrderBy { get; set; }
        public IEnumerable<(string columnName, bool descOrder)>? ThenBy { get; set; }
        public bool DescOrder { get; set; } = false;
        public string? SearchText { get; set; }
    }
}
