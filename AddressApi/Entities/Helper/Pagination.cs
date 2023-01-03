namespace AddressApi.Entities.Helper
{
    public class Pagination
    {
        public int pageNumber { get; set; } = 1;
        public int _pageSize = 2;
        public int pageSize { get; set; } = 10;

        public string SortBy { get; set; } = "FirstName";

        public string SortOrder { get; set; } = "ASC";
    }
}
