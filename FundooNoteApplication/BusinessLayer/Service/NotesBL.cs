﻿using BusinessLayer.Interface;
using CommonLayer.Model;
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
    }
}
