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
    class DayChart : ChartComponentBase
    {

        //list of inital positions for each bar in the day graph
        public List<Vector2> dayPosition = new List<Vector2>();

        //list of ride data; Y cood defines the miles
        private List<Vector2> rideData = new List<Vector2>();

        //list of run data; Y cood defines the miles
        private List<Vector2> runData = new List<Vector2>();

        //list of swim data ; Y cood defines the miles
        private List<Vector2> swimData = new List<Vector2>();

        //list of days of the week 
        private List<string> days = new List<string>();

        //list of disabled days
        private List<string> disabledDays = new List<string>();

        //selected day
        public string selectedDay { get; set; }

        //distance between each bar in the graph
        public float dayStep { get; set; }
        
        //initial offset of the day graph relative to the canvas
        public float initDayPositionY { get; set; }

        public List<Vector2> DayPosition
        {
            get
            {
                return dayPosition;
            }

        }

        public List<Vector2> RideData
        {
            get
            {
                return rideData;
            }

        }

        public List<string> Days
        {
            get
            {
                return days;
            }

        }

        public List<Vector2> RunData
        {
            get
            {
                return runData;
            }

        }

        public List<Vector2> SwimData
        {
            get
            {
                return swimData;
            }

        }

        public List<string> DisabledDays
        {
            get
            {
                return disabledDays;
            }

        }

        public override void Draw(CanvasDrawingSession session, Size parentSize)
        {
            //TODO: do the actual drawing here

            //distance between each bar in the graph
            dayStep = (float)parentSize.Width / 16;
            //initial X offset of the day graph relative to the canvas
            initGraphPositionX = (float)(parentSize.Width / 16);
            //initial Y offset of the day graph relative to the canvas
            initDayPositionY = (float)((parentSize.Height*3) /16);
            selectedDay = "W";

            //Add days of the week to be displayed for day chart
            Days.Add("S");
            Days.Add("S");
            Days.Add("M");
            Days.Add("T");
            Days.Add("W");
            Days.Add("R");
            Days.Add("F");

            //Add disabled days
            DisabledDays.Add("R");
            DisabledDays.Add("F");


            //RideData data
            RideData.Add(new Vector2(0, 20));
            RideData.Add(new Vector2(0, 2));
            RideData.Add(new Vector2(0, 50));
            RideData.Add(new Vector2(0, 50));
            RideData.Add(new Vector2(0, 100));
            RideData.Add(new Vector2(0, 80));
            RideData.Add(new Vector2(0, 40));

            //Run data
            RunData.Add(new Vector2(0, 24));
            RunData.Add(new Vector2(0, 52));
            RunData.Add(new Vector2(0, 60));
            RunData.Add(new Vector2(0, 28));
            RunData.Add(new Vector2(0, 150));
            RunData.Add(new Vector2(0, 89));
            RunData.Add(new Vector2(0, 75));


            //Swim data
            RunData.Add(new Vector2(0, 45));
            RunData.Add(new Vector2(0, 123));
            RunData.Add(new Vector2(0, 70));
            RunData.Add(new Vector2(0, 25));
            RunData.Add(new Vector2(0, 50));
            RunData.Add(new Vector2(0, 189));
            RunData.Add(new Vector2(0, 75));

            //Calculater the position of each bar in the dat graph
            for (int i = 0; i < 7; i++)
            {
                DayPosition.Add(new Vector2(initGraphPositionX + i*dayStep, initDayPositionY));
            }

            //Draw the day graph
            for (int j = 0; j < DayPosition.Count; j++)
            {

                //Check if the day is disabled        
                if (DisabledDays.Any(str => str.Contains(Days.ElementAt(j))))
                {
                    session.DrawText(Days.ElementAt(j), DayPosition[j] - new Vector2(10, -10), Colors.LightGray);
                }
                //else check if the day is selected
                else if (Days.ElementAt(j) == selectedDay)
                {
                    session.DrawText(Days.ElementAt(j), DayPosition[j] - new Vector2(10, -10), Color.FromArgb(255, 247, 99, 13));
                }
                //otherwise draw regular day
                else
                {
                    session.DrawText(Days.ElementAt(j), DayPosition[j] - new Vector2(10, -10), Colors.Black);
                }


                //Check if the day is disabled and draw the bar of the day graph accordingly
                if (DisabledDays.Any(str => str.Contains(Days.ElementAt(j))))
                {
                    session.DrawLine(DayPosition[j], new Vector2(DayPosition[j].X, DayPosition[j].Y + 1), Colors.Orange, 10);
                }
                //Check if no workout was done and draw the bar of the day graph accordingly
                else if (RideData.ElementAt(j).Y == 0)
                {
                    session.DrawLine(DayPosition[j], new Vector2(DayPosition[j].X, DayPosition[j].Y + 1), Color.FromArgb(255, 247, 99, 13), 10);
                }
                //otherwise a regular day so draw the graph accordingly
                else
                {
                    session.DrawLine(DayPosition[j], DayPosition[j] - RideData[j], Color.FromArgb(255, 247, 99, 13), 10);
                }
                
                //}

                }
            }


        }
    }


