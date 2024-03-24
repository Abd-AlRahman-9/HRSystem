namespace HRDomain.Specification.Params
{
    public class GetAllAttendancesParams
    {
        public DateOnly? From { get; set; } = null;
        public DateOnly? To { get; set; } = null;
        public string? sort { get; set; }
        public string? Search { get; set; }

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : value; }
        }
        public int PageIndex { get; set; } = 1;
    }
}
