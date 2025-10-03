using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Test.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        Connect conn = new Connect();

        [HttpGet]
        public List<Book> GetAllBooks()
        {

            conn.Connection.Open();

            List<Book> books = new List<Book>();

            string sql = "SELECT * FROM books";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var book = new Book
                {
                    Id = dr.GetInt32(0),
                    Title = dr.GetString(1),
                    Author = dr.GetString(2),
                    RelesDate = dr.GetDateTime(3)
                };

                books.Add(book);
            }

            conn.Connection.Close();

            return books;
        }


        [HttpGet("getById")]
        public Book GetById(int id)
        {
            conn.Connection.Open();

            string sql = "SELECT * FROM books WHERE Id = @id";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            var book = new Book
            {
                Id = dr.GetInt32(0),
                Title = dr.GetString(1),
                Author = dr.GetString(2),
                RelesDate = dr.GetDateTime(3)
            };

            conn.Connection.Close();

            return book;
        }

        [HttpDelete("deleteById")]
        public object DeleteItem(int id)
        {
            conn.Connection.Open();

            string sql = "DELETE FROM `books` WHERE id = @id";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            conn.Connection.Close();

            var result = new
            {
                message = "Sikeres törlés.",

            };

            return result;
        }
        
        [HttpPut("addNewItem")]
        public object AddNewItem(Book newRecord)
        {

            conn.Connection.Open();

            string sql = "INSERT INTO books(Title, Author, RelesDate) VALUES (@title, @author, @releasedate)";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            var book = newRecord.GetType().GetProperties();

            cmd.Parameters.AddWithValue("@title", book[0].GetValue(newRecord));
            cmd.Parameters.AddWithValue("@author", book[1].GetValue(newRecord));
            cmd.Parameters.AddWithValue("@releasedate", book[2].GetValue(newRecord));

            cmd.ExecuteNonQuery();

            conn.Connection.Close();

            var result = new
            {
                message = "Sikeres felvétel",
                result = newRecord
            };

            return result;
        }

        [HttpOptions("UpdateItemById")]

        public object UpdateItem(Book modifiedItem)
        {

            conn.Connection.Open();

            string sql = "UPDATE books SET Title = @title, Author = @author, RelesDate = @releasedate  WHERE Id = @id";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            var book = modifiedItem.GetType().GetProperties();

            cmd.Parameters.AddWithValue("@id", book[0].GetValue(modifiedItem));
            cmd.Parameters.AddWithValue("@title", book[1].GetValue(modifiedItem));
            cmd.Parameters.AddWithValue("@author", book[2].GetValue(modifiedItem));
            cmd.Parameters.AddWithValue("@releasedate", book[3].GetValue(modifiedItem));

            cmd.ExecuteNonQuery();

            conn.Connection.Close();

            var result = new
            {
                message = "Sikeres felvétel",
                result = modifiedItem
            };

            return result;
        }
    }
}
