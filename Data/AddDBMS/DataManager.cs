using EReaderNow.Data.Repository;

namespace EReaderNow.Data.AddDBMS
{
    public class DataManager
    {
            public ITextFieldsRepository TextField { get; set; }
            public IRepositoryCategory BooksItems { get; set; }

            public IAllDescriptionBooks AllDescriptionBooks { get; set; }

            public DataManager(ITextFieldsRepository textFieldsRepository, IRepositoryCategory categoryRepository, IAllDescriptionBooks allDescriptionBooksRepository)
        {
            TextField = textFieldsRepository;
            BooksItems = categoryRepository;
            AllDescriptionBooks = allDescriptionBooksRepository;
            
    }

    }
}
