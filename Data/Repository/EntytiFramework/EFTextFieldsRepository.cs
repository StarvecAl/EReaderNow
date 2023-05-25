using EReaderNow.Data.AddDBMS;
using EReaderNow.Data.Domain;

using Microsoft.EntityFrameworkCore;


namespace EReaderNow.Data.Repository.EntytiFramework
{
    public class EFTextFieldsRepository : ITextFieldsRepository
    {   
        private readonly AddDB context;

        public EFTextFieldsRepository(AddDB context)
        { 
        this.context = context;
        }

        public IQueryable<TextField> GetTextFields()
        {
            return context.TextField;
        }

        public TextField GetTextFieldById(Guid id)
        {
            return context.TextField.FirstOrDefault(x => x.ID == id);
        }

        public TextField GetTextFieldByWord(string codeWord)
        {
            return context.TextField.FirstOrDefault(x => x.codeWord == codeWord);
        }

        public void SaveTextField(TextField entity)
        {
            if (entity.ID == default)
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            else
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteTextField(Guid id)
        {
            context.TextField.Remove(new TextField() { ID = id });
            context.SaveChanges();
        }
    }
} 
