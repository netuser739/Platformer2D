using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ListExecuteObjects : IEnumerator
    {
        private IExecute[] _executeObj;
        public int _index = -1;


        public ListExecuteObjects(IExecute obj)
        {
            _executeObj = new[] { obj };
        }

        public IExecute this[int curr]
        {
            get => _executeObj[curr];
            private set => _executeObj[curr] = value;
        }

        public object Current => _executeObj[_index];
        public int Lenght => _executeObj.Length;

        public void Add(IExecute obj)
        {
            Array.Resize(ref _executeObj, Lenght + 1);
            _executeObj[Lenght - 1] = obj;
        }

        public bool MoveNext()
        {
            if(_index >= Lenght - 1)
            {
                return false;
            }
            
            _index++;
            return true;
            
        }

        public void Reset()
        {
            _index = -1;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }
    }
}