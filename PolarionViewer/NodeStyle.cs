using System.Drawing;
using Microsoft.Msagl.Drawing;
using Color = Microsoft.Msagl.Drawing;

namespace PolarionViewer
{
    /// <summary>
    /// 
    /// </summary>
    public class NodeStyle
    {
        private Color.Color? _fillColor = Color.Color.Magenta;
        private double? _xRadius = 0.0f;
        private double? _yRadius = 0.0f;
        private Color.Color? _color = Color.Color.Magenta;
        private Shape? _shape = Shape.Box;
        private Font _font = null;
        private Color.Color? _textColor = Color.Color.Black;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public NodeStyle(Color.Color color)
        {
            _fillColor = color;
            _xRadius = 5;
            _yRadius = 5;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void ApplyToNode(Node node)
        {
            node.Attr.FillColor = _fillColor.HasValue ? _color.Value : node.Attr.FillColor;
            node.Attr.XRadius = _xRadius.HasValue ? _xRadius.Value : node.Attr.XRadius;
            node.Attr.YRadius = _yRadius.HasValue ? _yRadius.Value : node.Attr.YRadius;
            node.Attr.Color = _color.HasValue ? _color.Value : node.Attr.Color;
            node.Attr.Shape = _shape.HasValue ? _shape.Value : node.Attr.Shape;
            node.Label.FontColor = _textColor.HasValue ? _textColor.Value : Color.Color.Black;
            if (_font != null)
            {
                node.Label.FontName = _font.Name;
                node.Label.FontSize = (int)_font.Size;
            }
        }
    }
}
