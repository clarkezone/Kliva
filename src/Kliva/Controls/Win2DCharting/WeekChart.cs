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

namespace Kliva.Controls.Win2DCharting
{
    class WeekChart : ChartComponentBase
    {
        
        private List<Vector2> weekPosition = new List<Vector2>();

        private List<Vector2> weekData = new List<Vector2>();

        public float initWeekPositionY { get; set; }

        public float weekStep { get; set; }

        public float monthPositionY { get; set; }

        public float monthStep { get; set; }

        public int selectedWeek { get; set; }

        private List<string> months = new List<string>();


        public List<Vector2> WeekPosition
        {
            get
            {
                return weekPosition;
            }
        }

        public List<Vector2> WeekData
        {
            get
            {
                return weekData;
            }

        }

        public List<string> Months
        {
            get
            {
                return months;
            }

        }

        public override void Draw(CanvasDrawingSession session, Size parentSize)
        {
            //TODO: do the actual drawing here

            //using to demark the WeekChart area
            session.FillRectangle(new Windows.Foundation.Rect(0, parentSize.Height / 2, parentSize.Width, parentSize.Height / 2), Color.FromArgb(255, 229, 229, 229));

            //Week Data
            WeekData.Add(new Vector2(0, 45));
            WeekData.Add(new Vector2(0, 123));
            WeekData.Add(new Vector2(0, 70));
            WeekData.Add(new Vector2(0, 25));
            WeekData.Add(new Vector2(0, 50));
            WeekData.Add(new Vector2(0, 189));
            WeekData.Add(new Vector2(0, 75));
            WeekData.Add(new Vector2(0, 123));
            WeekData.Add(new Vector2(0, 70));
            WeekData.Add(new Vector2(0, 25));
            WeekData.Add(new Vector2(0, 50));
            WeekData.Add(new Vector2(0, 189));
            //Add a buffer to List of weekData
            weekData.Add(new Vector2(0, 75));

            //Add Months to be displayed for the week chart
            Months.Add("JAN");
            Months.Add("FEB");
            Months.Add("MAR");

            initGraphPositionX = (float)(parentSize.Width / 16);
            initWeekPositionY = (float)((parentSize.Height * 7) / 8);
            weekStep = (float)parentSize.Width / 14;
            monthPositionY = (float)(parentSize.Height * 14.5) / 16;
            monthStep = (float)parentSize.Width / 3;
            selectedWeek = 11;

            for (int i = 0; i < 12; i++)
            { 
                 weekPosition.Add(new Vector2(initGraphPositionX + i*weekStep, initWeekPositionY));
            }

            //Add a buffer to List of weekPositions
            weekPosition.Add(new Vector2(initGraphPositionX+11*weekStep, initWeekPositionY));

            //Draw Week Garph
            for (int j = 0; j < weekPosition.Count - 1; j++)
            {
                //draw vertical lines to connect non-zero weekChart values to the base line
                if (weekPosition[j].Y - weekData[j].Y < initWeekPositionY)
                {
                    session.DrawLine(weekPosition[j] - weekData[j], new Vector2(weekPosition[j].X, initWeekPositionY), Color.FromArgb(255, 206, 206, 206));
                }

                session.DrawLine(weekPosition[j] - weekData[j], weekPosition[j + 1] - weekData[j + 1], Color.FromArgb(255, 206, 206, 206));
            }


            for (int m = 0; m < weekPosition.Count - 1; m++)
            {
                //args.DrawingSession.DrawCircle(weekPosition[j], 5, Colors.Black);
                session.FillCircle(weekPosition[m] - weekData[m], 4, Color.FromArgb(255, 126, 126, 126));
            }

            //Draw a base line connecting end points of the week graph
            session.DrawLine(new Vector2(weekPosition[0].X, initWeekPositionY), new Vector2(weekPosition[11].X, initWeekPositionY), Color.FromArgb(255, 206, 206, 206));

            //Draw selected line of the polygraph
            session.DrawLine(new Vector2(weekPosition[selectedWeek].X, initWeekPositionY), new Vector2(weekPosition[selectedWeek].X, (float) parentSize.Height/2), Color.FromArgb(255, 247, 99, 13), 2);
            session.DrawCircle(weekPosition[selectedWeek] - weekData[selectedWeek], 10, Color.FromArgb(255, 247, 99, 13));
            session.FillCircle(weekPosition[selectedWeek] - weekData[selectedWeek], 4, Color.FromArgb(255, 247, 99, 13));


            //Draw name of the months relevant to the chart
            for (int k = 0; k < Months.Count; k++)
            {
                session.DrawText(Months[k], new Vector2(initGraphPositionX + k * monthStep, monthPositionY ), Color.FromArgb(255, 85, 85, 85));
            }
        }
    }
}
