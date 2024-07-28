using System.ComponentModel.DataAnnotations;

namespace RESTful.API.Infrastructures.Request
{
    public class SearchRequest
    {
        public List<Filter>? Filters {  get; set; }

        public SortByInfo? SortBy { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int? PageNumber { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int? PageSize { get; set; }
    }
}
