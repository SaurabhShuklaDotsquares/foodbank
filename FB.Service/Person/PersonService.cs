using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FB.Service
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> repoPerson;
        private readonly IRepository<Household> repoHousehold;
        private readonly IRepository<DeclarationHistory> repoDeclarationHistory;
        private readonly IRepository<Declaration> repoDeclaration;

        public PersonService(IRepository<Person> _repoPerson, IRepository<Household> _repoHousehold, IRepository<DeclarationHistory> _repoDeclarationHistory, IRepository<Declaration> _repoDeclaration)
        {
            repoPerson = _repoPerson;
            repoHousehold = _repoHousehold;
            repoDeclarationHistory = _repoDeclarationHistory;
            repoDeclaration = _repoDeclaration;
        }
        public Person GetPerson(int id)
        {
            return repoPerson.Query().Filter(e => e.PersonId == id).Include(x=>x.User).Get().FirstOrDefault();
        }

        public Person GetPersonById(int id)
        {
            return repoPerson.Query()
                .Include(c => c.CentralOffice)
                .Include(ch => ch.Charity)
                .Include(b => b.Branch)
                .Include(a => a.MmopersonAdditonalDetails)
                .Include(a => a.Address)
                .Include(h => h.Household)
                .Include(h => h.MmomemberCertificate)
                .Include(h => h.User)
                .Filter(e => e.PersonId == id).Get().FirstOrDefault();
        }
        public bool Save(Person person)
        {
            try
            {
                if (person.PersonId > 0)
                    repoPerson.Update(person);
                else
                    repoPerson.Insert(person);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Save Person
        /// </summary>
        /// <param name="household"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        public int SaveHousehold(Household household, bool isNew = true)
        {
            if (isNew)
            {
                repoHousehold.InsertGraph(household);
            }
            else
            {
                repoHousehold.Update(household);
            }
            return household.HouseholdId;
        }

        /// <summary>
        /// To get persons using surname
        /// </summary>
        /// <param name="surname"></param>
        /// <returns></returns>
        public List<Person> GetPersons(List<UserDataAccessDto> userDataAccess, int roleID, string surname, int? organisationId = null, int? charityId = null, int? branchId = null)
        {
            var predicate = PredicateBuilder.True<Person>();

            if (branchId.HasValue)
                predicate = predicate.And(p => p.BranchId == branchId.Value);
            else if (charityId.HasValue)
                predicate = predicate.And(p => p.Branch.CharityId == charityId.Value);
            else if (organisationId.HasValue)
                predicate = predicate.And(p => p.Branch.Charity.CentralOfficeId == organisationId.Value);

            if (!string.IsNullOrWhiteSpace(surname))
                predicate = predicate.And(p => p.Surname.StartsWith(surname));


            List<Person> res = repoPerson.Query().Filter(predicate)
               .OrderBy(o => o.OrderBy(oo => oo.Surname))
               .Get().ToList();

            return res;
        }
        public List<DeclarationHistory> GetDeclarationHistories(int PersonId)
        {
            return repoDeclarationHistory.Query().Filter(f => f.PersonId == PersonId).Get().ToList();
        }

        /// <summary>
        /// To save the declaration of person
        /// </summary>
        /// <param name="declaration"></param>
        public void SavePersonDeclaration(Declaration declaration)
        {
            var declarationHistories = GetDeclarationHistories(declaration.PersonId);
            if (declaration.DeclarationId == 0)
            {
                if (!declarationHistories.Any(e =>
                 (declaration.DateDeclarationSigned.HasValue ? e.DateDeclarationSigned == declaration.DateDeclarationSigned.Value : true)
                 && (declaration.DateDeclarationValidFrom.HasValue ? e.DateDeclarationValidFrom == declaration.DateDeclarationValidFrom.Value : true)
                 && (declaration.DateDeclarationValidTo.HasValue ? e.DateDeclarationValidTo == declaration.DateDeclarationValidTo.Value : true)
                ))
                {
                    repoDeclaration.InsertGraph(declaration);
                }
            }
            else
            {
                if (!declarationHistories.Any(e =>
                 (declaration.DateDeclarationSigned.HasValue ? e.DateDeclarationSigned == declaration.DateDeclarationSigned.Value : true)
                 && (declaration.DateDeclarationValidFrom.HasValue ? e.DateDeclarationValidFrom == declaration.DateDeclarationValidFrom.Value : true)
                 && (declaration.DateDeclarationValidTo.HasValue ? e.DateDeclarationValidTo == declaration.DateDeclarationValidTo.Value : true)
                ))
                {
                    repoDeclaration.Update(declaration);
                }
            }
        }

        public KeyValuePair<int, List<DeclarationDto>> GetDeclarationListById(DataTableServerSide searchModel,int personId)
        {
            var predicate = PredicateBuilder.True<Declaration>();
            predicate = CustomPredicate.BuildPredicate<Declaration>(searchModel, new Type[] { typeof(Declaration) });
            predicate = predicate.And(x => x.PersonId == personId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var centralOfficeList = repoDeclaration
                .Query()
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.DeclarationId))
                .CustomOrderBy(u => u.OrderBy(searchModel))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = DeclarationDtoMapper.DeclarationMap(centralOfficeList);

            KeyValuePair<int, List<DeclarationDto>> resultResponse = new KeyValuePair<int, List<DeclarationDto>>(totalCount, results);

            return resultResponse;
        }
      
        public void Dispose()
        {
            if (repoPerson != null)
            {
                repoPerson.Dispose();
            }
        }
    }
}
