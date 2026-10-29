

using System.Collections;

public class MyQueue<T> : IEnumerable<T>
{
    // Circular Manner
    private int _front, _rear;
    private int _capacity;
    T[] _array;
    private int _size;

    public int Count => _size;

    public MyQueue(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be positive");

        _capacity = capacity;
        _front = 0;
        _size = 0;
        _rear = 0;
        _array = new T[_capacity];
    }

    public void Enqueue(T ele)
    {
        if (_size == _capacity)
        {
            T[] arr = new T[ 2 * _capacity];

            for (int i = 0; i < _size; i++)
            {
                arr[i] = _array[_front];
                _front = (_front + 1) % _capacity;
            }

            _array = arr;
            _capacity *= 2;
            _front = 0;
            _rear = _size;
        }

        _array[_rear] = ele;
        _rear = (_rear + 1) % _capacity;
        _size++;
    }

    public T DeQueue()
    {
        checkEmpty();
        T ret = _array[_front];
        _array[_front] = default!;
        _front = (_front + 1) % _capacity;
        _size--;
        return ret;
    }

    public T Peek()
    {
        checkEmpty();
        return _array[_front];
    }

    public void Clear()
    {
        Array.Clear(_array, 0, _array.Length);
        _front = 0;
        _rear = 0;
        _size = 0;
    }

    public bool Contains(T ele)
    {
        for (int i = 0; i < _size; i++)
        {
            int index = (_front + i) % _capacity;
            if (EqualityComparer<T>.Default.Equals(ele, _array[index]))
                return true;
        }
        return false;
    }

    private void checkEmpty()
    {
        if (_size == 0)
            throw new InvalidOperationException("Queue is empty");
    }

    public IEnumerator<T> GetEnumerator() => new Enumerator(_array, _front, _capacity);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private class Enumerator : IEnumerator<T>
    {

        private readonly T[] _array;
        private readonly int _capacity;
        private readonly int _front;
        private int _currentIndex = -1;

        public Enumerator(T[] array, int front, int size)
        {
            _array = array;
            _front = front;
            _capacity = size;
        }

        public T Current 
        {
            get
            {
                if (_currentIndex < 0)
                    throw new InvalidOperationException("The Enumeration does not started");
                if (_currentIndex >= _capacity)
                    throw new InvalidOperationException("The Enumeration has ended");
                return _array[(_front+_currentIndex)%_capacity];
            }
        }

        object? IEnumerator.Current => Current;

        public bool MoveNext() => ++_currentIndex<_capacity;

        public void Reset() => _currentIndex = -1;

        public void Dispose() { }

    }
}