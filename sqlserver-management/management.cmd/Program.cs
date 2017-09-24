using sqlserver.management;
using System;

namespace management.cmd
{
    class Program
    {
        static void Main(string[] args)
        {


            var result = Managment.RestoreBakFile(@".\SQLEXPRESS", "sa", "P2ssw0rd", "test_1", @"C:\DATA\SQLSERVER\MSSQL12.SQLEXPRESS\MSSQL\Backup\test.bak");
           
            Console.WriteLine("Hello World!");
        }
    }
}
