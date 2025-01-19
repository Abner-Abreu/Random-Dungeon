namespace Player_Utils;

#region Enums
public enum playerType
{
    Warrior,
    Mage,
    Explorer,
    Cleric,
    Summoner,
    None
}
public enum playerHability
{
    WallDestroyer,
    Instinct,
    Swap,
    Ilumination,
    GoblinSummon
}
public enum moveDirection
{
    Up,
    Down,
    Rigth,
    Left
}
#endregion
public partial class Player
{
    public static Dictionary<playerType, playerHability> ClassHability = new Dictionary<playerType, playerHability>()
    {
        {playerType.Warrior, playerHability.WallDestroyer},
        {playerType.Explorer, playerHability.Instinct},
        {playerType.Mage, playerHability.Swap},
        {playerType.Cleric, playerHability.Ilumination},
        {playerType.Summoner, playerHability.GoblinSummon}
    };
    public static Dictionary<playerHability, string> HabilityDescription = new Dictionary<playerHability, string>()
    {
        {playerHability.WallDestroyer, "Destructor de Paredes: La ira te inunda y destruyes una pared cercana al azar"},
        {playerHability.Instinct, "Instinto: Tu instinto de explorador te advierte de la direccion del peligro"},
        {playerHability.Swap, "Intercambio: Intercambia tu posición con otro aventurero"},
        {playerHability.Ilumination, "Iluminación: Recibe la guía divina"},
        {playerHability.GoblinSummon, "Invocar Goblin: Invocas a un pequeño goblin que desactiva las trampas cercanas a ti"}
    };
}
