using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace InventaryApp.Server.Models
{
    public class Record
    {

        public Record()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        [Key]
        public string Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }
        [Required]
        [StringLength(450)]
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
    }
}
