
using Microsoft.Msagl.Drawing;

namespace PolarionViewerLibrary
{
    /// <summary>
    /// Defines the interface for getting the configured color for any given label
    /// </summary>
    public interface IColorConfiguration
    {
        /// <summary>
        /// Returns a color to be used in a node that is identified by the label
        /// </summary>
        /// <param name="label">The label of the node type for which we want to get the configured color</param>
        /// <returns></returns>
        Color GetColor(string label);        
    }
}
