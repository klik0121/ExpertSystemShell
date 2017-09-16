using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDemo
{
    public class ParkingZoneState
    {
        protected double xmin;
        protected double xmax;
        protected double ymin;
        protected double ymax;

        protected double truckLength;
        protected double truckWidth;
        protected Rectangle truckRectangle;

        public double X { get; set; }
        public double Fi { get; set; }
        public double Teta { get; set; }
        public double Y { get; set; }

        public ParkingZoneState(double xmin = -150, double xmax = 150, double ymin = 0,
            double ymax = 300, double truckLength = 20, double truckWidth = 10)
        {
            this.xmin = xmin;
            this.xmax = xmax;
            this.ymin = ymin;
            this.ymax = ymax;
            this.truckLength = truckLength;
            this.truckWidth = truckWidth;

            //иницализация какими-то начальными значениями,
            //на случай вызова метода Draw(Graphics g)
            X = -50;
            Fi = -50;
            Teta = 0;
        }

        public void Draw(Graphics g)
        {
            Draw(g, X, Y, Fi, Teta);
        }

        protected void Draw(Graphics g, double x, double y, double fi, double teta)
        {
            float widthScaled = (float)XToGraphics(xmin + truckWidth, g);
            float lengthScaled = (float)YToGraphics(ymin + truckLength, g);
            float scaledX = (float)XToGraphics(x, g);
            float scaledY = (float)YToGraphics(y, g);
            truckRectangle = new Rectangle((int) (scaledX - widthScaled/2),
                (int) (scaledY), (int) widthScaled, (int) lengthScaled);
            using (Matrix m = new Matrix())
            using (Matrix m1 = new Matrix())
            {
                Pen p = new Pen(Color.Red, 2);
                p.EndCap = LineCap.DiamondAnchor;
                m1.RotateAt((float)(fi + teta), new PointF(scaledX, scaledY + lengthScaled));
                g.Transform = m1;
                g.DrawLine(p, scaledX, scaledY + lengthScaled, scaledX, scaledY + 3 * (lengthScaled / 2));
                g.ResetTransform();
                m.RotateAt((float)fi, new PointF(scaledX, scaledY + lengthScaled));
                g.Transform = m;
                g.FillRectangle(Brushes.Brown, truckRectangle);
                p.Color = Color.Brown;                
                g.DrawLine(p, scaledX, scaledY + lengthScaled, scaledX, scaledY + 3*(lengthScaled/2));
                g.ResetTransform();
            }
        }

        protected void DrawTeta(Graphics g, double x, double y, double teta)
        {
            double widthScaled = XToGraphics(xmin + truckWidth, g);
            double lengthScaled = YToGraphics(ymin + truckLength, g);
            float scaledX = (float)XToGraphics(x, g);
            float scaledY = (float)YToGraphics(y, g);
            using (Matrix m = new Matrix())
            {
                m.RotateAt((float)teta, new PointF(scaledX, scaledY));
                g.Transform = m;
                Pen p = new Pen(Color.Red, 2);
                p.StartCap = LineCap.DiamondAnchor;
                g.DrawLine(p, (int)(scaledX - widthScaled/2),
                    (int)(scaledY), (int)(scaledX - widthScaled/2), (float)(scaledY + lengthScaled/2));
                g.ResetTransform();
            }
        }

        protected double XToGraphics(double x, Graphics g)
        {
            double scale = g.VisibleClipBounds.Width/(xmax - xmin);
            return (x - xmin)*scale + g.VisibleClipBounds.Left;
        }

        protected double YToGraphics(double y, Graphics g)
        {
            double scale = g.VisibleClipBounds.Height/(xmax - xmin);
            return (y - ymin)*scale + g.VisibleClipBounds.Top;
        }
    }
}
