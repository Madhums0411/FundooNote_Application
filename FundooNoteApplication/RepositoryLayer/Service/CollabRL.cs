using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRL : ICollabRL
    {
        private readonly FundoContext fundoContext;
        public CollabRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public CollabEntity AddCollab(long notesId, string email)
        {
            try
            {
                var notesModel = fundoContext.NotesTable.Where(x => x.NotesId == notesId).FirstOrDefault();
                var userModel = fundoContext.UserTable.Where(x => x.Email == email).FirstOrDefault();
                if (notesModel != null && userModel != null)
                {
                    CollabEntity collab = new CollabEntity();
                    collab.UserId = userModel.UserId;
                    collab.NotesId = notesModel.NotesId;
                    collab.CollabEmail = userModel.Email;
                    fundoContext.Add(collab);
                    fundoContext.SaveChanges();
                    return collab;
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