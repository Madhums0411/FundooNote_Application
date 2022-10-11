using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly FundoContext fundoContext;
        
        public NotesRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;

        }
        public NotesEntity Create(NotesModel notes, long UserId)
        {
            try
            {
                NotesEntity notesent = new NotesEntity();
                var result = fundoContext.NotesTable.Where(e => e.UserID == UserId);
                if (result != null)
                {


                    notesent.UserID = UserId;
                    notesent.Title = notes.Title;
                    notesent.Description = notes.Description;
                    notesent.colour = notes.colour;
                    notesent.Image = notes.Image;
                    notesent.Archive = notes.Archive;
                    notesent.Pin = notes.Pin;
                    notesent.Trash = notes.Trash;
                    notesent.Reminder = notes.Reminder;
                    notesent.Created = notes.Created;
                    notesent.Edited = notes.Edited;

                    fundoContext.NotesTable.Add(notesent);
                    fundoContext.SaveChanges();
                    return notesent;
                }
                else
                {
                    return null;
                }

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
                var result = fundoContext.NotesTable.Where(e => e.UserID == userId).ToList();
                return result;

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
                var noteCheck = fundoContext.NotesTable.Where(x => x.NotesId == NoteId).FirstOrDefault();
                this.fundoContext.NotesTable.Remove(noteCheck);
                int result = this.fundoContext.SaveChanges();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
                var update = fundoContext.NotesTable.Where(x => x.NotesId == noteId).FirstOrDefault();
                if (update != null && update.NotesId == noteId)

                {
                    update.Title = noteModel.Title;
                    update.Description = noteModel.Description;
                    update.Reminder = noteModel.Reminder;
                    update.Edited = noteModel.Edited;
                }

                int result = fundoContext.SaveChanges();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
                var result = fundoContext.NotesTable.Where(x => x.UserID == UserId && x.NotesId == NotesId).FirstOrDefault();
                if (result.Pin == true)
                {
                    result.Pin = false;
                    fundoContext.SaveChanges();
                    return result;
                }
                else
                {
                    result.Pin = true;
                    fundoContext.SaveChanges();
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity NoteArchive(long UserId, long NoteId)
        {
            try
            {
                var result = fundoContext.NotesTable.Where(x => x.UserID == UserId && x.NotesId == NoteId).FirstOrDefault();
                if (result.Archive == true)
                {
                    result.Archive = false;
                    fundoContext.SaveChanges();
                    return null;
                }
                else
                {
                    result.Archive = true;
                    fundoContext.SaveChanges();

                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
