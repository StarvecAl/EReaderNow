using EReaderNow.Data.Domain;

namespace EReaderNow.Data.Repository
{
    public interface IAllDescriptionBooks
    {
        IQueryable<AllDescriptionBooks> GetBooksFields();
        AllDescriptionBooks GetBooksFieldById(int id);
      
        void SaveBooksField(AllDescriptionBooks entity);
        void DeleteBooksField(int id);
    }
}
