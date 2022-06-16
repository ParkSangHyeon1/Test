using System;
using System.Collections.Generic;

namespace Test
{
    class Pos
    {
        public Pos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;

    }

    class Player
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        Random _random = new Random();
        Board _board;



        enum dir
        {
            Up = 0,
            Down = 2,
            Left = 1,
            Right = 3,
        }

        int _dir = (int)dir.Up;

        List<Pos> _points = new List<Pos>();

        public void Initialize(int posY, int posX, int destY, int destX, Board board)
        {
            PosX = posX;
            PosY = posY;

            _board = board;
            // 바라보고 있는 방향 기준 , 좌표 변화 
            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosY));

            //바라보는 방향 기준 // 좌표 변화

            //도착하기전까지 계속 실행
           　while (PosX != board.DestY || PosX != board.DestX)
            {
                //바라보는 방향으로 가는 지 확인
                if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    // 오른쪽으로 방향으로 90도 회전

                    _dir = (_dir - 1 + 4) % 4;

                    //앞으로 한보 전진

                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                //앞으로 한보
                else if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }

                else
                {
                    //왼쪽으로 전진
                    _dir = (_dir + 1 + 4) % 4;

                }
             }
        }
        const int MOVE_TICK = 10;
        int _sumTick = 0;
        int _lastIndex = 0;

        public void Update(int delataTick)
        {
            if (_lastIndex >= _points.Count)
                return;

            _sumTick += delataTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;
            }
        }
    }
}