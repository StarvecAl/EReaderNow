namespace EReaderNow.Models
{
    public class CategoryBooks
    {

        public int id { get; set; }
        public string name { get; set; }
        public string Author { get; set; }
        public string Desc { get; set; }
        public string longDesc { get; set; }
        public string img { get; set; }
        public ushort price { get; set; }
        public string genre { get; set; }

        public CategoryBooks(int id, string name, string Author, string genre, string Desc, ushort price, string img)
        {
            Console.WriteLine("Создание объекта CategotyBooks");
            this.id = id;
            this.name = name;
            this.Author = Author;
            this.genre = genre;
            this.Desc = Desc;
            this.price = price;
            this.img = img;
        }
    }
}
