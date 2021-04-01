using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

public struct Piece
{
    public ushort ID;
    public int X;
    public int Y;
    public string Name;
}

namespace Chip_Chess_Engine
{
    public partial class Form1 : Form
    {
        private readonly SolidBrush _brush1 = new(Color.LightSalmon);
        private readonly SolidBrush _brush2 = new(Color.Chocolate);
        private Graphics _graphics;
        private int _boardSize = 50;
        
        private bool _dragging;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;

        private Rectangle[/* y coord */][/* x coord */] _squares;
        
        #region Window Move
        
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if(WindowState == FormWindowState.Maximized) {
                WindowState = FormWindowState.Normal;
                Location = new Point(Left, 10);
                Cursor.Position = MousePosition.X < Left ? new Point(Location.X + 10, 15) : new Point(MousePosition.X, 15);
                _boardSize = 50;
                Refresh();
            }
            _dragging = true;
            _dragCursorPoint = Cursor.Position;
            _dragFormPoint = this.Location;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                Location = Point.Add(_dragFormPoint, new Size(dif));
                if (MousePosition.Y == 0) {
                    WindowState = FormWindowState.Maximized;
                    _boardSize = 80;
                    Refresh();
                }
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e) => _dragging = false;
        
        #endregion
        
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            BuildGrid();
        }
        
        private void BuildGrid()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    _graphics = this.CreateGraphics();
                    _graphics.FillRectangle(_brush1, new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize));
                    _squares[y][x] = new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize);
                    
                    x++;
                    
                    _graphics = this.CreateGraphics();
                    _graphics.FillRectangle(_brush2, new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize));
                    _squares[y][x] = new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize);
                    
                }

                y++;
                
                for (int x = 0; x < 8; x++)
                {
                    _graphics = this.CreateGraphics();
                    _graphics.FillRectangle(_brush2, new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize));

                    x++;
                    
                    _graphics = this.CreateGraphics();
                    _graphics.FillRectangle(_brush1, new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize));
                    
                }
            }
        }

        private void PlacePieces()
        {
            
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void fullScrn_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized) {
                WindowState = FormWindowState.Normal;
                _boardSize = 50;
            }
            else if (WindowState == FormWindowState.Normal) {
                WindowState = FormWindowState.Maximized;
                _boardSize = 80;
            }
            Refresh();
        }

        private void min_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
    }
}