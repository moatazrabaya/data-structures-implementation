using System.Collections;

namespace CA_Stack;

public class MyStack<T> : IEnumerable<T>
{

    private const int capacity = 10;
    private T[] _array;
    private int _size;

    public int Count => _size;

    public MyStack()
    {
        _array = new T[capacity];
    }

    private void EnsureCapacity()
    {
        if (_size == _array.Length)
        {
            T[] temp = new T[_array.Length * 2];
            Array.Copy(_array, temp, _size);
            _array = temp;
        }
    }

    public void Push(T ele)
    {
        EnsureCapacity();
        _array[_size] = ele;
        _size++;
    }

    public T Pop()
    {
        checkEmpty();
        T ele = _array[_size - 1];
        _array[_size - 1] = default!;
        _size--;
        return ele;
    }

    public T Peek()
    {
        checkEmpty();
        return _array[_size - 1];
    }

    public void Clear()
    {
        Array.Clear(_array, 0, _size);
        _size = 0;
    }

    public bool Contains(T ele)
    {
        for(int i=0; i < _size; i++)
        {
            if (EqualityComparer<T>.Default.Equals(_array[i], ele))
                return true;
        }
        return false;
    }

    private void checkEmpty()
    {
        if (_size == 0)
            throw new InvalidOperationException("There is no elements in the stack");
    }

    public IEnumerator<T> GetEnumerator() => new Enumerator(_array, _size);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private class Enumerator : IEnumerator<T>
    {

        private readonly T[] _array;
        private readonly int _size;
        private int _currentIndex;

        public Enumerator(T[] array, int size)
        {
            _array = array;
            _size = size;
            _currentIndex = _size;
        }

        public T Current
        {
            get
            {
                if (_currentIndex >= _size)
                    throw new InvalidOperationException("Enumeration not started");
                if (_currentIndex < 0)
                    throw new InvalidOperationException("Enumeration has ended");
                return _array[_currentIndex];
            }
        }

        object IEnumerator.Current => Current!;

        public bool MoveNext() => --_currentIndex >= 0;

        public void Reset() => _currentIndex = _size;

        public void Dispose() { }

    }
}