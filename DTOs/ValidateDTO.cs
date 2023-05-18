using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class ValidateDTO
    {
        [Range(1, 1000, ErrorMessage = "The value must be between 1 and 1000.")]
        public int KidIndex { get; set; }
        public string ExpectedReturn { get; set; }
    }
}
