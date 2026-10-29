
namespace CA_Stack;

class Program
{
    static void Main(string[] args)
    {
        MyStack<int> s = new();

        s.Push(1);
        s.Push(2);
        s.Push(3);
        s.Push(4);
        s.Push(5);

        foreach(int i in s)
            Console.WriteLine(i);
    }
}