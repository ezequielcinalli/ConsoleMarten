public static class DemoConsole
{
    public static void WriteWithColour(string value)
    {
        lock (Console.Out)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}