using System;
using System.Linq;
using System.Text;

namespace SummerFresh.Environment
{
    public interface ISessionState : IDisposable
    {
        object this[string name] { get; set; }

        bool Remove(string name);
    }
}