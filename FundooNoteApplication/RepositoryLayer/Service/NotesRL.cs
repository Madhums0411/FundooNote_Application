using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly FundoContext fundoContext;
        //public object UserId { get; private set; }
        private readonly IConfiguration configuration;
        public NotesRL(FundoContext fundoContext, IConfiguration configuration)
        {
            this.fundoContext = fundoContext;
            this.configuration = configuration;

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
        public NotesEntity NoteArchive(long UserId, long NotesId)
        {
            try
            {
                var result = fundoContext.NotesTable.Where(x => x.UserID == UserId && x.NotesId == NotesId).FirstOrDefault();
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
        public bool NoteTrash(long UserId, long NoteId)
        {
            try
            {
                var result = fundoContext.NotesTable.Where(x => x.UserID == UserId && x.NotesId == NoteId).FirstOrDefault();
                if (result.Trash == true)
                {
                    result.Trash = false;
                    fundoContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Trash = true;
                    fundoContext.SaveChanges();
                    return true;
                }
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
                var result = fundoContext.NotesTable.Where(x => x.NotesId == notesId).FirstOrDefault();
                if (result != null)
                {
                    result.colour = Colour;
                    fundoContext.NotesTable.Update(result);
                    fundoContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public string Image(long userId, long notesId, IFormFile file)
        {
            try
            {
                var result = fundoContext.NotesTable.Where(u => u.UserID == userId && u.NotesId == notesId).FirstOrDefault();
                if (result != null)
                {
                    Account account = new Account(
                       this.configuration["CloudinarySettings:CloudName"],
                       this.configuration["CloudinarySettings:ApiKey"],
                        this.configuration["CloudinarySettings:ApiSecret"]
                        );
                    Cloudinary _cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream()),
                    };
                    var uploadresult = _cloudinary.Upload(uploadParams);
                    string ImagePath = uploadresult.Url.ToString();
                    result.Image = ImagePath;
                    fundoContext.SaveChanges();
                    return "Image upload Successfully";

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
