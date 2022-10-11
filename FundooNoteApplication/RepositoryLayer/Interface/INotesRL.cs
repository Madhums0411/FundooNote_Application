using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity Create(NotesModel notes, long UserId);
        public List<NotesEntity> GetNote(long userId);
        public bool DeleteNote(long NoteId);
        public bool UpdateNote(NotesModel noteModel, long noteId);
        public NotesEntity Pin(long NoteId, long UserId);

    }
}
