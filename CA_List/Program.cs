namespace CA_List;

class Program
{
    static void Main(string[] args)
    {
        MyList<int> list = new(3);

        list.Add(1);
        list.Add(2);
        list.Add(3);

        foreach(int i in list)
            Console.WriteLine(i);
    }
}