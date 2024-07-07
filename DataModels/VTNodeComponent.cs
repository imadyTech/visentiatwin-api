using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;

namespace VisentiaTwin_API.DataModels
{
    public class VTNodeComponent
    {
        [JsonProperty] public int VTNodeId { get; set; }
        [JsonIgnore] public VTNode? VTNode { get; set; }
        [JsonProperty] public int VTComponentId { get; set; }
        [JsonProperty] public VTComponent? VTComponent { get; set; }
        [JsonProperty] public bool isSelected { get; set; }

        /// <summary>
        /// global position
        /// </summary>
        [JsonProperty] public float posX { get; set; }
        [JsonProperty] public float posY { get; set; }
        [JsonProperty] public float posZ { get; set; }
        /// <summary>
        /// global scale
        /// </summary>
        [JsonProperty] public float sclX { get; set; }
        [JsonProperty] public float sclY { get; set; }
        [JsonProperty] public float sclZ { get; set; }
        /// <summary>
        /// global rotation
        /// </summary>
        [JsonProperty] public float rotX { get; set; }
        [JsonProperty] public float rotY { get; set; }
        [JsonProperty] public float rotZ { get; set; }
    }
}
