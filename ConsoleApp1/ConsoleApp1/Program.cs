// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

int countdown = 10;

for (int i = 0; i < countdown; i++)
{
    Console.WriteLine(i);
    countdown = countdown - i;
    Thread.Sleep(10000);
}