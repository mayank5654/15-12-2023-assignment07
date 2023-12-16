using System;
using System.Data;
using System.Data.SqlClient;

namespace assignment7
{ 

    class Program
    {
        static string connectionString = "server=DESKTOP-LVRAQ1E;database=LibraryDBs;trusted_connection=true";
        static DataSet libraryDataSet = new DataSet();

        static void Main()
        {
            // Step 3: Retrieve Data into a DataSet
            RetrieveData();

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Display Book Inventory");
                Console.WriteLine("2. Add New Book");
                Console.WriteLine("3. Update Book Quantity");
                Console.WriteLine("4. Apply Changes to Database");
                Console.WriteLine("5. Exit");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            // Step 4: Display Book Inventory
                            DisplayBookInventory();
                            break;

                        case 2:
                            // Step 5: Add New Book
                            AddNewBook();
                            break;

                        case 3:
                            // Step 6: Update Book Quantity
                            UpdateBookQuantity();
                            break;

                        case 4:
                            // Step 7: Apply Changes to Database
                            ApplyChangesToDatabase();
                            break;

                        case 5:
                            // Exit the program
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        static void RetrieveData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Books";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(libraryDataSet, "Books");
            }
        }

        static void DisplayBookInventory()
        {
            Console.WriteLine("Book Inventory:");
            foreach (DataRow row in libraryDataSet.Tables["Books"].Rows)
            {
                Console.WriteLine($"Book ID: {row["BookId"]}, Title: {row["Title"]}, Author: {row["Author"]}, Genre: {row["Genre"]}, Quantity: {row["Quantity"]}");
            }
            Console.WriteLine();
        }

        static void AddNewBook()
        {
            Console.WriteLine("Add a new book to the library:");

            DataRow newRow = libraryDataSet.Tables["Books"].NewRow();

            Console.Write("Title: ");
            newRow["Title"] = Console.ReadLine();

            Console.Write("Author: ");
            newRow["Author"] = Console.ReadLine();

            Console.Write("Genre: ");
            newRow["Genre"] = Console.ReadLine();

            Console.Write("Quantity: ");
            newRow["Quantity"] = int.Parse(Console.ReadLine());

            libraryDataSet.Tables["Books"].Rows.Add(newRow);
        }

        static void UpdateBookQuantity()
        {
            Console.WriteLine("Update book quantity:");

            Console.Write("Enter the book title: ");
            string title = Console.ReadLine();

            DataRow[] foundRows = libraryDataSet.Tables["Books"].Select($"Title = '{title}'");

            if (foundRows.Length > 0)
            {
                Console.Write("Enter the new quantity: ");
                int newQuantity = int.Parse(Console.ReadLine());

                foundRows[0]["Quantity"] = newQuantity;
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        static void ApplyChangesToDatabase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Books", connection);

                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                adapter.Update(libraryDataSet, "Books");
            }
        }
    }

}   