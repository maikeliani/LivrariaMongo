using LivrariaMongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

internal class Program
{
    private static void Main(string[] args)
    {


        MongoClient mongo = new MongoClient("mongodb://localhost:27017");
        var baseDeDados = mongo.GetDatabase("livraria");
        var collection = baseDeDados.GetCollection<BsonDocument>("Book");



        int Menu()
        {
            Console.Clear();
            int xpto;
            Console.WriteLine("\n\nMENU DE OPÇÕES\n1- CADASTRAR LIVRO"
                + "\n2- DELETAR LIVRO\n3- BUSCAR LIVRO\n4- CONSULTA LIVROS DISPONÍVEIS\n5- ATUALIZAR UM LIVRO" +
                "\n0- SAIR\nESCOLHA UMA OPÇÃO: \n");


            try
            {
                xpto = int.Parse(Console.ReadLine());
            }

            catch
            {
                return -1;
            }

            return xpto;
        }




        int option;

        do
        {
            option = Menu();

            switch (option)
            {
                case 1:
                    Console.WriteLine(RegisterBook());
                    Thread.Sleep(2000);
                    break;

                case 2:
                    DeleteBook();
                    Thread.Sleep(2000);
                    break;

                case 3:
                    SearchBook();
                    Thread.Sleep(2000);
                    break;

                case 4:
                    ReturnCollection();
                    Thread.Sleep(2000);
                    break;

                case 5:
                    UpdateBook();
                    Thread.Sleep(2000);
                    break;


                case 0:
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Informe um valor corretamente, de acordo com o menu.");
                    Console.Beep();
                    Thread.Sleep(1000);
                    break;
            }
        } while (option != 0);




        void DeleteBook()
        {
            if (collection.CountDocuments(new BsonDocument()) == 0)
            {
                Console.WriteLine("Nenhum livro disponivel!");
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("INFORME O ISBN DO LIVROQUE DESEJA DELETAR: ");
                var isbn = Console.ReadLine();
                var f = Builders<BsonDocument>.Filter.Regex("isbn", isbn);

                var b = collection.FindOneAndDelete(f);

                Console.WriteLine("LIVRO DELETADO");
            }


        }


        void UpdateBook()
        {
            Console.WriteLine("Informe o isbn do livro que deseja alterar: ");
            string wanted = Console.ReadLine();
            var filter = Builders<BsonDocument>.Filter.Eq("isbn", wanted);

            int option;
            do
            {
                Console.WriteLine("Escolha o  que deseja editar deste livro: ");
                Console.WriteLine("\n1- alterar titulo");
                Console.WriteLine("\n2- alterar isbn");
                Console.WriteLine("\n3- alterar editora");
                Console.WriteLine("\n4- alterar  autor");
                Console.WriteLine("\n0- sair");

                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 0:
                        Console.WriteLine("Você escolheu sair...");
                        Thread.Sleep(1000);
                        break;

                    case 1:
                        Console.WriteLine("informe o novo titulo:");
                        var title = Console.ReadLine();

                        var newTitle = Builders<BsonDocument>.Update.Set("name", title);

                        collection.UpdateOne(filter, newTitle);
                       
                        break;

                    case 2:
                        Console.WriteLine("novo  o novo valor para o isbn:");
                        var isbn = Console.ReadLine();

                        var updateIsbn = Builders<BsonDocument>.Update.Set("isbn", isbn);

                        
                        collection.UpdateOne(filter, updateIsbn);
                        break;



                    case 3:
                        Console.WriteLine("Informe o novo nome para  editora:");
                        var editor = Console.ReadLine();

                        var updateEditor = Builders<BsonDocument>.Update.Set("Publisher", editor);

                        collection.UpdateOne(filter, updateEditor);
                        break;


                    case 4:
                        Console.WriteLine("Informe o novo nome para o  autor:");
                        var author = Console.ReadLine();

                        var updateAuthor = Builders<BsonDocument>.Update.Set("author", author);

                        collection.UpdateOne(filter, updateAuthor);
                        break;
                }

            } while (option != 0);
        }




        void SearchBook()
        {
            var collection = baseDeDados.GetCollection<BsonDocument>("Book");
            Console.WriteLine("INFORME O ISBN DO LIVRO: ");
            var isbn = Console.ReadLine();
            var f = Builders<BsonDocument>.Filter.Regex("isbn", isbn);

            var b = collection.Find(f).FirstOrDefault();
            var book = BsonSerializer.Deserialize<Book>(b);
            Console.WriteLine(b);

        }


        string RegisterBook()
        {

            Console.WriteLine("Informe o nome do livro:  ");
            string name = Console.ReadLine();

            Console.WriteLine("Informe o nome do autor:  ");
            string a = Console.ReadLine();


            Console.WriteLine("Informe o isbn do livro:  ");
            string num = Console.ReadLine();

            Console.WriteLine("Informe o nome da editora:  ");
            string ed = Console.ReadLine();



            Book book = new(name, a, ed, num);

            Console.WriteLine(book.ToString());

            var dr = new BsonDocument
                {
                    {"name" , book.Name },
                    {"author" , book.Author },
                    {"Publisher" , book.Publisher },
                    {"isbn" , book.Isbn }

                };

            Console.WriteLine(dr);
            collection.InsertOne(dr);
            return "CADASTRADO";
        }





        void ReturnCollection()
        {

            var documents = collection.Find(new BsonDocument()).ToList();

            foreach (var document in documents)
            {
                var book = BsonSerializer.Deserialize<Book>(document);
                Console.WriteLine(book.ToString());


            }

        }



    }
}
