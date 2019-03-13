using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public interface IFormService : IControl
    {
        string KeyFieldName { get; }

        int Insert(IDictionary<string, object> data);

        int Update(string key, IDictionary<string, object> data);

        IDictionary<string, object> Get(string key);

        int Delete(string key);
    }
}
