namespace Admin.Haircut.Business.Models.Base
{
    public class PagingResult<T>
    {
        public PagingResult()
        {

        }
        public PagingResult(IReadOnlyCollection<T> items, long total, PagingRequest pagingRequest)
        {
            Items = items;

            Page = pagingRequest.Page;
            PageSize = pagingRequest.PageSize;
            PageCount = total % PageSize == 0 ? total / PageSize : (total / PageSize) + 1;
            ItemCount = total;

            Meta = pagingRequest.Meta;

        }

        public PagingResult(PagingRequest pagingRequest)
        {
            Page = pagingRequest.Page;
            PageSize = pagingRequest.PageSize;
        }

        public long PageCount { get; set; }

        public long ItemCount { get; set; }

        public long Page { get; set; }

        public long PageSize { get; set; }

        public IReadOnlyCollection<T> Items { get; set; }

        public object Meta { get; set; }

    }
}
