using System.Collections;

namespace CA_List;

public class MyList<T> : IEnumerable<T>
{
    private const int defaultCapacity = 10;
    private T[] _array;
    private int _size = 0;

    public int Count => _size;

    public MyList(int capacity)
    {
        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));

        _array = new T[capacity];
    }

    public MyList()
    {
        _array = new T[defaultCapacity];
    }

    public T this[int index]
    {
        get
        {
            checkRange(index);
            return _array[index];
        }
        set
        {
            checkRange(index);
            _array[index] = value;
        }
    }

    private void checkRange(int index)
    {
        if (index < 0 || index >= _size)
            throw new ArgumentOutOfRangeException(nameof(index));
    }

    private void EnsureCapacity()
    {
        if (_size == _array.Length)
        {
            int newCapacity = (_array.Length == 0) ? defaultCapacity : (2 * _array.Length);
            T[] temp = new T[newCapacity];
            Array.Copy(_array, temp, _size);
            _array = temp;
        }
    }
    
    public void Add(T element)
    {
        EnsureCapacity();

        _array[_size] = element;
        _size++;
    }

    public void Insert(int index, T element)
    {
        if (index < 0 || index > _size)
            throw new ArgumentOutOfRangeException(nameof(index));

        EnsureCapacity();
        Array.Copy(_array, index, _array, index + 1, _size - index);
        _array[index] = element;
        _size++;
    }

    public T RemoveAt(int index)
    {  
        if(_size==0)
            throw new InvalidOperationException("The list has no elements");

        checkRange(index);

        T temp = _array[index];

        Array.Copy(_array, index+1, _array, index, _size - index - 1);

        _size--;
        _array[_size] = default;
        return temp;
    }

    public bool Remove(T value)
    {
        int index = IndexOf(value);
        if (index == -1)
            return false;

        RemoveAt(index);
        return true;
    }

    public void Clear()
    {
        Array.Clear(_array, 0, _size);
        _size = 0;
    }

    public int IndexOf(T element)
    {
        for (int i = 0; i < _size; i++)
        {
            if (EqualityComparer<T>.Default.Equals(_array[i], element))
                return i;
        }
        return -1;
    }

    public bool Contains(T element)
    {
        for (int i = 0; i < _size; i++)
        {
            if (EqualityComparer<T>.Default.Equals(_array[i], element))
                return true;
        }
        return false;
    }

    public int LastIndexOf(T element)
    {
        for (int i = _size - 1; i >= 0; i--)
        {
            if (EqualityComparer<T>.Default.Equals(_array[i], element))
                return i;
        }

        return -1;
    }

    public void TrimExcess()
    {
        T[] temp = new T[_size];
        Array.Copy(_array, 0, temp, 0, _size);
        _array = temp;
    }

    public IEnumerator<T> GetEnumerator() => new Enumerator(_array, _size);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private class Enumerator : IEnumerator<T>
    {
        private readonly T[] _array;
        private int _index = -1;
        private readonly int _size;

        public Enumerator(T[] array, int size)
        {
            _array = array;
            _size = size;
        }

        public T Current
        {
            get
            {
                if (_index == -1)
                    throw new InvalidOperationException("Enumeration not started");
                if(_index >= _size)
                    throw new InvalidOperationException("Enumeration has ended");
                return _array[_index];
            }  
        }
           
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            return ++_index < _size;
        }

        public void Reset() => _index = -1;

        public void Dispose() { }
    }
       
}