using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface INoteService : IDisposable
    {
        void Save(FamilyNotes model, bool isNew = true);
        void Delete(int id);
        FamilyNotes GetNote(int id, int personId);
        KeyValuePair<int, List<NoteDto>> GetNotes(DataTableServerSide searchModel, int personId, int roleId, int userId);
    }
}
