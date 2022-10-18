using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }

        public NotesEntity Create(NotesModel notes, long UserId)
        {
            try
            {
                return notesRL.Create(notes, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<NotesEntity> GetNote(long userId)
        {
            try
            {
                return notesRL.GetNote(userId);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool DeleteNote(long NoteId)
        {
            try
            {
                return notesRL.DeleteNote(NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateNote(NotesModel noteModel, long noteId)
        {
            try
            {
                return notesRL.UpdateNote(noteModel, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity PinNote(long NotesId, long UserId)
        {
            try
            {
                return notesRL.PinNote(NotesId, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity NoteArchive(long UserId, long NotesId)
        {
            try
            {
                return notesRL.NoteArchive(UserId, NotesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool NoteTrash(long UserId, long NoteId)
        {
            try
            {
                return notesRL.NoteTrash(UserId, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity NoteColourChange(long notesId, string Colour)
        {
            try
            {
                return notesRL.NoteColourChange(notesId, Colour);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public string Image(long UserId, long notesId, IFormFile file)
        {
            try
            {
                return notesRL.Image(UserId, notesId, file);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}

