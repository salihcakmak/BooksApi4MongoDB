using BooksApi4MongoDB.DbModels;
using BooksApi4MongoDB.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi4MongoDB.Services
{
    public class BookService
    {
        #region Fields
        private readonly IMongoCollection<Book> _books;
        #endregion

        #region Ctor
        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }
        #endregion

        #region Services
        //Kitap listesini dönderir
        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        //İstenilen kitap bilgisini dönderir
        public Book Get(string id) =>
            _books.Find(book => book.Id == id).FirstOrDefault();

        //Veri tabanına yeni kitap ekler
        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        //Kitap bilgisini günceller
        public void Update(string id, Book bookIn) =>
             _books.ReplaceOne(book => book.Id == id, bookIn);

        //Veri tabanından kitap nesnesini siler, parametre olarak direk modeli alabilir
        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        //Veri tabanından kitap nesnesini siler, parametre olarak id bilgisini alır
        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
        #endregion


    }
}
