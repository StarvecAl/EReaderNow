using EReaderNow.Data.Domain;
using EReaderNow.ViewModel;

namespace EReaderNow.Data.Repository
{
    public interface IRepositoryCategory
    {

        IQueryable<BooksItem> GetBooksFields();
        IQueryable<Genre> GetGenre();
    /*    string GetGenreById(int id);*/
        BooksItem GetBooksFieldById(int ID);
        public IQueryable<BooksItem> GetBooksInclude();
        public List<BooksItem> GetBooksGnenre(int genre);
        void SaveBooksField(AllDescriptionBooks entity);
        void SaveBookView(ref AllDescriptionBooks entity);
        void Save(BooksItem entity);
        void SaveListGenre(int id,int genre);
       /* BooksItem GetBooksFieldByGenre(ListGenre genre);*/
        void SaveBooksField(BooksItem entity);
        void SaveGenre(Genre genre);
        void DeleteBooksField(int categoryID);
    }
}
