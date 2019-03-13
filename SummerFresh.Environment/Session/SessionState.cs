using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace SummerFresh.Environment
{
    [Serializable]
    public class SessionState : ISessionState
    {
        private IDictionary<string, object> _items   = new Dictionary<string, object>();
        
        public object this[string name]
        {
            get
            {
                lock (this)
                {
                    object value;
                    return _items.TryGetValue(name, out value) ? value : null;
                }
            }
            set
            {
                lock (this)
                {
                    _items[name] = value;
                }
            }
        }

        public bool Remove(string name)
        {
            lock (this)
            {
                return _items.Remove(name);
            }
        }

        public void Dispose()
        {
            lock (this)
            {
                _items.Clear();
            }
        }
    }
}