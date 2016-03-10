using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Composition;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Kliva.Controls.Win2DCharting;
using Windows.Foundation;
using Windows.Graphics.DirectX;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace Kliva.Controls.Win2DCharting
{
    public class GraphControl : Control
    {
        Compositor _compositor;
        CanvasDevice device;
        CompositionGraphicsDevice compositionGraphicsDevice;
        Visual _backingVisual;
        DrawingSurfaceRenderer drawingSurfaceRenderer;
        SpriteVisual drawingSurfaceVisual;
        CompositionDrawingSurface drawingSurface;
        List<ChartComponentBase> chartComponents = new List<ChartComponentBase>();

        private List<Vector2> test = new List<Vector2>();

        public string Name { get; set; }

        public GraphControl()
        {
            Loaded += GraphControl_Loaded;
            SizeChanged += GraphControl_SizeChanged;

            //TODO: add other components in here and configure state
            chartComponents.Add(new DayChart());
            chartComponents.Add(new WeekChart());
            chartComponents.Add(new CircleChart());
            chartComponents.Add(new totalWorkout());
        }

        private void GraphControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //TODO: update sizes

           
        }

        private void GraphControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _backingVisual = ElementCompositionPreview.GetElementVisual(this as UIElement);
            _compositor = _backingVisual.Compositor;
            CreateDevice();
            CreateSurface(_compositor, compositionGraphicsDevice);
            
            ElementCompositionPreview.SetElementChildVisual(this as UIElement, drawingSurfaceVisual);
        }

        private void CreateSurface(Compositor compositor, CompositionGraphicsDevice compositionGraphicsDevice)
        {
            drawingSurfaceVisual = compositor.CreateSpriteVisual();
            drawingSurface = compositionGraphicsDevice.CreateDrawingSurface(this.RenderSize, DirectXPixelFormat.B8G8R8A8UIntNormalized, DirectXAlphaMode.Ignore);
            drawingSurfaceVisual.Brush = compositor.CreateSurfaceBrush(drawingSurface);
            drawingSurfaceVisual.Size = new Vector2((float)this.RenderSize.Width, (float)this.RenderSize.Height);
 
      
      DrawDrawingSurface();

            compositionGraphicsDevice.RenderingDeviceReplaced += CompositionGraphicsDevice_RenderingDeviceReplaced;
        }

        void CompositionGraphicsDevice_RenderingDeviceReplaced(CompositionGraphicsDevice sender, RenderingDeviceReplacedEventArgs args)
        {
            DrawDrawingSurface();
        }

        void DrawDrawingSurface()
        {
            //++drawCount;

            using (var ds = CanvasComposition.CreateDrawingSession(drawingSurface))
            {
                //This is the bounds of the actual control.. in red
                //ds.Clear(Colors.Red);

                foreach (var item in chartComponents)
                {
                    item.Draw(ds, this.RenderSize);
                }
                

                //var rect = new Rect(new Point(2, 2), (drawingSurface.Size.ToVector2() - new Vector2(4, 4)).ToSize());

                //ds.FillRoundedRectangle(rect, 15, 15, Colors.LightBlue);
                //ds.DrawRoundedRectangle(rect, 15, 15, Colors.Gray, 2);

                //ds.DrawText("This is a composition drawing surface", rect, Colors.Black, new CanvasTextFormat()
                //{
                //    FontFamily = "Comic Sans MS",
                //    FontSize = 32,
                //    WordWrapping = CanvasWordWrapping.WholeWord,
                //    VerticalAlignment = CanvasVerticalAlignment.Center,
                //    HorizontalAlignment = CanvasHorizontalAlignment.Center
                //});

                //ds.DrawText("Draws: " + drawCount, rect, Colors.Black, new CanvasTextFormat()
                //{
                //    FontSize = 10,
                //    VerticalAlignment = CanvasVerticalAlignment.Bottom,
                //    HorizontalAlignment = CanvasHorizontalAlignment.Center
                //});
            }
        }

        void CreateDevice()
        {
            device = CanvasDevice.GetSharedDevice();
            //TODO: devicelost
            //device.DeviceLost += Device_DeviceLost;

            if (compositionGraphicsDevice == null)
            {
                compositionGraphicsDevice = CanvasComposition.CreateCompositionGraphicsDevice(_compositor, device);
            }
        }

        public List<Vector2> Test
        {
            get
            {
                return test;
            }


        }

    
    }
}
