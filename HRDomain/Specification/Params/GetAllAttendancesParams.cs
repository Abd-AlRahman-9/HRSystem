namespace HRDomain.Specification.Params
{
    public class GetAllAttendancesParams
    {
        public int From { get; set; }
        public int Year { get; set; }
        public int? To { get; set; } 
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
