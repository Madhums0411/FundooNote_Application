using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity Create(NotesModel notes, long UserId);
        public List<NotesEntity> GetNote(long userId);
        public bool DeleteNote(long NoteId);
        public bool UpdateNote(NotesModel noteModel, long noteId);
        public NotesEntity PinNote(long NoteId, long UserId);
        public NotesEntity NoteArchive(long UserId, long NotesId);
        public bool NoteTrash(long UserId, long NoteId);
        public NotesEntity NoteColourChange(long notesId, string Colour);
        public string Image(long userId, long noteId, IFormFile file);
    }
}
