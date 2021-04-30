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
        private static readonly Image pawn_black   = new Bitmap("assets/pawn_black.png");
        private static readonly Image bishop_black = new Bitmap("assets/bishop_black.png");
        private static readonly Image knight_black = new Bitmap("assets/knight_black.png");
        private static readonly Image queen_black  = new Bitmap("assets/queen_black.png");
        private static readonly Image king_black   = new Bitmap("assets/king_black.png");
        private static readonly Image rook_black   = new Bitmap("assets/rook_black.png");
        
        //white pieces
        private static readonly Image pawn_white   = new Bitmap("assets/pawn_white.png");
        private static readonly Image bishop_white = new Bitmap("assets/bishop_white.png");
        private static readonly Image knight_white = new Bitmap("assets/knight_white.png");
        private static readonly Image queen_white  = new Bitmap("assets/queen_white.png");
        private static readonly Image king_white   = new Bitmap("assets/king_white.png");
        private static readonly Image rook_white   = new Bitmap("assets/rook_white.png");

        private static readonly Image blank_image = new Bitmap(16, 16);
        // ReSharper restore InconsistentNaming
        
        #endregion

        /*
         * All positive ID's are for white pieces
         * All negative ID's are for black pieces
         *
         * 1 = pawn
         * 2 = rook
         * 3 = bishop
         * 4 = knight
         * 5 = queen
         * 6 = king
         *
         */

        
        private readonly SolidBrush _brush1 = new(Color.LightSalmon);
        private readonly SolidBrush _brush2 = new(Color.Chocolate);
        private Graphics _graphics;
        private int _boardSize = 50;

        private static Rectangle[][] _squares =
        {
            new Rectangle[] {new (),new (),new (),new (),new (),new (),new (),new ()},
            new Rectangle[] {new (),new (),new (),new (),new (),new (),new (),new ()},
            new Rectangle[] {new (),new (),new (),new (),new (),new (),new (),new ()},
            new Rectangle[] {new (),new (),new (),new (),new (),new (),new (),new ()},
            new Rectangle[] {new (),new (),new (),new (),new (),new (),new (),new ()},
            new Rectangle[] {new (),new (),new (),new (),new (),new (),new (),new ()},
            new Rectangle[] {new (),new (),new (),new (),new (),new (),new (),new ()},
            new Rectangle[] {new (),new (),new (),new (),new (),new (),new (),new ()},
        };

        private static short[][] _coords =
        {
            new short[] {new(), new(), new(), new(), new(), new(), new(), new()},
            new short[] {new(), new(), new(), new(), new(), new(), new(), new()},
            new short[] {new(), new(), new(), new(), new(), new(), new(), new()},
            new short[] {new(), new(), new(), new(), new(), new(), new(), new()},
            new short[] {new(), new(), new(), new(), new(), new(), new(), new()},
            new short[] {new(), new(), new(), new(), new(), new(), new(), new()},
            new short[] {new(), new(), new(), new(), new(), new(), new(), new()},
            new short[] {new(), new(), new(), new(), new(), new(), new(), new()},
        };

        private Image _getImageById(short id)
        {
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
                -3 => king_black,
                -4 => bishop_white,
                -5 => queen_black,
                -6 => knight_black,
                
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
            Point dif = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
            Location = Point.Add(_dragFormPoint, new Size(dif));
            if (MousePosition.Y == 0) {
                WindowState = FormWindowState.Maximized;
                _boardSize = 80;
                Refresh();
            }
        }
        
        private void panel1_MouseUp(object sender, MouseEventArgs e) => _dragging = false;
        
        #endregion
        
        public Form1()
        {
            InitializeComponent();
            BuildGrid();
            PlacePieces();
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            BuildGrid();
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    _graphics.DrawImage(_getImageById(_coords[y][x]), _squares[y][x]);
                }
            }

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
                    _squares[y][x] = tempRect;
                    
                    x++;

                    tempRect = new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize);
                    _graphics.FillRectangle(_brush2, tempRect);
                    _squares[y][x] = tempRect;
                    
                }

                y++;
                
                for (var x = 0; x < 8; x++)
                {
                    tempRect = new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize);
                    _graphics.FillRectangle(_brush2, tempRect);
                    _squares[y][x] = tempRect;

                    x++;

                    tempRect = new Rectangle(x * _boardSize + 40, y * _boardSize + 70, _boardSize, _boardSize);
                    _graphics.FillRectangle(_brush1, tempRect);
                    _squares[y][x] = tempRect;

                }
            }
        }

        private static void PlacePieces()
        {
            short id = 0;
            for (short y = 0; y < 8; y++) { 
                for (short x = 0; x < 8; x++) {
                    switch (y)
                    {
                        case 0: //top row
                            if (x < 4) id = (short) (x + 2);
                            else id = (short) ((-1)*x + 9);
                            break;
                        case 7: //bottom row
                            if (x < 4) id = (short) (-1*(x + 2));
                            else id = (short) (-1*((-1) * x + 9));
                            break;
                        case 1:
                            id = 1;
                            break;
                        case 6:
                            id = -1;
                            break;
                        default:
                            id = 0;
                            break;
                    }

                    _coords[y][x] = id;
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

            settings.Location = new Point(Right-146, settings.Location.Y);
            settingsButton.Location = new Point(Right-69, settingsButton.Location.Y);
            
            Refresh();
        }

        private void min_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void settingsButton_Click(object sender, EventArgs e)
        {
            if (settings.Visible) settings.Hide();
            else settings.Show();
        }
    }
}