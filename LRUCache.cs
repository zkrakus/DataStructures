using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    class LRUCache
    {
        class DLLNode
        {
            public int Key { get; set; }
            public int Value { get; set; }
            public DLLNode Next { get; set; } = null;
            public DLLNode Prev { get; set; } = null;

            public DLLNode(int key, int value)
            {
                this.Value = value;
                this.Key = key;
            }
        }

        DLLNode head = null;
        DLLNode tail = null;
        readonly Dictionary<int, DLLNode> cache;
        readonly int size = 0;

        public LRUCache(int size)
        {
            this.size = size;
            cache = new Dictionary<int, DLLNode>(size);
        }

        public int Get(int key)
        {
            if (cache.TryGetValue(key, out DLLNode node))
            {
                RemoveFromDLL(node);
                AddAtDLLHead(node);
                return node.Value;
            }
            else
                return -1;
        }

        public void Put(int key, int value)
        {
            if (cache.TryGetValue(key, out DLLNode node))
            {
                RemoveFromDLL(node);
                AddAtDLLHead(node);
                node.Value = value;
            }
            else if (cache.Count < size)
            {
                DLLNode newNode = new DLLNode(key, value);
                AddAtDLLHead(newNode);
                cache.Add(key, newNode);
            }
            else if (size <= cache.Count)
            {
                DLLNode oldTail = PopDLLTail();
                cache.Remove(oldTail.Key);

                DLLNode newNode = new DLLNode(key, value);
                AddAtDLLHead(newNode);
                cache.Add(key, newNode);
            }
        }

        private void AddAtDLLHead(DLLNode node)
        {
            if (head == null)
            {
                head = node;
                tail = node;
            }
            else
            {
                head.Prev = node;
                node.Next = head;
                head = node;
            }
        }

        private DLLNode PopDLLTail()
        {
            if (tail == null)
                return null;
            else if (head == tail)
            {
                var tmp = head;
                head = null;
                tail = null;
                return tmp;
            }
            else
            {
                var oldTail = tail;
                tail.Prev.Next = null;
                tail = tail.Prev;

                return oldTail;
            }
        }

        private void RemoveFromDLL(DLLNode node)
        {
            if (node == head && node == tail)
            {
                head = null;
                tail = null;
            }
            else if (node == head)
            {
                if (head.Next != null)
                {
                    head.Next.Prev = null;
                    head = head.Next;
                }
                else
                    head = null;
            }
            else if (node == tail)
            {
                tail.Prev.Next = null;
                tail = tail.Prev;
            }
            else
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }
        }

    }
}
