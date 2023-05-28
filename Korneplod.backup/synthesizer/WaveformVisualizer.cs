using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace synthesizer
{
    class WaveformVisualizer : FrameworkElement
    {
        public static readonly DependencyProperty WaveformProperty = DependencyProperty.Register(
            "Waveform",
            typeof(float[]),
            typeof(WaveformVisualizer),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.None,
                Changed_Waveform,
                Coerce_Waveform
            ));

        static object Coerce_Waveform(DependencyObject d, object v)
        {
            return v;
        }

        static void Changed_Waveform(DependencyObject d, DependencyPropertyChangedEventArgs a)
        {
            var fv = d as WaveformVisualizer;
            if (fv != null)
            {
                fv.InvalidateVisual();
            }
        }

        public float[] Waveform
        {
            get
            {
                return (float[])GetValue(WaveformProperty);
            }
            set
            {
                if (Waveform != value)
                {
                    SetValue(WaveformProperty, value);
                }
            }
        }

        public WaveformVisualizer()
        {
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var width = ActualWidth;
            var height = ActualHeight;
            var h2 = height * 0.5;

            drawingContext.DrawRectangle(Brushes.Black, null, new Rect(0, 0, width, height));
            drawingContext.DrawLine(new Pen(Brushes.Orange, 5), new Point(0, 0), new Point(1,1));

            var samples = Waveform;
            if (samples != null && samples.Length - 1 > 0)
            {
                var length = samples.Length;
                byte filler = 0;
                var cellWidth = this.Width / length;

                for (var iter = 0; iter < 2 * length - 1; ++iter)
                {
                    filler = (byte)(iter % 2 == 0 ? 0 : 1);
                    //var h = h2 * Math.Min(Math.Max(-1.0, samples[(int)iter / 2]), 5.0) + h2;
                    double h = h2 * Math.Min(Math.Max(-1.0, samples[(int)iter / 2]), 5.0) + h2;
                    //drawingContext.DrawLine(Brushes.Crimson, null, new Rect(iter * cellWidth / 2, h, cellWidth / 2, 5));
                    //drawingContext.DrawLine(Brushes.Goldenrod, null, new Rect(iter * cellWidth / 2, h, cellWidth / 2, 3));
                    double next_h = h2 * Math.Min(Math.Max(-1.0, samples[(int)(iter+1) / 2]), 5.0) + h2;
                    drawingContext.DrawLine(new Pen(Brushes.Orange, 3), new Point(iter * 2, h), new Point(iter * 2 + 1,next_h));

                }
            }
        }
    }
}