namespace Others;

public class Pair
{
    public int first;
    public int second;

    public Pair(int a, int b)
    {
        first = a;
        second = b;
    }

    public static Pair Add(Pair a, Pair b)
    {
        return new Pair(a.first + b.first, a.second + b.second);
    }
    public static Pair operator + (Pair a, Pair b)
    {
        return Add(a,b);
    }
}
