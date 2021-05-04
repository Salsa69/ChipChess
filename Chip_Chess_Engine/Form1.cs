/*
Chip Chess Engine! All code written by Salsa (salsa##7717 on discord)
Written all in one class and one file (besides all the win forms stuff)
because object oriented programming is pretty pointless in this case

Probably going to re-write in c++ later but I don't care right now
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Chip_Chess_Engine
{

    public partial class Form1 : Form
    {
        #region Loading piece images to memory

        // ReSharper disable InconsistentNaming
        // black pieces
        private static readonly Image pawn_black = new Bitmap("assets/pawn_black.png");
        private static readonly Image bishop_black = new Bitmap("assets/bishop_black.png");
        private static readonly Image knight_black = new Bitmap("assets/knight_black.png");
        private static readonly Image queen_black = new Bitmap("assets/queen_black.png");
        private static readonly Image king_black = new Bitmap("assets/king_black.png");
        private static readonly Image rook_black = new Bitmap("assets/rook_black.png");

        //white pieces
        private static readonly Image pawn_white = new Bitmap("assets/pawn_white.png");
        private static readonly Image bishop_white = new Bitmap("assets/bishop_white.png");
        private static readonly Image knight_white = new Bitmap("assets/knight_white.png");
        private static readonly Image queen_white = new Bitmap("assets/queen_white.png");
        private static readonly Image king_white = new Bitmap("assets/king_white.png");
        private static readonly Image rook_white = new Bitmap("assets/rook_white.png");

        private static readonly Image blank_image = new Bitmap(1,1);
        // ReSharper restore InconsistentNaming

        #endregion

        private readonly SolidBrush _brush1 = new(Color.CadetBlue);
        private readonly SolidBrush _brush2 = new(Color.DarkSlateGray);
        private Graphics _graphics;
        private int _boardSize = 50;

        public int  ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        public int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;

        private static Rectangle[] _squares =
        {
            new(), new(), new(), new(), new(), new(), new(), new(),
            new(), new(), new(), new(), new(), new(), new(), new(),
            new(), new(), new(), new(), new(), new(), new(), new(),
            new(), new(), new(), new(), new(), new(), new(), new(),
            new(), new(), new(), new(), new(), new(), new(), new(),
            new(), new(), new(), new(), new(), new(), new(), new(),
            new(), new(), new(), new(), new(), new(), new(), new(),
            new(), new(), new(), new(), new(), new(), new(), new()
        };
        

        private static short[] _coords = // Initialized like a chess board
        {
             2,  3,  4,  5,  6,  4,  3,  2,
             1,  1,  1,  1,  1,  1,  1,  1,
             0,  0,  0,  0,  0,  0,  0,  0,
             0,  0,  0,  0,  0,  0,  0,  0,
             0,  0,  0,  0,  0,  0,  0,  0,
             0,  0,  0,  0,  0,  0,  0,  0,
            -1, -1, -1, -1, -1, -1, -1, -1,
            -2, -3, -4, -5, -6, -4, -3, -2
        };

        private SolidBrush GetBrush(int n) => (((int) (n / 8)) % 2 == 0 ? 2 - Convert.ToInt16((n % 8) % 2 == 0) : 1 + Convert.ToInt16((n % 8) % 2 == 0)) == 1 ? _brush1 : _brush2;
        
        
        private static Image GetImageById(short id) 
        {
            // Negative ID's are black, positive ID's are white
            // pawn = 1, rook = 2, knight = 3, bishop = 4, queen = 5, king = 6
            return id switch
            {
                1 => pawn_white,
                2 => rook_white,
                3 => knight_white,
                4 => bishop_white,
                5 => queen_white,
                6 => king_white,
                
                -1 => pawn_black,
                -2 => rook_black,
                -3 => knight_black,
                -4 => bishop_black,
                -5 => queen_black,
                -6 => king_black,
                
                _ => blank_image
            };
        }
        
        #region Window Move
        
        private bool _dragging;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if(WindowState == FormWindowState.Maximized) {
                WindowState = FormWindowState.Normal;
                Location = new Point(Left, 10);
                Cursor.Position = MousePosition.X < Left ? new Point(Location.X + 10, 15) : new Point(MousePosition.X, 15);
                Cursor.Position = MousePosition.X > Right ? new Point(Location.X + 500, 15) : new Point(MousePosition.X, 15);
                _boardSize = 50;
                Refresh();
            }
            _dragging = true;
            _dragCursorPoint = Cursor.Position;
            _dragFormPoint = Location;
        }
        
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            Location = Point.Add(_dragFormPoint, 
                new Size(Point.Subtract(Cursor.Position, new Size(_dragCursorPoint))));
            if (MousePosition.Y == 0) {
                WindowState = FormWindowState.Maximized;
                _boardSize = 80;
            }
        }
        
        private void panel1_MouseUp(object sender, MouseEventArgs e) => _dragging = false;
        
        #endregion
        
        public Form1()
        {
            InitializeComponent();
            
            BuildGrid();
            for (var i = 0; i < 64; i++)
            {
                _graphics.DrawImage(GetImageById(_coords[i]), _squares[i]);
            }
            Refresh();
        }

        private int GetPosFromPoint(Point p) =>
            8 * ((int) ((p.Y - Location.Y - 70) / _boardSize)) +
            ((int) ((p.X - Location.X - 40) / _boardSize));
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawScreen();
        }

        private void DrawScreen()
        {
            BuildGrid();
            
            for (var i = 0; i < 64; i++)
            {
                _graphics.DrawImage(GetImageById(_coords[i]), _squares[i]);
            }
        }

        private void DrawSquare(int x, int y)
        {
            SolidBrush tempBrush = GetBrush(8*y+x);
            _graphics.FillRectangle(tempBrush,
                new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize));
            _graphics.DrawImage(GetImageById(_coords[8*y+x]), _squares[8*y+x]);
        }
        
        private void BuildGrid()
        {
            _graphics = CreateGraphics();
            Rectangle tempRect;
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    tempRect = new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize);
                    _graphics.FillRectangle(_brush1, tempRect);
                    _squares[8*y+x] = tempRect;
                    
                    x++;

                    tempRect = new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize);
                    _graphics.FillRectangle(_brush2, tempRect);
                    _squares[8*y+x] = tempRect;
                    
                }

                y++;
                
                for (var x = 0; x < 8; x++)
                {
                    tempRect = new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize);
                    _graphics.FillRectangle(_brush2, tempRect);
                    _squares[8*y+x] = tempRect;

                    x++;

                    tempRect = new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize);
                    _graphics.FillRectangle(_brush1, tempRect);
                    _squares[8*y+x] = tempRect;

                }
            }
        }

        private void exit_Click(object sender, EventArgs e) => Environment.Exit(0);

        private void fullScrn_Click(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Maximized:
                    WindowState = FormWindowState.Normal;
                    _boardSize = 50;
                    break;
                case FormWindowState.Normal:
                    WindowState = FormWindowState.Maximized;
                    _boardSize = 80;
                    break;
            }
            
            Refresh();
        }

        private void form_MouseDown(object sender, EventArgs e)
        {
            Point pos = Cursor.Position;
            if (pos.X < (8 * _boardSize + Location.X + 40) &&
                pos.X > (Location.X + 40) &&
                pos.Y < (8 * _boardSize + Location.Y + 70) &&
                pos.Y > (Location.Y + 70)) 
            {
                _coords[GetPosFromPoint(pos)] = 0;
                DrawSquare(GetPosFromPoint(pos) % 8, (int) (GetPosFromPoint(pos) / 8));
            }
        }

        private void form_MouseUp(object sender, EventArgs e)
        {
            
        }

        private void form_MouseMove(object sender, EventArgs e)
        {
            
        }
        
        private void min_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

    }
}