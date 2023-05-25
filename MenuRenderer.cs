namespace Adventure_Console
{
    internal class MenuRenderer
    {
        public void RenderMenuOptions(string[] options, int selectedOption)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, Console.CursorTop);

            for (int i = 0; i < options.Length; i++)
            {
                Console.ResetColor();
                if (i == selectedOption - 1)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(options[i]);
                Console.ResetColor();
            }
        }
    }
}