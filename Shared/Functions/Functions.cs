using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Text.RegularExpressions;

public static class Functions
{
    public static bool CompareBytes(byte[] a, byte[] b)
    {
        if (a == b) return true;

        if (a == null || b == null || a.Length != b.Length) return false;

        for (int i = 0; i < a.Length; i++) if (a[i] != b[i]) return false;

        return true;
    }

    public static string ConvertByteSize(double byteCount)
    {
        string size = "0 Bytes";
        if (byteCount >= 1073741824.0)
            size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
        else if (byteCount >= 1048576.0)
            size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
        else if (byteCount >= 1024.0)
            size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
        else if (byteCount > 0 && byteCount < 1024.0)
            size = byteCount.ToString() + " Bytes";

        return size;
    }

    public static bool TryParse(string s, out Point temp)
    {
        temp = Point.Empty;

        if (String.IsNullOrWhiteSpace(s)) return false;

        string[] data = s.Split(',');
        if (data.Length <= 1) return false;

        if (!Int32.TryParse(data[0], out int tempX))
            return false;

        if (!Int32.TryParse(data[1], out int tempY))
            return false;

        temp = new Point(tempX, tempY);
        return true;
    }
    public static Point Subtract(this Point p1, Point p2)
    {
        return new Point(p1.X - p2.X, p1.Y - p2.Y);
    }
    public static Point Subtract(this Point p1, int x, int y)
    {
        return new Point(p1.X - x, p1.Y - y);
    }
    public static Point Add(this Point p1, Point p2)
    {
        return new Point(p1.X + p2.X, p1.Y + p2.Y);
    }
    public static Point Add(this Point p1, int x, int y)
    {
        return new Point(p1.X + x, p1.Y + y);
    }
    public static string PointToString(Point p)
    {
        return String.Format("{0}, {1}", p.X, p.Y);
    }
    public static bool InRange(Point a, Point b, int i)
    {
        return Math.Abs(a.X - b.X) <= i && Math.Abs(a.Y - b.Y) <= i;
    }

    public static bool FacingEachOther(MirDirection dirA, Point pointA, MirDirection dirB, Point pointB)
    {
        if (dirA == DirectionFromPoint(pointA, pointB) && dirB == DirectionFromPoint(pointB, pointA))
        {
            return true;
        }

        return false;
    }

    public static string PrintTimeSpanFromSeconds(double secs, bool accurate = true)
    {
        TimeSpan t = TimeSpan.FromSeconds(secs);
        string answer;
        if (t.TotalMinutes < 1.0)
        {
            answer = string.Format("{0}秒", t.Seconds);
        }
        else if (t.TotalHours < 1.0)
        {
            answer = accurate ? string.Format("{0}分钟 {1:D2}秒", t.Minutes, t.Seconds) : string.Format("{0}分钟", t.Minutes);
        }
        else if (t.TotalDays < 1.0)
        {
            answer = accurate ? string.Format("{0}小时 {1:D2}分钟 {2:D2}秒", (int)t.Hours, t.Minutes, t.Seconds) : string.Format("{0}小时{1:D2}分分钟", (int)t.TotalHours, t.Minutes);
        }
        else // more than 1 day
        {
            answer = accurate ? string.Format("{0}天 {1:D2}小时{2:D2}分钟{3:D2}秒", (int)t.Days, (int)t.Hours, t.Minutes, t.Seconds) : string.Format("{0}天 {1}小时 {2:D2}分钟", (int)t.TotalDays, (int)t.Hours, t.Minutes);
        }

        return answer;
    }

