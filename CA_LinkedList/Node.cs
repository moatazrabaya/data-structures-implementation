namespace CA_LinkedList;

public class Node<T>
{

    public T? Data { get; set; }
    public Node<T>? Next { get; set; }
    public Node<T>? Prev { get; set; }

    public Node(T data, Node<T>? next=null, Node<T>? prev=null)
    {
        Data = data;
        Next = next;
        Prev = prev;
    }
}