using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SampleAPI.Entities
{
    /// <summary>
    /// Order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// gets or sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// gets or sets Id
        /// </summary>
        
        public DateTime EntryDate { get; set; }


        /// <summary>
        /// gets or sets Id
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// gets or sets Description
        /// </summary>
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
