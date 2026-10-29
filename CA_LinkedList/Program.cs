

using System.Collections.Generic;
using System.Xml.Linq;

namespace CA_LinkedList;

class Program
{
    static void Main(string[] args)
    {

        MyLinkedList<string> list = new();

        list.AddLast("A");
        list.AddLast("B");
        list.AddLast("C");

        //Node<string> node = new(null!);

        list.AddLast("D");
        

        foreach(var item in list)
            Console.WriteLine(item);

        list.RotateRight(2);

        foreach (var item in list)
            Console.WriteLine(item);

    }
}
