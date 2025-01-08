namespace Pair_Type;

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

    public static Pair MultiplyByInteger(Pair a, int b)
    {
        return new Pair(a.first * b, a.second * b);
    }
    public static Pair operator * (Pair a, int b)
    { 
        return MultiplyByInteger(a,b); 
    }
}