    public static string PrintTimeSpanFromMilliSeconds(double milliSeconds)
    {
        TimeSpan t = TimeSpan.FromMilliseconds(milliSeconds);
        string answer;
        if (t.TotalMinutes < 1.0)
        {
            answer = string.Format("{0}.{1}秒", t.Seconds, (decimal)(t.Milliseconds / 100));
        }
        else if (t.TotalHours < 1.0)
        {
            answer = string.Format("{0}分钟 {1:D2}秒", t.TotalMinutes, t.Seconds);
        }
        else if (t.TotalDays < 1.0)
        {
            answer = string.Format("{0}小时 {1:D2}分钟 {2:D2}秒", (int)t.TotalHours, t.Minutes, t.Seconds);
        }
        else
        {
            answer = string.Format("{0}天 {1}小时 {2:D2}分钟 {3:D2}秒", (int)t.Days, (int)t.Hours, t.Minutes, t.Seconds);
        }

        return answer;
    }

    public static MirDirection PreviousDir(MirDirection d)
    {
        switch (d)
        {
            case MirDirection.Up:
                return MirDirection.UpLeft;
            case MirDirection.UpRight:
                return MirDirection.Up;
            case MirDirection.Right:
                return MirDirection.UpRight;
            case MirDirection.DownRight:
                return MirDirection.Right;
            case MirDirection.Down:
                return MirDirection.DownRight;
            case MirDirection.DownLeft:
                return MirDirection.Down;
            case MirDirection.Left:
                return MirDirection.DownLeft;
            case MirDirection.UpLeft:
                return MirDirection.Left;
            default: return d;
        }
    }
    public static MirDirection NextDir(MirDirection d)
    {
        switch (d)
        {
            case MirDirection.Up:
                return MirDirection.UpRight;
            case MirDirection.UpRight:
                return MirDirection.Right;
            case MirDirection.Right:
                return MirDirection.DownRight;
            case MirDirection.DownRight:
                return MirDirection.Down;
            case MirDirection.Down:
                return MirDirection.DownLeft;
            case MirDirection.DownLeft:
                return MirDirection.Left;
            case MirDirection.Left:
                return MirDirection.UpLeft;
            case MirDirection.UpLeft:
                return MirDirection.Up;
            default: return d;
        }
    }
    public static MirDirection DirectionFromPoint(Point source, Point dest)
    {
        if (source.X < dest.X)
        {
            if (source.Y < dest.Y)
                return MirDirection.DownRight;
            if (source.Y > dest.Y)
                return MirDirection.UpRight;
            return MirDirection.Right;
        }

        if (source.X > dest.X)
        {
            if (source.Y < dest.Y)
                return MirDirection.DownLeft;
            if (source.Y > dest.Y)
                return MirDirection.UpLeft;
            return MirDirection.Left;
        }

        return source.Y < dest.Y ? MirDirection.Down : MirDirection.Up;
    }

    public static MirDirection ShiftDirection(MirDirection dir, int i)
    {
        return (MirDirection)(((int)dir + i + 8) % 8);
    }

    public static Size Add(this Size p1, Size p2)
    {
        return new Size(p1.Width + p2.Width, p1.Height + p2.Height);
    }
    public static Size Add(this Size p1, int width, int height)
    {
        return new Size(p1.Width + width, p1.Height + height);
    }

    public static Point PointMove(Point p, MirDirection d, int i)
    {
        switch (d)
        {
            case MirDirection.Up:
                p.Offset(0, -i);
                break;
            case MirDirection.UpRight:
                p.Offset(i, -i);
                break;
            case MirDirection.Right:
                p.Offset(i, 0);
                break;
            case MirDirection.DownRight:
                p.Offset(i, i);
                break;
            case MirDirection.Down:
                p.Offset(0, i);
                break;
            case MirDirection.DownLeft:
                p.Offset(-i, i);
                break;
            case MirDirection.Left:
                p.Offset(-i, 0);
                break;
            case MirDirection.UpLeft:
                p.Offset(-i, -i);
                break;
        }
        return p;
    }
    public static Point Left(Point p, MirDirection d)
    {
        switch (d)
        {
            case MirDirection.Up:
                p.Offset(-1, 0);
                break;
            case MirDirection.UpRight:
                p.Offset(-1, -1);
                break;
            case MirDirection.Right:
                p.Offset(0, -1);
                break;
            case MirDirection.DownRight:
                p.Offset(1, -1);
                break;
            case MirDirection.Down:
                p.Offset(1, 0);
                break;
            case MirDirection.DownLeft:
                p.Offset(1, 1);
                break;
            case MirDirection.Left:
                p.Offset(0, 1);
                break;
            case MirDirection.UpLeft:
                p.Offset(-1, 1);
                break;
        }
        return p;
    }

