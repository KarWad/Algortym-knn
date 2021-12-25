// For Directory.GetFiles and Directory.GetDirectories
// For File.Exists, Directory.Exists
using System;

public class IndexAndDistance : IComparable<IndexAndDistance>
{

    public int idx;
    public int dist;
    public int CompareTo(IndexAndDistance other)
    {
        if (this.dist < other.dist) return -1;
        else if (this.dist > other.dist) return +1;
        else return -0;
    }
}