using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class MyDynamicArray<T>
        where T : IEquatable<T>
    {
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                _items[index] = value;
            }
        }

        // 동적 배열의 용량(현재 참조하고 있는 배열의 길이)
        public int Capacity => _items.Length;

        // 실제 가지고 있는 자료의 개수
        public int Count => _count;

        private const int DEFAULT_SIZE = 1;
        private T[] _items = new T[DEFAULT_SIZE]; // 동적 배열의 배열 기반이기 때문에 아이템을 저장할 배열이 필요함.
        private int _count;

        // 매치 조건 탐색
        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match.Invoke(_items[i]))
                    return _items[i];
            }
            return default;
        }

        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match.Invoke(_items[i]))
                    return i;
            }
            return -1;
        }

        // 삽입
        public void Add(T item)
        {
            if (_count >= _items.Length)
            {
                T[] tmp = new T[_count * 2];
                Array.Copy(_items, tmp, _count);
                _items = tmp; // 배열 참조 변경
            }
            _items[_count] = item; // 마지막에 아이템 추가
        }

        // 삭제
        public void RemoveAt(int index)
        {
            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--; // 하나 뺐으니까 차감
        }

        public bool Remove(T item)
        {
            int index = FindIndex(x => x.Equals(item));
            if (index < 0)
                return false;
            RemoveAt(index);
            return true;
        }
    }
}