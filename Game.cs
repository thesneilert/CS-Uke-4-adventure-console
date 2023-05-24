namespace Adventure_Console;

internal enum GameMenu
{
    ExitGame,
    MainMenu,
    CreateCharacter,
    ConfirmCharacterCreation,
    LoadSaveGame,
    GameMenu,
    MineStone,
    Shop,
    IntroScreen
}

public enum Race
{
    Human,
    Gnome,
    Troll
}

internal class Game
{
    private GameMenu _currentMenu;
    private readonly List<Character> _characters = new();

    public Game()
    {
        _currentMenu = GameMenu.MainMenu;
    }

    public void Run()
    {
        while (_currentMenu != GameMenu.ExitGame)
            switch (_currentMenu)
            {
                case GameMenu.MainMenu:
                    ShowMainMenu();
                    break;
                case GameMenu.CreateCharacter:
                    ShowCreateCharacter();
                    break;
                case GameMenu.ConfirmCharacterCreation:
                    ConfirmCharacterCreation();
                    break;
                case GameMenu.LoadSaveGame:
                    //ShowLoadSaveGame();
                    break;
                case GameMenu.GameMenu:
                    ShowGameMenu();
                    break;
                case GameMenu.MineStone:
                    //MineStone();
                    break;
                case GameMenu.Shop:
                    //VisitShop();
                    break;
                case GameMenu.IntroScreen:
                    IntroScreen();
                    break;
            }
    }

    private void ShowMainMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(Ascii.Mountains);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(Ascii.AdventureConsoleTittleText);
        Console.WriteLine(Ascii.ByTheSneilertTextandPerson);
        Console.ResetColor();
        Console.WriteLine("1. Create a new character");
        Console.WriteLine("2. Load save game (not working)");
        Console.WriteLine("3. Exit game");
        Console.Write("\nEnter your choice: ");

        var choice = GetChoice(3);
        switch (choice)
        {
            case 1:
                _currentMenu = GameMenu.CreateCharacter;
                break;
            case 2:
                _currentMenu = GameMenu.LoadSaveGame;
                break;
            case 3:
                _currentMenu = GameMenu.ExitGame;
                break;
        }
    }

    private void ShowCreateCharacter()
    {
        //handling clean and title
        Console.Clear();
        _currentMenu = GameMenu.GameMenu;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(Ascii.CreateCharacterText);
        Console.ResetColor();

        //name
        Console.Write("\n\nEnter your name:");
        var username = Console.ReadLine();

        //race
        Console.WriteLine("\nChoose your race:");
        Console.WriteLine("1. Human");
        Console.WriteLine("2. Gnome");
        Console.WriteLine("3. Troll");
        Console.Write("\nEnter your choice:");
        var raceChoice = GetChoice(3);
        var race = (Race)(raceChoice - 1);

        //add user to list
        if (username != null)
        {
            var newCharacter = new Character(username, race.ToString());
            _characters.Add(newCharacter);
        }

        //clear og confirm
        Console.Clear();
        ConfirmCharacterCreation();
    }

    private void ConfirmCharacterCreation()
    {
        Console.Clear();
        _currentMenu = GameMenu.ConfirmCharacterCreation;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(Ascii.CreateCharacterText);
        Console.ResetColor();

        Console.WriteLine($"\n\nAre you a {_characters[0].Race} named {_characters[0].Username}?");
        Console.Write("\nYes or No:");
        var choice = Console.ReadLine();

        if (choice?.ToLower() == "yes")
            IntroScreen();
        else if (choice.ToLower() == "no")
        {
            _characters[0].Race = string.Empty;
            _characters[0].Username = string.Empty;
            ShowCreateCharacter();
        }
        else
        {
            ConfirmCharacterCreation();
        }
    }
    
    private void IntroScreen()
    {
        Console.Clear();
        _currentMenu = GameMenu.IntroScreen;

        string text = File.ReadAllText("text/IntroScreen.txt");
        string username = _characters[0].Username;
        text = text.Replace("{username}", username);
        Console.WriteLine(text);

        Console.Write("\n\nPress any key to continue...");
        Console.ReadLine();
        ShowGameMenu();
    }
    
    public void ShowGameMenu()
    {
        Console.Clear();
        _currentMenu = GameMenu.GameMenu;
        Console.WriteLine("heihei");
        Console.ReadKey();
    }


    /*--------------------------------------------------------------------------------------------------------------
                           chatGPT MAGIC for å ikke lage ny linje ved feil typing*/
    private int GetChoice(int maxChoice)
    {
        int choice;
        var topCursorPosition = Console.CursorTop;
        ConsoleKeyInfo keyInfo;

        do
        {
            keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine(); // Move to the next line
                Console.SetCursorPosition(0, topCursorPosition); // Move the cursor back to the top
                Console.Write("Invalid choice. Please try again: "); // Overwrite the line
            }
            else if (int.TryParse(keyInfo.KeyChar.ToString(), out choice) && choice >= 1 && choice <= maxChoice)
            {
                Console.WriteLine(); // Move to the next line
                return choice;
            }
        } while (true);
    }
    /*--------------------------------------------------------------------------------------------------------------*/
}