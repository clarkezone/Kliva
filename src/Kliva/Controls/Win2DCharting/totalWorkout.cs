using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Windows.UI.Xaml.Controls;
using Windows.UI;
using Windows.Foundation;
using Microsoft.Graphics.Canvas.Text;

namespace Kliva.Controls.Win2DCharting 
{
    class totalWorkout : ChartComponentBase
    {
        public string workoutData; 


        public override void Draw(CanvasDrawingSession session, Size parentSize)
        {
            //remove - using to demark the Daychart area
            //session.FillRectangle(new Windows.Foundation.Rect(0, parentSize.Height / 4, parentSize.Width / 2, parentSize.Height / 4), Colors.LightPink);

            workoutData = "336.98";
            initGraphPositionX = (float)(parentSize.Width / 16);

            session.DrawText(workoutData, initGraphPositionX, (float) ((parentSize.Height*5)/16),  Colors.Black, new CanvasTextFormat()
            {

                FontSize = 50,
             });
            session.DrawText("Total miles this week", initGraphPositionX, (float)((parentSize.Height * 6.5) / 16), Colors.Black);

        }
    }
}
