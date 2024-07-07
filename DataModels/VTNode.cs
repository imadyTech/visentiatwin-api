

using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VisentiaTwin_API.DataModels
{
    /// <summary>
    /// A ViPart is an instance of parts may used as an option of VtComponent
    /// </summary>
    public class VTNode
    {
        [DefaultValue(-1)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int VTNodeId { get; set; }
        [JsonProperty] public string Name { get; set; }
        [JsonProperty] public string? Description { get; set; }
        [JsonProperty] public string? Version { get; set; }
        [JsonProperty] public string? Author { get; set; }
        [JsonIgnore]   public VTSystem? VTSystem { get; set; }
        [ForeignKey("VTSystem")] public int VTSystemId { get; set; }
        [JsonProperty] public ICollection<VTNodeComponent> VTNodeComponents { get; set; } = new List<VTNodeComponent>();
    }
}