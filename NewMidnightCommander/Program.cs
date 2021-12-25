using NewMidnightCommander;

Application.window = new DefaultWindow();

while (true)
{
    Application.Print();
    ConsoleKeyInfo info = Console.ReadKey();
    Application.HandleKey(info);
}
