using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Kliva.Controls.Win2DCharting
{
    abstract class ChartComponentBase
    {

        public float initGraphPositionX { get; set; }

        public abstract void Draw(CanvasDrawingSession session, Size ParentSize);
    }
}
