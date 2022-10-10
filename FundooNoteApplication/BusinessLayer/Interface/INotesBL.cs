using CommonLayer.Model;
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
    }
}
