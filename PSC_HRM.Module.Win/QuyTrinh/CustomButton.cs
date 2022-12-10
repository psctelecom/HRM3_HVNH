using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    [ToolboxBitmap(typeof(Button)), 
    ToolboxItem(true), 
    DefaultEvent("Click"),
    DefaultProperty("Caption"),
    ToolboxItemFilter("System.Windows.Forms")]
    public class CustomButton : UserControl
    {
        public enum ButtonState : byte
        {
            Disable,
            Normal,
            Hover
        }

        // Fields...
        private ButtonState _State = ButtonState.Disable;
        private int _Radial = 10;
        private string _Caption = "Custom Button";
        private Color _HoverColor = Color.YellowGreen;
        private Color _NormalColor = Color.WhiteSmoke;
        private Color _DisableColor = Color.FromArgb(177, 173, 136);
        private bool _IsHover;
        
        [Browsable(true)]
        [Category("Appearance")]
        public Color DisableColor
        {
            get { return _DisableColor; }
            set
            {
                _DisableColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Color NormalColor
        {
            get { return _NormalColor; }
            set
            {
                _NormalColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Color HoverColor
        {
            get { return _HoverColor; }
            set
            {
                _HoverColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public int Radial
        {
            get { return _Radial; }
            set
            {
                _Radial = value;
                Invalidate();
            }
        }
        
        [Browsable(true)]
        public string Caption
        {
            get { return _Caption; }
            set
            {
                _Caption = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        public ButtonState State
        {
            get { return _State; }
            set
            {
                _State = value;
                if (value == ButtonState.Disable)
                    Enabled = false;
                else
                    Enabled = true;
                Invalidate();
            }
        }        

        public CustomButton()
        {
            Width = 85;
            Height = 61;
            BackColor = Color.White;
            Cursor = Cursors.Hand;
            Invalidate();
        }

        /// <summary>
        /// Override paint method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Rectangle rect = e.ClipRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            DrawBackground(e.Graphics, rect);
            DrawBorder(e.Graphics, rect);
            rect = new Rectangle(e.ClipRectangle.X + 3, e.ClipRectangle.Y + 3, e.ClipRectangle.Width - 6, e.ClipRectangle.Height - 6);
            DrawText(e.Graphics, rect);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Enabled && !_IsHover)
            {
                State = ButtonState.Hover;
                _IsHover = true;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (Enabled && _IsHover)
            {
                State = ButtonState.Normal;
                _IsHover = false;
            }
        }
        
        /// <summary>
        /// Create button path
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private GraphicsPath CreateBorder(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddBezier(new Point(rect.X, rect.Y + Radial), new Point(rect.X, rect.Y), new Point(rect.X + Radial, rect.Y), new Point(rect.X + Radial, rect.Y));
            path.AddLine(new Point(rect.X + Radial, rect.Y), new Point(rect.Width - Radial, rect.Y));
            path.AddBezier(new Point(rect.Width - Radial, rect.Y), new Point(rect.Width, rect.Y), new Point(rect.Width, rect.Y + Radial), new Point(rect.Width, rect.Y + Radial));
            path.AddLine(new Point(rect.Width, rect.Y + Radial), new Point(rect.Width, rect.Height - Radial));
            path.AddBezier(new Point(rect.Width, rect.Height - Radial), new Point(rect.Width, rect.Height), new Point(rect.Width - Radial, rect.Height), new Point(rect.Width - Radial, rect.Height));
            path.AddLine(new Point(rect.Width - Radial, rect.Height), new Point(rect.X + Radial, rect.Height));
            path.AddBezier(new Point(rect.X + Radial, rect.Height), new Point(rect.X, rect.Height), new Point(rect.X, rect.Height - Radial), new Point(rect.X, rect.Height - Radial));
            path.AddLine(new Point(rect.X, rect.Height - Radial), new Point(rect.X, rect.Y + Radial));

            return path;
        }

        /// <summary>
        /// Create brush
        /// </summary>
        /// <returns></returns>
        private SolidBrush CreateBrush()
        {
            SolidBrush brush;
            if (State == ButtonState.Disable || !Enabled)
                brush = new SolidBrush(DisableColor);
            else if (State == ButtonState.Normal)
                brush = new SolidBrush(NormalColor);
            else if (State == ButtonState.Hover)
                brush = new SolidBrush(HoverColor);
            else
                brush = new SolidBrush(DisableColor);

            return brush;
        }

        /// <summary>
        /// Draw button background
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        private void DrawBackground(Graphics g, Rectangle rect)
        {
            GraphicsPath path = CreateBorder(rect);
            SolidBrush brush = CreateBrush();
            g.FillPath(brush, path);
        }

        /// <summary>
        /// Draw button border
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        private void DrawBorder(Graphics g, Rectangle rect)
        {
            GraphicsPath path = CreateBorder(rect);
            g.DrawPath(new Pen(Color.DimGray, 1.0f), path);
        }

        /// <summary>
        /// Draw button text
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        private void DrawText(Graphics g, Rectangle rect)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            g.DrawString(Caption, new Font("Tahoma", 9.0f), Brushes.Black, rect, format);
        }
    }
}
