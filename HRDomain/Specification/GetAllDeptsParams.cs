namespace HRDomain.Specification
{
    public class GetAllDeptsParams
    {
        public string MngNationalId { get; set; }
        public string sort { get; set; }
        public string Search { get; set; }
        public int? MngId { get; set; }
        private int? pageSize;
        public int? PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : value; }
        }
        public int? PageIndex { get; set; } = 1;

    }
}
