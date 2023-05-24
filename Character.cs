namespace Adventure_Console;

public class Character
{
    public string Username { get; set; }
    public string Race { get; set; }
    
    public Character(string username, string race)
    {
        Username = username;
        Race = race;
    }
}
