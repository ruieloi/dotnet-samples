using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;

namespace sqlserver.management
{
    public class Managment
    {
        /***
         * Restores a Bak file to a SQL Server
         ***/
        public static bool RestoreBakFile(string dbServerMachineName, string username, string password, string dbName, string databaseBakFile)
        {           
            var serverConnection = new ServerConnection(dbServerMachineName, username, password);
             Server server = new Server(serverConnection);

            Database database = new Database(server, dbName);

            //If Need
            database.Create();
            database.Refresh();

            //Restoring
            Restore restore = new Restore
            {
                NoRecovery = false,
                Action = RestoreActionType.Database
            };

            BackupDeviceItem bdi = default(BackupDeviceItem);
            
            bdi = new BackupDeviceItem(databaseBakFile, DeviceType.File);

            restore.Devices.Add(bdi);
            restore.Database = dbName;
            restore.ReplaceDatabase = true;

            RelocateFile DataFile = new RelocateFile();
            string MDF = restore.ReadFileList(server).Rows[0][1].ToString();
            DataFile.LogicalFileName = restore.ReadFileList(server).Rows[0][0].ToString();
            DataFile.PhysicalFileName = server.Databases[dbName].FileGroups[0].Files[0].FileName;

            RelocateFile LogFile = new RelocateFile();
            string LDF = restore.ReadFileList(server).Rows[1][1].ToString();
            LogFile.LogicalFileName = restore.ReadFileList(server).Rows[1][0].ToString();
            LogFile.PhysicalFileName = server.Databases[dbName].LogFiles[0].FileName;

            restore.RelocateFiles.Add(DataFile);
            restore.RelocateFiles.Add(LogFile);

            restore.PercentCompleteNotification = 10;
            restore.SqlRestore(server);
            database.Refresh();
            database.SetOnline();
            server.Refresh();

            return true;
        }

    }
}
