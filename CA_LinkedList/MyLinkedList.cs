using System.Collections;

namespace CA_LinkedList;

public class MyLinkedList<T> : IEnumerable<T>
{
    public Node<T>? First => _head;
    public Node<T>? Last => _tail;

    private Node<T>? _head;
    private Node<T>? _tail;
    private int _count;

    public int Count => _count;

    public MyLinkedList()
    {
        _head = _tail = null;
    }

    public MyLinkedList(IEnumerable<T> collection)
    {
        if(collection is null)
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
        Node<T> element = new(data);

        if (_head is null)
        {
            _head = _tail = element;
        }
        else
        {
            element.Next = _head;
            _head.Prev=element;
            _head = element;
        }
        _count++;
    }

    public void AddLast(T data)
    {
        Node<T> element = new(data);
        if (_head is null)
        {
            _head = _tail = element;
        }
        else
        {
            _tail!.Next=element;
            element.Prev=_tail;
            _tail = element;
        }
        _count++;
    }

    public void AddAfter(Node<T> node, T data)
    {
       if (node is null)
           throw new ArgumentNullException(nameof(node));

       EnsureNotEmpty();

       Node<T>? curr = _head;

       while (curr != null)
       {
           if (curr == node)
           {
                Node<T> element = new(data);

                element.Prev = curr;
                element.Next = curr.Next;

                curr.Next = element;

                if (element.Next != null)
                    element.Next.Prev = element;
                else
                    _tail = element;

                break;
           }

           curr = curr.Next;
        }

       if (curr is null)
           throw new InvalidOperationException($"There is no node similar to this node: {{ {node}: data = {node.Data} }}");
    }

    public void InsertAt(int index, T value)
    {
        if (index < 0 || index > _count)
            throw new ArgumentOutOfRangeException("Invalid index");

        if (index == 0)
            AddFirst(value);
        else if (index == _count)
            AddLast(value);
        else
        {
            Node<T> curr = _head!;

            while (index > 0)
            {
                curr = curr.Next!;
                index--;
            }

            Node<T> newNode = new(value);

            newNode.Next = curr;
            newNode.Prev = curr.Prev;
            curr.Prev!.Next = newNode;
            curr.Prev = newNode;
            _count++;
        }
        
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
            _head!.Prev=null;
        }
        _count--;
    }

    public void RemoveLast()
    {
        EnsureNotEmpty();

        if (_head == _tail)
        {
            _head = _tail = null;
        }         
        else
        {
            _tail = _tail!.Prev;
            _tail!.Next=null;
        }
        _count--;
    }

    public void Remove(T ele)
    {
        EnsureNotEmpty();

        Node<T>? curr = _head;

        while (curr != null)
        {
            
            if ((curr.Data is null && ele is null) || (curr.Data!=null && curr.Data.Equals(ele)))
            {
                if(curr == _tail)
                {
                    RemoveLast();
                }
                else if(curr == _head)
                {
                    RemoveFirst();
                }
                else
                {
                    curr.Prev!.Next = curr.Next;
                    curr.Next!.Prev = curr.Prev;  
                }

                return;
            }
            curr = curr.Next;
        }
    }

    public void RemoveAt(int index)
    {
        EnsureNotEmpty();

        if (index < 0 || index >= _count)
            throw new ArgumentOutOfRangeException("Invalid index");

        Node<T> curr = _head!;

        while (index > 0)
        {
            curr = curr.Next!;
            index--;
        }

        if (curr == _head)
            RemoveFirst();
        else if (curr == _tail)
            RemoveLast();
        else
        {
            curr.Prev!.Next = curr.Next;
            curr.Next!.Prev = curr.Prev;
        }
    }

    public Node<T>? Find(T data)
    {
        Node<T>? curr = _head;
        while (curr!=null)
        {
            if ((curr.Data is null && data is null) || (curr.Data != null && curr.Data.Equals(data)))
                break;
            curr = curr.Next;
        }
        return curr;
    }

    public Node<T>? FindLast(T data)
    {
        Node<T>? curr = _tail;
        while (curr!=null)
        {
            if ((curr.Data is null && data is null) || (curr.Data != null && curr.Data.Equals(data)))
                break;
            curr = curr.Prev;
        }
        return curr;
    }

    public bool Contains(T data)
    {
        return Find(data) != null;
    }

    public T[] ToArray()
    {
        T[] array = new T[_count];

        int i = 0;
        foreach (var item in this)
            array[i++] = item;

        return array;
    }

    public void RotateRight(int rotateBy)
    {   
        if (_head is null || rotateBy % _count == 0)
            return;

        rotateBy = rotateBy % _count;

        Node<T> newTail = _head;

        for (int i = 0; i < _count - rotateBy - 1; i++)
            newTail = newTail.Next!;

        Node<T> newHead = newTail.Next!;

        newTail.Next = null;
        newHead.Prev = null;

        _tail!.Next = _head;
        _head.Prev = _tail;

        _head = newHead;
        _tail = newTail;
    }

    public void RotateLeft(int rotateBy)
    {
        if (_head is null || rotateBy % _count == 0)
            return;
        rotateBy = rotateBy % _count;

        Node<T> newTail = _head;

        for (int i = 0; i < rotateBy - 1; i++)
            newTail = newTail.Next!;

        Node<T> newHead = newTail.Next!;

        newTail.Next = null;
        newHead.Prev = null;

        _tail!.Next = _head;
        _head.Prev = _tail;

        _head = newHead;
        _tail = newTail;

    }

    public Node<T> MiddleElement()
    {
        EnsureNotEmpty();

        Node<T> slow = _head!;
        Node<T>? fast = _head!.Next;

        while(fast!=null&&fast.Next!=null)
        {
            slow = slow.Next!;
            fast = fast.Next.Next;
        }

        return slow;
    }

    private void EnsureNotEmpty()
    {
        if (_head is null)
            throw new InvalidOperationException("The linked list is empty.");
    }

    public IEnumerator<T> GetEnumerator() => new Enumerator<T>(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    class Enumerator<E> : IEnumerator<E>
    {
        private Node<E>? _current;
        private MyLinkedList<E> _list;
        public Enumerator(MyLinkedList<E> list)
        {
            _list = list;
            _current = null;
        }

        public E Current
        {
            get
            {
                if (_current is null)
                    throw new InvalidOperationException("Enumeration is not at a valid element.");
                return _current!.Data!;
            }
        }

        object? IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            _current = _current == null ? _list._head : _current.Next;
            return _current != null;
        }

        public void Reset() => _current = null;
    }
}