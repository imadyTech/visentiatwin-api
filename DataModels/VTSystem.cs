using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VisentiaTwin_API.DataModels
{
    /// <summary>
    /// A Visentia System Product
    /// </summary>
    public class VTSystem
    {
        [DefaultValue(-1)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int SystemId { get; set; }

        [JsonProperty] public string Name { get; set; }
        [JsonProperty] public string? Description { get; set; }
        [JsonProperty] public string? Version { get; set; }
        [JsonProperty] public string? Author { get; set; }
        [JsonProperty] public string? estimatorString { get; set; }
        [JsonProperty] public ICollection<VTNode> VTNodes { get; set; } = new List<VTNode>();
    }
}