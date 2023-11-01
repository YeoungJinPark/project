using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvenManager
{
    public interface IDBManger
    {
        void Execute(string query);
        DataTable Select(string query);
        void InsertProduct(string[] values);
        void InsertStock(string[] values);
        void CloseCon();
    }
}
