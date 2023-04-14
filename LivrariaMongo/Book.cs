using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LivrariaMongo
{
    [BsonIgnoreExtraElements]
    internal class Book

    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string  Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("Publisher")]
        public string Publisher { get; set; }

        [BsonElement("isbn")]
        [BsonRepresentation(BsonType.String)]
        public string Isbn { get; set; }




        public Book(string name, string author, string publisher, string isbn)
        {
            this.Name = name;
            this.Author = author;
            this.Publisher = publisher;
            this.Isbn = isbn;

        }


       

        
        public override string ToString()
        {
            return $"Nome: {Name}\nAutor: {Author}\nEditora: {Publisher}\nISBN: {Isbn}\n ";
        }



    }
}

