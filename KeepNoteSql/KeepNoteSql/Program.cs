using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Channels;

namespace KeepNoteSql
{
    class Note
    {
        public void CreateNote(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from note", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            var row = ds.Tables[0].NewRow();

            //Console.Write("Enter Id: ");
            //row["id"] = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Title: ");
            row["title"] = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter Description: ");
            row["descrp"] = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter Date: ");
            row["dates"] = Convert.ToDateTime(Console.ReadLine());

            ds.Tables[0].Rows.Add(row);

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Note created successfully");

        }

        public void ViewNote(SqlConnection con)
        {
            Console.WriteLine("Enter id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlDataAdapter adp2 = new SqlDataAdapter($"select * from note where id = {id}", con);
            DataSet ds = new DataSet();
            adp2.Fill(ds, "notetable");
            for (int i = 0; i < ds.Tables["notetable"].Rows.Count; i++)
            {
                Console.WriteLine("id | title | descrp | date");
                for (int j = 0; j < ds.Tables["notetable"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["notetable"].Rows[i][j]} | ");
                }
                Console.WriteLine();
            }

        }
        public void ViewAllNotes(SqlConnection con)
        {
            SqlDataAdapter adp3 = new SqlDataAdapter("select * from note", con);
            DataSet ds = new DataSet();
            adp3.Fill(ds, "notetable");
            for (int i = 0; i < ds.Tables["notetable"].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables["notetable"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["notetable"].Rows[i][j]} | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine($"Total Notes are: {ds.Tables["notetable"].Rows.Count}");
        }
        public void UpdateNote(SqlConnection con)
        {
            Console.WriteLine("Enter the id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlDataAdapter adp4 = new SqlDataAdapter($"select * from note where id ={id}", con);
            DataSet ds = new DataSet();
            adp4.Fill(ds);

            Console.WriteLine("Enter the column name u want to update: ");
            string colname = Console.ReadLine();

            Console.WriteLine("Enter the row index u want to update: ");
            int index = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the updated value:");
            string value = Console.ReadLine();

            ds.Tables[0].Rows[index][colname] = value;

            SqlCommandBuilder builder = new SqlCommandBuilder(adp4);
            adp4.Update(ds);
            Console.WriteLine("Note updated successfully");

        }
        public void DeleteNote(SqlConnection con)
        {
            SqlDataAdapter adp5 = new SqlDataAdapter($"select * from note", con);
            DataSet ds = new DataSet();
            adp5.Fill(ds);

            Console.WriteLine("Enter the row u want to delete:");
            int row = Convert.ToInt32(Console.ReadLine());

            ds.Tables[0].Rows[row].Delete();

            SqlCommandBuilder builder = new SqlCommandBuilder(adp5);
            adp5.Update(ds);
            Console.WriteLine("Note deleted successfully");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server=IN-7G3K9S3; database=KeepNoteDB; Integrated Security=true");
            Note obj = new Note();
            string res = "";
            do
            {
                Console.WriteLine("Welcome to KeepNote App");
                Console.WriteLine("1.CreateNote");
                Console.WriteLine("2.ViewNote");
                Console.WriteLine("3.ViewAllNotes");
                Console.WriteLine("4.UpdateNote");
                Console.WriteLine("5.DeleteNote");

                Console.WriteLine("Enter ur choice");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            obj.CreateNote(con);
                            break;
                        }
                    case 2:
                        {
                            obj.ViewNote(con);
                            break;
                        }
                    case 3:
                        {
                            obj.ViewAllNotes(con);
                            break;
                        }
                    case 4:
                        {
                            obj.UpdateNote(con);
                            break;
                        }
                    case 5:
                        {
                            obj.DeleteNote(con);
                            break;
                        }
                }
                Console.WriteLine("Do u want to continue[y/n]");
                res = Console.ReadLine();

            } while (res.ToLower() == "y");
        }
    }
}