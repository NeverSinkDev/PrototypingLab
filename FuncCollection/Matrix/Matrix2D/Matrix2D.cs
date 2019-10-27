using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuncCollection.Matrix.Matrix2D
{
    public class Matrix2D<T> : IEnumerable<Cell<T>>
    {
        public EnumeratorConfiguration2D EnumConfig = new EnumeratorConfiguration2D();

        public int XCount { get; set; }

        public int YCount { get; set; }

        public Cell<T>[,] Data { get; set; }

        public Cell<T> this[int x, int y]
        {
            get
            {
                if (!this.ValidateCoordinates(x, y))
                {
                    return Cell<T>.NullCell;
                }

                return this.Data[x, y];
            }

            set
            {
                if (!this.ValidateCoordinates(x, y))
                {
                    return;
                }

                this.Data[x, y] = value;
            }
        }

        public void Fill(T value)
        {
            for (int i = 0; i < this.XCount; i++)
            {
                for (int j = 0; j < XCount; j++)
                {
                    this.Encapsulate(value, i, j);
                }
            }
        }

        public void Fill(IEnumerable<T> values)
        {
            var en = values.GetEnumerator();

            for (int i = 0; i < this.XCount; i++)
            {
                for (int j = 0; j < XCount; j++)
                {
                    en.MoveNext();
                    this.Encapsulate(en.Current, i, j);
                }
            }
        }

        public void Fill(Func<int, int, T> valueFunc)
        {
            for (int i = 0; i < this.XCount; i++)
            {
                for (int j = 0; j < XCount; j++)
                {
                    this.Encapsulate(valueFunc(i, j), i, j);
                }
            }
        }

        public void Init(int x, int y)
        {
            this.Data = new Cell<T>[x, y];
            this.XCount = x;
            this.YCount = y;
        }

        public void Encapsulate(T value, int x, int y)
        {
            this[x, y] = new Cell<T>()
            {
                X = x,
                Y = y,
                Master = this,
                Value = value
            };
        }

        public bool ValidateCoordinates(int x, int y)
        {
            if (x < 0 || x > XCount - 1)
            {
                return false;
            }

            if (y < 0 || y > YCount - 1)
            {
                return false;
            }

            return true;
        }

        public IEnumerator<Cell<T>> GetEnumerator()
        {
            this.EnumConfig.ResetExecution();
            var x = this.EnumConfig.XStart;
            var y = this.EnumConfig.YStart;

            for (int i = 0; i < this.EnumConfig.PrimaryDirection.Count; i++)
            {
                var cell = this[x, y];
                yield return cell;

                while (true)
                {
                    this.EnumConfig.MoveToNextSecondary(ref x, ref y);
                    cell = this[x, y];

                    if (cell.Valid)
                    {
                        yield return cell;
                        continue;
                    }

                    this.EnumConfig.MoveToNextPrimary(ref x, ref y);
                    cell = this[x, y];

                    if (this[x, y].Valid)
                    {
                        yield return cell;
                        continue;
                    }

                    break;
                }
                this.EnumConfig.CurrentPrimaryIndex++;
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<IEnumerable<Cell<T>>> ToRowEnum()
        {
            this.EnumConfig.ResetExecution();
            var x = this.EnumConfig.XStart;
            var y = this.EnumConfig.YStart;

            for (int i = 0; i < this.EnumConfig.PrimaryDirection.Count; i++)
            {
                var cell = this[x, y];
                var cellList = new List<Cell<T>>();
                cellList.Add(cell);

                while (true)
                {
                    this.EnumConfig.MoveToNextSecondary(ref x, ref y);
                    cell = this[x, y];

                    if (cell.Valid)
                    {
                        cellList.Add(cell);
                        continue;
                    }

                    yield return cellList;
                    cellList = new List<Cell<T>>();

                    this.EnumConfig.MoveToNextPrimary(ref x, ref y);
                    cell = this[x, y];

                    if (this[x, y].Valid)
                    {
                        cellList.Add(cell);
                        continue;
                    }

                    break;
                }
                this.EnumConfig.CurrentPrimaryIndex++;
            }
            yield break;
        }
    }

    public class Cell<T>
    {
        public static Cell<T> NullCell = new Cell<T>() {Valid = false};

        public bool Valid { get; private set; } = true;

        public Matrix2D<T> Master { get; set; }

        public T Value { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Cell<T> GetNeigbour(Direction a = Direction.Non, Direction b = Direction.Non)
        {
            return this.Master[EnumeratorConfiguration2D.ConvertDirection(this.X, a),
                EnumeratorConfiguration2D.ConvertDirection(this.Y, b)];
        }
    }

    public class EnumeratorConfiguration2D
    {
        public int CurrentPrimaryIndex { get; set; } = 0;

        public int PrimaryX = 0;
        public int PrimaryY = 0;

        public List<Tuple<Direction, Direction>> PrimaryDirection { get; set; } =
            new List<Tuple<Direction, Direction>>() { new Tuple<Direction, Direction>(Direction.Inc, Direction.Non) };

        public Tuple<Direction, Direction> SecondaryDirection { get; set; } =
            new Tuple<Direction, Direction>(Direction.Non, Direction.Inc);

        public int XStart { get; set; } = 0;

        public int YStart { get; set; } = 0;

        public void MoveToNext(ref int x, ref int y, Tuple<Direction, Direction> dir2)
        {
            x = ConvertDirection(x, dir2.Item1);
            y = ConvertDirection(y, dir2.Item2);
        }

        public void MoveToNextPrimary(ref int x, ref int y)
        {
            MoveToNext(ref PrimaryX, ref PrimaryY, this.PrimaryDirection[this.CurrentPrimaryIndex]);
            x = PrimaryX;
            y = PrimaryY;
        }
        public void MoveToNextSecondary(ref int x, ref int y)
        {
            MoveToNext(ref x, ref y, this.SecondaryDirection);
        }

        public void ResetConfiguration()
        {
            this.PrimaryDirection.Clear();
            this.XStart = 0;
            this.YStart = 0;
            this.ResetExecution();
        }

        public void ResetExecution()
        {
            this.CurrentPrimaryIndex = 0;
            this.PrimaryX = XStart;
            this.PrimaryY = YStart;
        }

        public void ApplyPresetHorizontal()
        {
            this.ResetConfiguration();
            this.PrimaryDirection.Add(new Tuple<Direction, Direction>(Direction.Non, Direction.Inc));
            this.SecondaryDirection = new Tuple<Direction, Direction>(Direction.Inc, Direction.Non);
        }

        public void ApplyPresetVertical()
        {
            this.ResetConfiguration();
            this.PrimaryDirection.Add(new Tuple<Direction, Direction>(Direction.Inc, Direction.Non));
            this.SecondaryDirection = new Tuple<Direction, Direction>(Direction.Non, Direction.Inc);
        }

        public void ApplyPresetDiagonal1()
        {
            this.ResetConfiguration();
            this.PrimaryDirection.Add(new Tuple<Direction, Direction>(Direction.Non, Direction.Inc));
            this.PrimaryDirection.Add(new Tuple<Direction, Direction>(Direction.Inc, Direction.Non));
            this.SecondaryDirection = new Tuple<Direction, Direction>(Direction.Inc, Direction.Sub);
        }

        public void ApplyPresetDiagonal2(int ystart)
        {
            this.ResetConfiguration();
            this.YStart = ystart;
            this.PrimaryDirection.Add(new Tuple<Direction, Direction>(Direction.Inc, Direction.Non));
            this.PrimaryDirection.Add(new Tuple<Direction, Direction>(Direction.Non, Direction.Sub));
            this.SecondaryDirection = new Tuple<Direction, Direction>(Direction.Inc, Direction.Sub);
        }

        public static int ConvertDirection(int val, Direction d)
        {
            switch (d)
            {
                case Direction.Sub:
                    return --val;
                case Direction.Inc:
                    return ++val;
            }

            return val;
        }
    }

    public enum Direction
    {
        Sub = 0,
        Non = 1,
        Inc = 2

    }
}

