namespace PlayerUtils;

public class Player 
{
    private string _name;
    private string _playerClass;
    #region Stats
        int strength;
        int agility;
        int intelligence;
        int vitality;
    #endregion

    public Player(string name, string playerClass)
    {
        if (playerClass == null) throw new Exception("playerClass can't be a null expretion");
        if (name == null) throw new Exception("name can't be a null expretion");
        
        _name = name;
        switch (playerClass)
        {
            case "Warrior":
            {
                _playerClass = "Warrior";
                strength = 5;
                agility = 3;
                intelligence = 2;
                vitality = 4;
                break;
            }
            case "Mage":
            {
                _playerClass = "Mage";
                strength = 2;
                agility = 4;
                intelligence = 5;
                vitality = 3;
                break;
            }
            case "Explorer":
            {
                _playerClass = "Explorer";
                strength = 3;
                agility = 5;
                intelligence = 4;
                vitality = 2;
                break;
            }
        }
    }

    public bool ContainsVoid(string name)
    {
        foreach(char c in name)
        {
            if(c == ' ')
            {
                return true;
            }
        }
        return false;
    }
}
