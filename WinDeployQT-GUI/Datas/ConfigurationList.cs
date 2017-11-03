using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDeployQT_GUI.Datas
{
    public class List<T> : IList<Configuration>
    {
        public Configuration this[int index] { get => new Configuration(index); set => new NotImplementedException(); }

        public int Count => this.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Configuration item)
        {
            this.Add(item);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Configuration item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Configuration[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Configuration> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(Configuration item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Configuration item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Configuration item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        
        
    }
}

