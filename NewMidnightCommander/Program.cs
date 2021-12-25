using NewMidnightCommander;

Application.window = new DefaultWindow();
Console.CursorVisible = false;

while (true)
{
    Application.Print();
    ConsoleKeyInfo info = Console.ReadKey();
    Application.HandleKey(info);
}
