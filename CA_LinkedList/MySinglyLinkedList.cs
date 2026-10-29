using System.Collections;

namespace CA_LinkedList;

public class MySinglyLinkedList<T> : IEnumerable<T>
{
    public SNode<T>? Head => _head;
    public SNode<T>? Tail => _tail;

    private SNode<T>? _head;
    private SNode<T>? _tail;
    private int _count;

    public int Count => _count;

    public MySinglyLinkedList()
    {
        _head = _tail = null;
    }

    public MySinglyLinkedList(IEnumerable<T> collection)
    {
        if (collection is null)
            throw new ArgumentNullException(nameof(collection));

        foreach (T item in collection)
        {
            AddLast(item);
        }
    }

    public void Clear()
    {
        _head = _tail = null;
        _count = 0;
    }

    public void AddFirst(T data)
    {
        SNode<T> element = new(data);

        if (_head is null)
        {
            _head = _tail = element;
        }
        else
        {
            element.Next = _head;
            _head = element;
        }
        _count++;
    }

    public void AddLast(T data)
    {
        SNode<T> element = new(data);
        if (_head is null)
        {
            _head = _tail = element;
        }
        else
        {
            _tail!.Next = element;
            _tail = element;
        }
        _count++;
    }
   
    public void RemoveFirst()
    {
        EnsureNotEmpty();

        if (_head == _tail)
        {
            _head = _tail = null;
        }
        else
        {
            _head = _head!.Next;
        }
        _count--;
    }

    public void Remove(T ele)
    {
        EnsureNotEmpty();

        SNode<T>? first = _head;
        SNode<T>? second = _head!.Next;

        if(EqualityComparer<T>.Default.Equals(ele, first!.Data))
        {
            RemoveFirst();
            return;
        }

        while (first!=null&&second != null)
        {
            if (EqualityComparer<T>.Default.Equals(ele, second.Data))
            {
                first.Next = second.Next;
                _count--;
                if (second == _tail)
                    _tail = first;
                return;
            }
            first = first.Next;
            second = second.Next;
        }
    }

    public SNode<T>? Find(T data)
    {
        SNode<T>? curr = _head;
        while (!(curr is null))
        {
            if (EqualityComparer<T>.Default.Equals(data, curr.Data))
                return curr;
            curr = curr.Next;
        }
        return null;
    }

    public bool Contains(T data)
    {
        return Find(data) != null;
    }

    public SNode<T> MiddleElement()
    {
        EnsureNotEmpty();

        SNode<T> slow = _head!;
        SNode<T>? fast = _head!.Next;

        while (fast != null && fast.Next != null)
        {
            slow = slow.Next!;
            fast = fast.Next.Next;
        }

        return slow;
    }

    public void Reverse()
    {

        if (_head is null)
            return;

        SNode<T>? first = _head;
        SNode<T>? second = _head.Next;
        SNode<T>? temp;

        _tail = _head;
        _head.Next = null;

        while (second != null)
        {
            temp = second.Next;
            second.Next = first;
            first = second;
            second = temp;
        }
        _head = first;
    }

    public SNode<T> Reversed()
    {
        EnsureNotEmpty();

        return ReverseHelper(_head!, null);
    }

    private SNode<T> ReverseHelper(SNode<T>? curr, SNode<T>? head)
    {
        if (curr is null)
            return head!;

        SNode<T> newCurr = new(curr.Data!);

        newCurr.Next = head;

        return ReverseHelper(curr.Next, newCurr);
    }

    private void EnsureNotEmpty()
    {
        if (_head is null)
            throw new InvalidOperationException("The linked list is empty.");
    }

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    class Enumerator : IEnumerator<T>
    {
        private SNode<T>? currentNode = null;
        MySinglyLinkedList<T> list;

        public Enumerator(MySinglyLinkedList<T> list)
        {
            this.list = list;
            currentNode = list.Head;
        }

        public T Current
        {
            get
            {
                if (currentNode is null)
                    throw new InvalidOperationException();
                return currentNode!.Data!;
            }
        }

        object? IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            if (currentNode == null)
                currentNode = list.Head;
            else
                currentNode = currentNode.Next;
            return currentNode != null;
        }

        public void Reset() => currentNode = null;
    }
}