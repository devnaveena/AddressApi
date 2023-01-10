namespace AddressApi.Entities.Helper
{
    public class Pagination
    {
        const int maxPageSize = 50;
        public int pageNumber { get; set; } = 1;
       public int _pageSize { get; set; }
       

        public string SortBy { get; set; } = "UserName";

        public string SortOrder { get; set; } = "ASC";
    }
}
