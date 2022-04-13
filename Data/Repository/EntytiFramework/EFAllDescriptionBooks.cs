using EReaderNow.Data.AddDBMS;
using EReaderNow.Data.Domain;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace EReaderNow.Data.Repository.EntytiFramework
{
    public class EFAllDescriptionBooks : IAllDescriptionBooks
    {
        private readonly AddDB context;

        public EFAllDescriptionBooks(AddDB context)
        {
            this.context = context;
        }

        public IQueryable<AllDescriptionBooks> GetBooksFields()
        {
            return context.AllDescriptionBooks;
        }

        public AllDescriptionBooks GetBooksFieldById(int id)
        {
            return context.AllDescriptionBooks.FirstOrDefault(x => x.ID == id);
        }

       

        public void SaveBooksField(AllDescriptionBooks entity)
        {
            if (entity.ID == default)
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            else
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteBooksField(int id)
        {
            context.AllDescriptionBooks.Remove(new AllDescriptionBooks() { ID = id });
            context.SaveChanges();
        }

    }
}
