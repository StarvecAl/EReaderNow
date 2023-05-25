using EReaderNow.Data.Domain;
using EReaderNow.ViewModel;

namespace EReaderNow.Data.Repository
{
    public interface IRepositoryCategory
    {
        //список книг
        IQueryable<BooksItem> GetBooksFields();
       
        IQueryable<Genre> GetGenre();
    /*    string GetGenreById(int id);*/
        //книга по ID
        BooksItem GetBooksFieldById(int ID);
        // Выдать книги включая свяанные данные, в параметрах страница
        public IQueryable<BooksItem> GetBooksInclude(int i);
        // Выдать книгу по жанру
        public List<BooksItem> GetBooksGnenre(int genre, int skip);
        public List<BooksItem> GetBooksGnenre(int? genre, int skip, string filtr);
        //?
        void SaveBooksField(AllDescriptionBooks entity);
        // Сохранить текст книги
        void SaveBookView(ref AllDescriptionBooks entity);
        // ?
        void Save(BooksItem entity);
        // Сохранить жанры книги, вынесенно отдельно
        void SaveListGenre(Genre id,BooksItem genre);
       /* BooksItem GetBooksFieldByGenre(ListGenre genre);*/
       // Сохранить книгу
        void SaveBooksField(BooksItem entity);
        // Сохранить жанры
        void SaveGenre(Genre genre);
       // Удалить книгу, удалятся ли свзяанные файлы?
        void DeleteBooksField(int categoryID);
    }
}
