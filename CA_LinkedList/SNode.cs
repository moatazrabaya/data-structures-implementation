namespace CA_LinkedList;

public class SNode<T>
{
    public T? Data { get; set; }
    public SNode<T>? Next { get; set; }

    public SNode()
    {
        Data = default;
    }

    public SNode(T data, SNode<T>? next = null)
    {
        Data = data;
        Next = next;
    }
}