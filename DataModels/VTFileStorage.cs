using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisentiaTwin_API.DataModels
{
    public class VTFileStorage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty] public Guid VTFileId { get; set; }

        [Required]
        [JsonProperty] public string FileName { get; set; }

        [Required]
        [JsonProperty] public string Format { get; set; }

        [Required]
        [JsonProperty] public string Path { get; set; }
    }
}