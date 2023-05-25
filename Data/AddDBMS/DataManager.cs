using EReaderNow.Data.Repository;

namespace EReaderNow.Data.AddDBMS
{
    public class DataManager
    {
            public ITextFieldsRepository TextField { get; set; }
            public IRepositoryCategory BooksItems { get; set; }
      
         


        public DataManager( ITextFieldsRepository textFieldsRepository, IRepositoryCategory categoryRepository)
        {
            TextField = textFieldsRepository;
            BooksItems = categoryRepository;

          
    }

    }
}
