using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SampleAPI.Requests
{
    /// <summary>
    /// CreateOrderRequest
    /// </summary>
    public class CreateOrderRequest
    {
        /// <summary>
        /// gets or sets Id
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// gets or sets EntryDate
        /// </summary>
        public DateTime EntryDate { get; set; }


        /// <summary>
        /// gets or sets Name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        /// <summary>
        /// gets or sets Description
        /// </summary>
        [MaxLength(100)]
        public string? Description { get; set; }

        /// <summary>
        /// gets or sets IsInvoiced
        /// </summary>
        public bool IsInvoiced { get; set; } = true;

        /// <summary>
        /// gets or sets IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
