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
using Microsoft.Graphics.Canvas.Geometry;

namespace Kliva.Controls.Win2DCharting
{
    class CircleChart : ChartComponentBase
    {
        public string totalGoal { get; set; }

        public float totalMiles { get; set;}

        public float completedMiles { get; set; }

        public float outerCircleRadius { get; set; }

        public float innerCircleRadius { get; set; }

        public float arcRadiusX { get; set; }

        public float arcRadiusY { get; set; }

        public float arcStartAngle { get; set; }

        public float arcSweepAngle { get; set; }

        public Vector2 arcCenterPoint { get; set; }

        public override void Draw(CanvasDrawingSession session, Size parentSize)
        {
            //remove - using to demark the Daychart area
            //session.FillRectangle(new Windows.Foundation.Rect(parentSize.Width / 2, 0, parentSize.Width / 2, parentSize.Height / 2), Colors.LightCyan);

            //set total goal
            totalGoal = "500 mi";
            totalMiles = 500;
            completedMiles = (float)336.98;

            //set values for outer and inner circle
            outerCircleRadius = (float)(parentSize.Width / 6);
            innerCircleRadius = (float)((parentSize.Width ) / 8);

            arcRadiusX = outerCircleRadius;
            arcRadiusY = outerCircleRadius;
            arcStartAngle = (float)(0 * Math.PI / 180F);
            arcSweepAngle = (float)((360*completedMiles/totalMiles)*Math.PI / 180F);
            arcCenterPoint = new Vector2((float)((parentSize.Width * 3) / 4), ((float)parentSize.Height / 4));
            

            //Draw Circle
            session.DrawCircle((float)(parentSize.Width*3)/4, (float)parentSize.Height/4, outerCircleRadius, Color.FromArgb(255, 230, 230, 230 ),  10);
            session.FillCircle((float)(parentSize.Width * 3) / 4, (float)parentSize.Height / 4, innerCircleRadius, Color.FromArgb(255, 247, 99, 13));

            using (var arc = new CanvasPathBuilder(session))
            {
                arc.BeginFigure(arcCenterPoint.X + arcRadiusX, arcCenterPoint.Y);
                arc.AddArc(arcCenterPoint, arcRadiusX, arcRadiusY, arcStartAngle, arcSweepAngle);
                arc.EndFigure(CanvasFigureLoop.Open);

                using (var geometry = CanvasGeometry.CreatePath(arc))
                {
                    session.DrawGeometry(geometry, Color.FromArgb(255, 247, 99, 13), 10);
                }
            }




            session.DrawText(totalGoal, (float)(((parentSize.Width * 3) / 4) - 30), (float)((parentSize.Height / 4) - 30 ), Colors.White);
            session.DrawText("Goal", (float)(((parentSize.Width * 3) / 4) - 20), (float)((parentSize.Height / 4) - 10), Colors.White);



        }
    }
}
