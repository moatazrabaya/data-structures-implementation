class Program
{
    static void Main(string[] args)
    {
        MyQueue<int> queue = new(3);

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.DeQueue();
        queue.Enqueue(3);

        foreach (int i in queue)
            Console.WriteLine(i);

    }
}