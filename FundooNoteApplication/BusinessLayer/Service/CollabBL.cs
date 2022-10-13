using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        public CollabEntity AddCollab(long notesId, string email)
        {
            try
            {
                return collabRL.AddCollab(notesId, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string DeleteCollab(long collabId, string email)
        {
            try
            {
                return collabRL.DeleteCollab(collabId, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CollabEntity> GetCollab(long userId)
        {
            try
            {
                return collabRL.GetCollab(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
