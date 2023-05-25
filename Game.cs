namespace Adventure_Console;

internal enum GameMenu
{
    ExitGame,
    MainMenu,
    CreateCharacter,
    ConfirmCharacterCreation,
    GameMenu,
    MineStone,
    Shop,
    IntroScreen
}

internal class Game
{
    private GameMenu _currentMenu;
    private readonly List<Character> _characters = new();
    private int _selectedOption;
    private readonly MenuRenderer _menuRenderer;

    public Game()
    {
        _currentMenu = GameMenu.MainMenu;
        _selectedOption = 1;
        _menuRenderer = new MenuRenderer();
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
                case GameMenu.GameMenu:
                    ShowGameMenu();
                    break;
                case GameMenu.IntroScreen:
                    ShowIntroScreen();
                    break;
            }
    }

    public void ShowMainMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(Ascii.Mountains);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(Ascii.AdventureConsoleTittleText);
        Console.WriteLine(Ascii.ByTheSneilertTextandPerson);
        Console.ResetColor();
        RenderMainMenu();

        var keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                if (_selectedOption > 1)
                    _selectedOption--;
                break;
            case ConsoleKey.DownArrow:
                if (_selectedOption < 2)
                    _selectedOption++;
                break;
            case ConsoleKey.Enter:
                switch (_selectedOption)
                {
                    case 1:
                        _currentMenu = GameMenu.CreateCharacter;
                        break;
                    case 2:
                        _currentMenu = GameMenu.ExitGame;
                        break;
                }

                break;
        }
    }

    private void RenderMainMenu()
    {
        string[] options =
        {
            "Create a new character",
            "Exit game"
        };

        _menuRenderer.RenderMenuOptions(options, _selectedOption);
    }

    private void ShowCreateCharacter()
    {
        Console.Clear();
        _currentMenu = GameMenu.ConfirmCharacterCreation;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(Ascii.CreateCharacterText);
        Console.ResetColor();

        Console.Write("\n\nEnter your name: ");
        var username = Console.ReadLine();

        if (username != null)
        {
            var newCharacter = new Character(username);
            _characters.Add(newCharacter);
        }

        Console.WriteLine($"\nAre you sure you have entered your name correctly as {_characters[0].Username}?\n");
        RenderCreateCharacterMenu();

        var keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                if (_selectedOption > 1)
                    _selectedOption--;
                break;
            case ConsoleKey.DownArrow:
                if (_selectedOption < 2)
                    _selectedOption++;
                break;
            case ConsoleKey.Enter:
                switch (_selectedOption)
                {
                    case 1:
                        _currentMenu = GameMenu.IntroScreen;
                        break;
                    case 2:
                        _characters.RemoveAt(0);
                        _currentMenu = GameMenu.CreateCharacter;
                        break;
                }
                break;
        }
    }


    private void RenderCreateCharacterMenu()
    {
        string[] options =
        {
            "Yes",
            "No"
        };

        _menuRenderer.RenderMenuOptions(options, _selectedOption);
    }
    

    private void ShowIntroScreen()
    {
        Console.Clear();
        _currentMenu = GameMenu.IntroScreen;

        Console.Write(Text.IntroText());

        Console.Write("\n\nPress any key to continue...");
        Console.ReadLine();
        ShowGameMenu();
    }

    public void ShowGameMenu()
    {
        Console.Clear();
        _currentMenu = GameMenu.GameMenu;
        Console.WriteLine("Game Menu");
        Console.ReadKey();
    }
}