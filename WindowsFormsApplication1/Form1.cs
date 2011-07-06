using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(System.Guid.NewGuid().ToString("D"));
            string connectionString = "mongodb://localhost";
            MongoServer server = MongoServer.Create(connectionString);

            server.Connect();
            var db = server.GetDatabase("Phuc");

            MongoCollection books = db.GetCollection("books");

            var count = books.Count();
            var findBook = books.FindOneAs<BsonDocument>();
            
            //var query = new QueryDocument("author", "Phuc Nguyen");
            //var update = new UpdateDocument { "$set", new BsonDocument("title", "Duong thai")};
            //var update = MongoDB.Driver.Builders.Update.Set("title", "Duong thai");
            //books.Update(query, update);

            var query = Query.And(
                Query.Or(
                    Query.EQ("author", "Ha Linh"),
                    Query.EQ("author", "Phuc Nguyen")
                ),
                Query.EQ("title", "Duong thai")
                );
            var result = books.FindAs<BsonDocument>(query);
            foreach (BsonDocument item in result) {
                var z = item;
            }

            /*BsonDocument book = new BsonDocument { 
                {"author", "Ha Linh"},
                {"title","Duong thai"}
            };
            books.Insert(book);*/

            server.Disconnect();

        }
    }
}
