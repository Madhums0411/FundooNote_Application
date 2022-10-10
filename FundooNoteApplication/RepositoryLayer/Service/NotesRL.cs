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
        public object UserId { get; private set; }
        public NotesRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;

        }
        public NotesEntity Create(NotesModel notes, long UserId)
        {
            try
            {
                NotesEntity notesent = new NotesEntity();
                var result = fundoContext.NotesTable.Where(e => e.UserId == UserId);
                if (result != null)
                {


                    notesent.UserId = UserId;
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

    }
}
