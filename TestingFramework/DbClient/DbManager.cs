using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFramework.DbClient
{
    class DbManager
    {
        private static readonly Lazy<UsersContext> _userLazy = new(() => new UsersContext());

        public static UsersContext Users { get { return _userLazy.Value; } }

        static DbManager()
        {

        }
    }
}
