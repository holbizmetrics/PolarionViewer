using System.Collections.Generic;
using Microsoft.Msagl.Drawing;
namespace PolarionViewerLibrary
{
    /// <summary>
    /// Class to return a color for a given type.
    /// 
    /// The colors are returned in a round robin fashion type from a list of available colors.
    /// 
    /// Using a color is assigned for a given label it is remembered and it's returned for 
    /// calls with the same label.
    /// 
    /// When the end of the color's list is reached we start from the beginning
    /// 
    /// <example>Suppose we have in our list color1,color2,color3 and color4
    /// GetColor("A") == color1
    /// GetColor("B") == color2
    /// GetColor("A") == color1
    /// GetColor("C") == color3
    /// </example>
    /// 
    /// The colors are only remembered as long as the object instance lives. The choice of colors is not persisted 
    /// anywhere so calls in different order will result in different colors for the same labels for different instances
    /// or calls on different instances made across time
    /// 
    /// <example>
    /// CyclicColorConfiguration colorCfg1  = new CyclicColorConfiguration("type1");
    /// CyclicColorConfiguration colorCfg2  = new CyclicColorConfiguration("type1");
    /// 
    /// colorCfg1.GetColor("A");colorCfg1.GetColor("B");
    /// colorCfg2.GetColor("B");colorCfg2.GetColor("A");
    /// 
    /// if (colorCfg1.GetColor("A") != colorCfg2.GetColor("A")) // this is true
    /// 
    /// 
    /// </example>
    /// 
    /// 
    /// </summary>
    public class CyclicColorConfiguration : IColorConfiguration
    {
        private Color[] _colorsList;
        private Dictionary<string, Color> _assignedColors;
        private int _currentColor = 0;

        #region list of colors
        protected static Color [] ListOfColors = new Color [] {
            Color.LightBlue,
            Color.LightPink,
            Color.LightGreen,
            Color.LightGoldenrodYellow,
            Color.Lavender,
            Color.LimeGreen,
            Color.MediumSlateBlue,
            Color.MistyRose,
            Color.OliveDrab,
            Color.OrangeRed,
            Color.Red,
            Color.PaleTurquoise,
            Color.PeachPuff,
            Color.Peru,
            Color.RosyBrown,
            Color.SeaGreen,
            Color.Sienna,
            Color.Silver,
            Color.Tan,
            Color.Azure,
            Color.Maroon,
            Color.Purple   
        };
            

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public CyclicColorConfiguration(string type): this(type, ListOfColors)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="colorList"></param>
        public CyclicColorConfiguration(string type, Color [] colorList)
        {
            System.Diagnostics.Debug.Assert(colorList != null);
            System.Diagnostics.Debug.Assert(colorList.Length > 0);

            _colorsList = colorList;
            _assignedColors = new Dictionary<string, Color>();
            _currentColor = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Color> Assign => _assignedColors;

        /// <summary>
        /// Returns the color for any given label.
        /// 
        /// The call is deterministic. If we already assigned a color to a key that value will be returned, otherwise the 
        /// next "available" color from the list is returned.
        /// 
        /// If there are more labels then available colors there can be duplicated colors for two given labels
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public Color GetColor(string label)
        {
            Color color;

            // did we previously assigned a color for this label?
            if (_assignedColors.ContainsKey(label))
            {
                return _assignedColors[label];
            }

            color = _colorsList[_currentColor];

            _currentColor = (_currentColor + 1) % _colorsList.Length;

            return color;
        }
    }
}
