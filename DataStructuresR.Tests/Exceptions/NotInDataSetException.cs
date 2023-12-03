using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresR.Tests.Exceptions
{
    internal class NotInDataSetException<T> : Exception
    {

        public static NotInDataSetException<T> GetException(T? value)
        {
            string message = string.Format("{0} not expected in the data set", value);

            return new NotInDataSetException<T>(message);
        }

        private NotInDataSetException(string message) : base(message) 
        { }
    }
}
