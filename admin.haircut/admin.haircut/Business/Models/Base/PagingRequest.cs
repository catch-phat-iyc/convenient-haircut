namespace Admin.Haircut.Business.Models.Base
{
    public class PagingRequest
    {
        public long Page { get; set; } = 1;

        public long PageSize { get; set; } = 1000;

        public (long Skip, long Take) GetSkipTake()
        {
            if (Page <= 0)
                Page = 1;

            if (PageSize <= 0 || PageSize > 1000)
                PageSize = 1000;

            return ((Page - 1) * PageSize, PageSize);
        }

        //DTO
        public object? Meta { get; set; }
    }
}
