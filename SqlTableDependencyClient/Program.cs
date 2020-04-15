using System;
using TableDependency.SqlClient;

using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient.Base.Enums;

namespace SqlTableDependencyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var connectionString = "Data Source=DESKTOP-FOPG83K;Initial Catalog=TableDependencyDB;Integrated Security=True";
            using (var tableDependency = new SqlTableDependency<Customer>(connectionString, "Customers"))
            {
                tableDependency.OnChanged += TableDependency_Changed;
                tableDependency.OnError += TableDependency_OnError;
                tableDependency.Start();
                Console.WriteLine("Waiting to receive  notification");
                Console.WriteLine("Press a key to stop ");
                Console.ReadKey();
                tableDependency.Stop();
            }
                    }

        static void TableDependency_Changed(object sender, RecordChangedEventArgs<Customer> e)
        {
            Console.WriteLine(Environment.NewLine);
            if (e.ChangeType != ChangeType.None)
            {
                var changedEntity = e.Entity;
                Console.WriteLine("DML operation" + e.ChangeType);
                Console.WriteLine("ID" + changedEntity.Id);
                Console.WriteLine("Machine Name" + changedEntity.MachineName);
                Console.WriteLine("Type" + changedEntity.Type);
                Console.WriteLine("Status" + changedEntity.MachineStatus);
                Console.WriteLine(Environment.NewLine);
            }
        }

        static void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            Exception ex = e.Error;
        }
    }
}
