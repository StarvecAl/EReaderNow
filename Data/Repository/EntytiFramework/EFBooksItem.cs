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
            // Сохраняет полный список жанров при первом обращении к БД
            BooksItem.allGenre = context.genres.ToList();
        }

        public IQueryable<BooksItem> GetBooksFields()
        {
            return context.BooksItem;
        }
        public IQueryable<BooksItem>  GetBooksInclude(int i)
        {
            return context.BooksItem.Skip(i - 1).Include(c=> c.textBooks).Include(x=> x.Genres).Take(20);
        }
        public IQueryable<Genre> GetGenre()
        {
            return context.genres; 
        }
        public List<BooksItem> GetBooksGnenre(int genre,int skip)
        {
            IQueryable<BooksItem> genreBooks = context.listGenre.Where(y => y.Genre.ID == genre).Include(x => x.BooksItem.Genres).Select(x=> x.BooksItem).Skip(skip - 1).Take(20);
            
            return genreBooks.ToList();
        }
        public List<BooksItem> GetBooksGnenre(int? genre, int skip, string filtr)
        {
            if (genre != null && genre != 0)
            {
                IQueryable<BooksItem> genreBooks = context.listGenre.Where(y => y.Genre.ID == genre).Include(x => x.BooksItem.Genres).Select(x => x.BooksItem).Where(x => EF.Functions.Like(x.name!, "%" + filtr + "%")).Skip(skip - 1).Take(20);

                return genreBooks.ToList();
            }
            else return context.BooksItem.Where(x => EF.Functions.Like(x.name!, "%" + filtr + "%")).Include(x => x.Genres).ToList();

        }
        public void Save(BooksItem entity)
        {

            context.SaveChanges();

        }
        public void SaveGenre(Genre entity)
        {        
            context.Add(entity);
            context.SaveChanges();
        }
        public void SaveListGenre(Genre genre, BooksItem book)
        {
            ListGenre entity2 = new ListGenre() { BooksItem = book, Genre = genre };
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
