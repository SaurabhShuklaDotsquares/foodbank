using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;

namespace FB.Service
{
    public interface IPersonService : IDisposable
    {
        Person GetPerson(int id);
        Person GetPersonById(int id);
        bool Save(Person person);
        int SaveHousehold(Household household, bool isNew = true);
        List<Person> GetPersons(List<UserDataAccessDto> userDataAccess, int roleID, string surname, int? organisationId = null, int? charityId = null, int? branchId = null);
        public List<DeclarationHistory> GetDeclarationHistories(int PersonId);
        public void SavePersonDeclaration(Declaration declaration);
        KeyValuePair<int, List<DeclarationDto>> GetDeclarationListById(DataTableServerSide searchModel, int personId);
    }
}
