namespace ASMGX.DeepMed.Model.General
{
    public abstract class QueryParameters
    {
        public string Filter { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortingOrder { get; set; } = "desc";
        public string SortingProperty { get; set; } = string.Empty;
    }
}
