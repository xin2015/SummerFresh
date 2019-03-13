using System;
using System.Runtime.Serialization;

namespace SummerFresh.Data
{
    public class DaoException : Exception
    {
        internal DaoException()
        {

        }

        internal DaoException(string message) : base(message)
        {

        }

        internal DaoException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected DaoException(SerializationInfo info, StreamingContext context) : base(info,context)
        {

        }
    }

    /// <summary>
    /// 表示数据本应该返回0行或者一行但却返回了多行的异常
    /// </summary>
    public class DataReturnMultipleRowsException : DaoException
    {
        internal DataReturnMultipleRowsException()
        {

        }

        internal DataReturnMultipleRowsException(string message) : base(message)
        {

        }

        internal DataReturnMultipleRowsException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected DataReturnMultipleRowsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }        
    }
}