    public static Point Right(Point p, MirDirection d)
    {
        switch (d)
        {
            case MirDirection.Up:
                p.Offset(1, 0);
                break;
            case MirDirection.UpRight:
                p.Offset(1, 1);
                break;
            case MirDirection.Right:
                p.Offset(0, 1);
                break;
            case MirDirection.DownRight:
                p.Offset(-1, 1);
                break;
            case MirDirection.Down:
                p.Offset(-1, 0);
                break;
            case MirDirection.DownLeft:
                p.Offset(-1, -1);
                break;
            case MirDirection.Left:
                p.Offset(0, -1);
                break;
            case MirDirection.UpLeft:
                p.Offset(1, -1);
                break;
        }
        return p;
    }
    public static bool HasClassWeapon(int Weapon, MirClass Class)
    {

        switch (Weapon / Globals.ClassWeaponCount)
        {
            case 1:
                return Class == MirClass.刺客 || Class == MirClass.飞燕刺客;
            case 2:
                return Class == MirClass.弓手 || Class == MirClass.暗鬼弓手;
            case 3:
                return Class == MirClass.碧血战士;
            case 4:
                return Class == MirClass.虹玄法师;
            case 5:
                return Class == MirClass.翊仙道士;
            case 6:
                return Class == MirClass.飞燕刺客;
            case 7:
                return Class == MirClass.暗鬼弓手;
            default:
                return Class == MirClass.战士 || Class == MirClass.法师 || Class == MirClass.道士 || Class == MirClass.碧血战士 || Class == MirClass.虹玄法师 || Class == MirClass.翊仙道士;
            case 8:
                return false;
        }

    }
    public static int MaxDistance(Point p1, Point p2)
    {
        return Math.Max(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
    }

    public static MirDirection ReverseDirection(MirDirection dir)
    {
        switch (dir)
        {
            case MirDirection.Up:
                return MirDirection.Down;
            case MirDirection.UpRight:
                return MirDirection.DownLeft;
            case MirDirection.Right:
                return MirDirection.Left;
            case MirDirection.DownRight:
                return MirDirection.UpLeft;
            case MirDirection.Down:
                return MirDirection.Up;
            case MirDirection.DownLeft:
                return MirDirection.UpRight;
            case MirDirection.Left:
                return MirDirection.Right;
            case MirDirection.UpLeft:
                return MirDirection.DownRight;
            default:
                return dir;
        }
    }
    public static ItemInfo GetRealItem(ItemInfo Origin, ushort Level, MirClass job, List<ItemInfo> ItemList)
    {
        #region Demand conversion
        RequiredClass requClass = RequiredClass.None;
        switch (job)
        {
            case MirClass.战士:
                requClass = RequiredClass.战士;
                break;
            case MirClass.法师:
                requClass = RequiredClass.法师;
                break;
            case MirClass.道士:
                requClass = RequiredClass.道士;
                break;
            case MirClass.刺客:
                requClass = RequiredClass.刺客;
                break;
            case MirClass.弓手:
                requClass = RequiredClass.弓手;
                break;
            case MirClass.碧血战士:
                requClass = RequiredClass.碧血战士;
                break;
            case MirClass.虹玄法师:
                requClass = RequiredClass.虹玄法师;
                break;
            case MirClass.翊仙道士:
                requClass = RequiredClass.翊仙道士;
                break;
            case MirClass.飞燕刺客:
                requClass = RequiredClass.飞燕刺客;
                break;
            case MirClass.暗鬼弓手:
                requClass = RequiredClass.暗鬼弓手;
                break;
        }
        #endregion


        if (Origin.ClassBased && Origin.LevelBased)
            return GetClassAndLevelBasedItem(Origin, requClass, Level, ItemList);
        if (Origin.ClassBased)
            return GetClassBasedItem(Origin, requClass, ItemList);
        if (Origin.LevelBased)
            return GetLevelBasedItem(Origin, Level, ItemList);
        return Origin;
    }
    public static ItemInfo GetLevelBasedItem(ItemInfo Origin, ushort level, List<ItemInfo> ItemList)//等级
    {
        ItemInfo output = Origin;
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemInfo info = ItemList[i];
            if (info.Name.StartsWith(Origin.Name))
                if ((info.RequiredType == RequiredType.Level) && (info.RequiredAmount <= level) && (output.RequiredAmount < info.RequiredAmount) && (Origin.RequiredGender == info.RequiredGender))
                    output = info;
        }
        return output;
    }
    public static ItemInfo GetClassBasedItem(ItemInfo Origin, RequiredClass requClass, List<ItemInfo> ItemList)//职业
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemInfo info = ItemList[i];
            if (info.Name.StartsWith(Origin.Name))
                //if (((ushort)info.RequiredClass == (1 << (ushort)job)) && (Origin.RequiredGender == info.RequiredGender))
                if (info.RequiredClass.HasFlag(requClass) && Origin.RequiredClass != info.RequiredClass && Origin.RequiredGender == info.RequiredGender)
                    return info;
        }
        return Origin;
    }

    public static ItemInfo GetClassAndLevelBasedItem(ItemInfo Origin, RequiredClass requClass, ushort level, List<ItemInfo> ItemList)//职业和等级
    {
        ItemInfo output = Origin;
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemInfo info = ItemList[i];
            if (info.Name.StartsWith(Origin.Name))
                if (info.RequiredClass.HasFlag(requClass) && Origin.RequiredClass != info.RequiredClass)
                    //if ((ushort)info.RequiredClass == (1 << (ushort)job))
                    if ((info.RequiredType == RequiredType.Level) && (info.RequiredAmount <= level) && (output.RequiredAmount <= info.RequiredAmount) && (Origin.RequiredGender == info.RequiredGender))
                        output = info;
        }
        return output;
    }

    public static string StringOverLines(string line, int maxWordsPerLine, int maxLettersPerLine)
    {
        string newString = string.Empty;

        string[] words = line.Split(' ');

        int lineLength = 0;

        for (int i = 0; i < words.Length; i++)
        {
            lineLength += words[i].Length + 1;

            newString += words[i] + " ";
            if (i > 0 && i % maxWordsPerLine == 0 && lineLength > maxLettersPerLine)
            {
                lineLength = 0;
                newString += "\r\n";
            }
        }

        return newString;
    }

    public static IEnumerable<byte[]> SplitArray(byte[] value, int bufferLength)
    {
        int countOfArray = value.Length / bufferLength;
        if (value.Length % bufferLength > 0)
            countOfArray++;
        for (int i = 0; i < countOfArray; i++)
        {
            yield return value.Skip(i * bufferLength).Take(bufferLength).ToArray();
        }
    }

    public static byte[] CombineArray(List<byte[]> arrays)
    {
        byte[] rv = new byte[arrays.Sum(x => x.Length)];
        int offset = 0;
        foreach (byte[] array in arrays)
        {
            System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
            offset += array.Length;
        }
        return rv;
    }
    public static byte[] SerializeToBytes<T>(T item)
    {
        var formatter = new BinaryFormatter();
        using (var stream = new MemoryStream())
        {
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            return stream.ToArray();
        }
    }
    public static object DeserializeFromBytes(byte[] bytes)
    {
        var formatter = new BinaryFormatter();
        using (var stream = new MemoryStream(bytes))
        {
            return formatter.Deserialize(stream);
        }
    }
}