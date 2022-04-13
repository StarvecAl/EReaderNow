using EReaderNow.Data.AddDBMS;
using EReaderNow.Data.Domain;
using Microsoft.EntityFrameworkCore;


namespace EReaderNow.Data.Repository.EntytiFramework
{
    public class EFBooksItem : IRepositoryCategory
    {   
       
        private readonly AddDB context;

        public EFBooksItem (AddDB context)
        {
            
            this.context = context;
            BooksItem.allGenre = context.genres.ToList();
        }

        public IQueryable<BooksItem> GetBooksFields()
        {
            return context.BooksItem;
        }
        public IQueryable<BooksItem>  GetBooksInclude()
        {
            return context.BooksItem.Include(c=> c.textBooks).Include(x=> x.Genres);
        }
        public IQueryable<Genre> GetGenre()
        {
            return context.genres;
        }
        public List<BooksItem> GetBooksGnenre(int genre)
        {
            IQueryable<int?> genreBooks = context.listGenre.Where(y => y.GenreID == genre).Select(x=> x.BooksItemID);
            List<BooksItem> booksItems = new List<BooksItem>();
            foreach (int genreBook in genreBooks)
            {
                booksItems.Add(GetBooksFieldById(genreBook));
            }
            return booksItems;
        }
     /*   public string GetGenreById(int id)
        {
      
            foreach (var genre in genre)
            { if(genre.ID ==id ) return genre.genreName; }
            return null;
        }*/
        public void Save(BooksItem entity)
        {

            context.SaveChanges();

        }
        public void SaveGenre(Genre entity)
        {        
            context.Add(entity);
            context.SaveChanges();
        }
        public void SaveListGenre(int idBook, int idGenre)
        {
            ListGenre entity2 = new ListGenre() { BooksItemID = idBook, GenreID = idGenre };
            context.Add(entity2);
            context.SaveChanges();

        }

        public void SaveBooksField(AllDescriptionBooks entity)
        {
            if (entity.ID == default)
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            else
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public void SaveBookView(ref AllDescriptionBooks entity)
        {
            context.SaveChanges();
        }

        public BooksItem GetBooksFieldById(int id)
        {
            return context.BooksItem.Include(x => x.Genres).Include(c => c.textBooks).FirstOrDefault(x => x.ID == id);
        }

        /*public BooksItem GetBooksFieldByGenre(Genre genre)
        {
            return context.BooksItem.FirstOrDefault(x => x.genre.Find(x => x == genre) == genre);
        }*/

        public void SaveBooksField(BooksItem entity)
        {

            if (entity.ID == default)
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            else 
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteBooksField(int id)
        {
            context.BooksItem.Remove(new BooksItem() { ID = id });
            context.SaveChanges();
        }
    }
    
}
