using Mono.Data.Sqlite;

namespace IoMaker
{
    public class databaseLogic
    {
        public static void CreateTable(){
            string cs = @"URI=file:C:\Users\Jano\Documents\test.db";

            using (var connection = new SqliteConnection("Data Source=templates.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS Templates (
                    templateID int,
                    Name varchar,
                    AI_INV varchar,
                    AI_IO varchar,
                    AI_VAR varchar,
                    AI_ACC varchar,
                    AO_INV varchar,
                    AO_IO varchar,
                    AO_VAR varchar,
                    AO_ACC varchar,
                    DI_INV varchar,
                    DI_IO varchar,
                    DI_ACC varchar,
                    DO_IO varchar,
                    DO_ACC varchar
                    );
                ";
            }
        }
    }
}
