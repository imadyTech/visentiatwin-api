
using Newtonsoft.Json;
using System.ComponentModel;

namespace VisentiaTwin_API.DataModels
{
    /// <summary>
    /// A system is comprised of multiple components
    /// </summary>
    public class VTComponent
    {
        [JsonProperty]
        [DefaultValue(-1)]
        public int VTComponentId { get; set; }

        [JsonProperty]  public string Name { get; set; }
        [JsonProperty]  public string? Catergory { get; set; }
        [JsonProperty]  public string? Description { get; set; }
        [JsonProperty]  public string? Version { get; set; }
        [JsonProperty]  public string? Author { get; set; }
        [JsonProperty]  public float Cost { get; set; }
        [JsonProperty]  public string? estimatorString { get; set; }
        [JsonIgnore]    public ICollection<VTNodeComponent> VTNodeComponents { get; set; } = new List<VTNodeComponent>();
        [JsonProperty]  public Guid modelId { get; set; }

    }
}