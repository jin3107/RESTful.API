using System.ComponentModel.DataAnnotations;

namespace RESTful.API.Infrastructures.Request
{
    public class SortByInfo
    {
        [Required]
        public string? FieldName { get; set; }

        public bool? Accending { get; set; } = true;

        public SortByInfo() { }

        public SortByInfo(string fieldName, bool accending)
        {
            FieldName = fieldName;
            Accending = accending;
        }
    }
}
