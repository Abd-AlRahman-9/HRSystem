using HRSystem.DTO;

namespace HRSystem.Helpers
{
    public class Pagination<T>
    {
        public Pagination(int? pageIndex, int? pageSize, int count, IEnumerable<T> data)
        {
            PageIndex = pageIndex.Value;
            PageSize = pageSize.Value;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
        
    }
}
