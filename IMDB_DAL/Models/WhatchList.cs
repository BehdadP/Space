using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IMDB_DAL.Models
{
    [Table("WatchList", Schema = "dbo")]

    public class WatchList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        [Display(Name = "UserEmail")]
        public string UserEmail { get; set; }

        [Display(Name = "IMDBId")]
        public string IMDBId { get; set; }
        [Display(Name = "Watched")]
        public bool Watched { get; set; } = false;

        [JsonIgnore]
        [Display(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;



    }
}
