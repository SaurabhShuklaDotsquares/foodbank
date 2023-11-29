using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.ModelMapper
{
    public static class NoteDtoMapper
    {
        public static NoteDto Map(FamilyNotes note)
        {
            return new NoteDto
            {
                NoteId = note.NoteId,
                PersonId = note.FamilyId,
                Privacy = note.Privacy,
                Description = note.Description,
                Comment = note.Privacy ? EncryptionUtils.Decrypt(note.Comment, "") : note.Comment,
                NoteDate = note.NoteDate.ToFormatCustomString(),
                CreatedBy = note.CreatedBy,
             
            };
        }

        public static List<NoteDto> Map(List<FamilyNotes> notes)
        {
            List<NoteDto> notesList = new List<NoteDto>();
            foreach(var note in notes)
            {
               notesList.Add(Map(note));
            }
            return notesList;
        }
    }
}
