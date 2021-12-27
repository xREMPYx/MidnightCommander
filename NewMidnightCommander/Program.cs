using NewMidnightCommander;

Console.SetWindowSize(119,32);
Application.window = new DefaultWindow();
Console.CursorVisible = false;

while (true)
{
    Application.Print();
    ConsoleKeyInfo info = Console.ReadKey();
    Functions.ClearTextAlert();
    Application.HandleKey(info);
}