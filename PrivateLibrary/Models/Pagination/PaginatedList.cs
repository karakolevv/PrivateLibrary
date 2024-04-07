namespace PrivateLibrary.Models.Pagination
{
    public class PaginatedList<T> : List<T> where T : class
    {
        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling((float)count / pageSize);
            this.AddRange(items);
        }

        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalPages);
    }
}
