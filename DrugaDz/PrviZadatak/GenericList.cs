using System;
using System.Collections;
using System.Collections.Generic;

namespace Models
{
    public interface IGenericList<X> : IEnumerable<X>
    {
        /// <summary >
        /// Adds an item to the collection .
        /// </ summary >
        void Add(X item);
        /// <summary >
        /// Removes the first occurrence of an item from the collection .
        /// If the item was not found , method does nothing .
        /// </ summary >
        bool Remove(X item);
        /// <summary >
        /// Removes the item at the given index in the collection .
        /// </ summary >
        bool RemoveAt(int index);
        /// <summary >
        /// Returns the item at the given index in the collection .
        /// </ summary >
        X GetElement(int index);
        /// <summary >
        /// Returns the index of the item in the collection .
        /// If item is not found in the collection , method returns -1.
        /// </ summary >
        int IndexOf(X item);
        /// <summary >
        /// /// Readonly property . Gets the number of items contained in the collection.
        /// </ summary >
        int Count { get; }
        /// <summary >
        /// Removes all items from the collection .
        /// </ summary >
        void Clear();
        /// <summary >
        /// Determines whether the collection contains a specific value .
        /// </ summary >
        bool Contains(X item);
    }

    public class GenericList<X> : IGenericList<X>
    {
        private X[] _internalStorage;
        private int _length = 0;

        public GenericList()
        {
            _internalStorage = new X[4];
        }

        override public string ToString()
        {
            return string.Join(",", _internalStorage);
        }

        public GenericList(int initialSize)
        {
            if (initialSize < 0)
            {
                throw new ArgumentOutOfRangeException("ArgumentOutOfRange_NeedNonNegNum");
            }
            _internalStorage = new X[initialSize];
        }

        public IEnumerator<X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(X item)
        {
            if (_length >= _internalStorage.Length)
            {
                Array.Resize(ref _internalStorage, _internalStorage.Length * 2);
            }

            _internalStorage[_length] = item;
            _length++;
        }

        public bool RemoveAt(int index)
        {
            if (index >= _length)
            {
                return false;
            }
            else
            {
                for (int i = index; i < _internalStorage.Length - 1; i++)
                {
                    _internalStorage[i] = _internalStorage[i + 1];
                }
                --_length;

                return true;
            }
        }

        public bool Remove(X item)
        {
            int pozicija = IndexOf(item);
            if (pozicija == -1)
            {
                return false;
            }
            return RemoveAt(pozicija);
        }

        public X GetElement(int index)
        {
            if (index >= _length)
            {
                throw new IndexOutOfRangeException("Index out of a range");
            }

            return _internalStorage[index];
        }

        public int Count
        {
            get
            {
                return _length;
            }
        }

        public void Clear()
        {
            _internalStorage = new X[_internalStorage.Length];
            _length = 0;
        }

        public bool Contains(X item)
        {
            for (int i = 0; i < _length; i++)
            {
                if (_internalStorage[i].Equals(item))
                    return true;
            }
            return false;
        }

        public int IndexOf(X item)
        {
            for (int i = 0; i < _length; i++)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }


        public class GenericListEnumerator<X> : IEnumerator<X>
        {
            private X[] _internalStorage;
            int position = -1;

            public GenericListEnumerator(GenericList<X> genericlist)
            {
                _internalStorage = genericlist._internalStorage;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _internalStorage.Length);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public X Current
            {
                get
                {
                    try
                    {
                        return _internalStorage[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public void Dispose()
            {
            }

        }
    }
}
