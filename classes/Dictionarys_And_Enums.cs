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
public enum playerHability
{   
    WallDestroyer,
    Instinct,
    Swap
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

    public static Dictionary<playerType, playerHability> ClassHability = new Dictionary<playerType, playerHability>()
    {
        {playerType.Warrior, playerHability.WallDestroyer},
        {playerType.Explorer, playerHability.Instinct},
        {playerType.Mage, playerHability.Swap}
    };
    public static Dictionary<playerHability, string> HabilityDescription = new Dictionary<playerHability, string>()
    {
        {playerHability.WallDestroyer, "Destructor de Paredes: La ira te inunda y destruyes una pared cercana al azar"},
        {playerHability.Instinct, "Instinto: Tu instinto de explorador te advierte de la direccion del peligro"},
        {playerHability.Swap, "Intercambio: Intercambia tu posición con otro aventurero"}
    };
    public static Dictionary<moveDirection,Pair> Directions = new Dictionary<moveDirection, Pair>()
    {
        {moveDirection.Up, new Pair(0,-1)},
        {moveDirection.Down, new Pair(0,1)},
        {moveDirection.Rigth, new Pair(1,0)},
        {moveDirection.Left, new Pair(-1,0)}
    };
}
