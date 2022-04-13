﻿using EReaderNow.Data.Domain;

namespace EReaderNow.Data.Repository
{
    public interface ITextFieldsRepository
    {
        IQueryable<TextField> GetTextFields();
        TextField GetTextFieldById(Guid id);
        TextField GetTextFieldByWord(string CodeWord);
        void SaveTextField(TextField entity);
        void DeleteTextField(Guid id);
    }
}
