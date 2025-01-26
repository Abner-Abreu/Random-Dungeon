namespace Game_Board;
using Player_Utils;
public partial class GameBoard
{
    public bool RaceWinned(Player player)
    {   
        int centerIndex = (_maze.GetLength(0) - 1)/2;
        if (player.position.x >= centerIndex - 1 && player.position.x <= centerIndex + 1
        && player.position.y >= centerIndex - 1 && player.position.y <= centerIndex + 1)
        {
            return true;
        }
        else return false;
    }
}
