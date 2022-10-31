using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinDAL
{
    public static class DALFactory
    {
        public static T CreateDAL<T>() where T : DAL, new()
        {
            return new T();
        }
    }
}
