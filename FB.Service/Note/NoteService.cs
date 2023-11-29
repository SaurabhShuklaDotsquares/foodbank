using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.ModelMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Service
{
    public class NoteService : INoteService
    {
        private IRepository<FamilyNotes> repoNote;
        private IUserService userService;
        public NoteService(IRepository<FamilyNotes> _repoNote, IUserService _userService)
        {
            this.repoNote = _repoNote;
            this.userService = _userService;

        }

        /// <summary>
        /// To save the notes of person
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isNew"></param>
        public void Save(FamilyNotes model, bool isNew = true)
        {
            if (isNew)
            {
                repoNote.InsertGraph(model);
            }
            else
            {
                repoNote.Update(model);
            }
        }

        /// <summary>
        /// Get Note by id and personid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public FamilyNotes GetNote(int id, int personId)
        {
            return repoNote
                .Query()
                .Filter(f => f.NoteId == id && f.FamilyId == personId)
                .Get()
                .FirstOrDefault();
        }


        /// <summary>
        /// To delete the note
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            repoNote.Delete(id);
        }


        /// <summary>
        /// To get the notes
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public KeyValuePair<int, List<NoteDto>> GetNotes(DataTableServerSide searchModel, int personId, int roleId, int userId)
        {
            var userentity = userService.GetUser(userId);
            var predicate = CustomPredicate.BuildPredicate<FamilyNotes>(searchModel);
            predicate = predicate.And(r => r.FamilyId == personId);
            if (roleId != (int)UserRoles.SuperAdmin)
            {
                if (!userentity.IsPrivateNotesAccess)
                {
                    predicate = predicate.And(r => r.Privacy != true || r.CreatedBy == userId);
                }
            }

            //(CurrentUser.RoleID == (int)UserRoles.SuperAdmin ? c.Comment : (c.CreatedBy == CurrentUser.UserID ? c.Comment : userentity.IsPrivateNotesAccess ? c.Comment : string.Empty)) : c.Comment,
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);
            var res = repoNote
                .Query()
                .Filter(predicate)
                .CustomOrderBy(u => u.OrderBy(searchModel))
                .GetPage(page, searchModel.length, out int totalCount).ToList();
            var results = NoteDtoMapper.Map(res);
            return new KeyValuePair<int, List<NoteDto>>(totalCount > 0 ? totalCount : results.Count, results);


        }


        public void Dispose()
        {
            if (repoNote != null)
            {
                repoNote.Dispose();
                repoNote = null;
            }

            if (userService != null)
            {
                userService.Dispose();
                userService = null;
            }
        }
    }
}
