using sqlserver.management;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbName = "test_1";            
            var bakFile = @"C:\DATA\SQLSERVER\MSSQL12.SQLEXPRESS\MSSQL\Backup\test.bak";

            Console.WriteLine("Restoring file: ");
            Console.WriteLine(bakFile);

            try
            {
                Managment.RestoreBakFile(@".\SQLEXPRESS", "sa", "P2ssw0rd", dbName, bakFile);
                Console.WriteLine("File Restored");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error:" + ex.ToString());
            }
            Console.WriteLine("Hit any key to finish!");
            Console.ReadKey();
        }
    }
}
