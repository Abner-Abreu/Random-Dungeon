namespace Utils;
using Pair_Type;
public enum playerStat
{
    Strength,
    Agility,
    Itelligence
}
public enum playerType
{
    Warrior,
    Mage,
    Explorer,
    None
}
public enum moveDirection
    {
        Up,
        Down,
        Rigth,
        Left
    }
    

public class Utils
{   
    public static Dictionary<playerStat, int> WarriorStats = new Dictionary<playerStat, int>()
    {
        {playerStat.Strength, 5},
        {playerStat.Agility, 3},
        {playerStat.Itelligence, 2},
    };
    public static Dictionary<playerStat, int> MageStats = new Dictionary<playerStat, int>()
    {
        {playerStat.Strength, 2},
        {playerStat.Agility, 3},
        {playerStat.Itelligence, 5},
    };

    public static Dictionary<playerStat, int> ExplorerStats = new Dictionary<playerStat, int>()
    {
        {playerStat.Strength, 2},
        {playerStat.Agility, 5},
        {playerStat.Itelligence, 3},
    };

    public static Dictionary<playerType, Dictionary<playerStat, int>> ClassStats = new Dictionary<playerType, Dictionary<playerStat, int>>()
    {
        {playerType.Warrior,WarriorStats},
        {playerType.Mage,MageStats},
        {playerType.Explorer,ExplorerStats},
    };

    public static Dictionary<moveDirection,Pair> Directions = new Dictionary<moveDirection, Pair>()
    {
        {moveDirection.Up, new Pair(0,1)},
        {moveDirection.Down, new Pair(0,-1)},
        {moveDirection.Rigth, new Pair(1,0)},
        {moveDirection.Left, new Pair(-1,0)}
    };
    Pair[] directionChecker = { 
        Utils.Directions[moveDirection.Up] * 2,
        Utils.Directions[moveDirection.Down] * 2,
        Utils.Directions[moveDirection.Rigth] *2,
        Utils.Directions[moveDirection.Left] * 2
    };
}
