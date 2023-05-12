using System.Data;
using System.Data.SqlClient;

namespace Disconnected
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server = IN-7G3K9S3; database = TestDB; Integrated Security = true");
            SqlDataAdapter adp = new SqlDataAdapter("select * from products", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            var row = ds.Tables[0].NewRow();

            Console.Write("Enter ProductId: ");
            row["ProductId"] = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter ProductName: ");
            row["ProductName"] = Console.ReadLine();

            Console.Write("Enter Brand: ");
            row["Brand"] = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            row["Quantity"] = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Price: ");
            row["Price"] = Convert.ToDouble(Console.ReadLine());



            //row["ProductId"] = 3;
            //row["ProductName"] = "Laptop";
            //row["Brand"] = "Dell";
            //row["Quantity"] = 3;
            //row["Price"] = 85000;

            ds.Tables[0].Rows.Add(row);


            //ds.Tables[0].Rows[1][3] = 8;


            //ds.Tables[0].Rows[1].Delete();


            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("database updated");
        }
    }
}