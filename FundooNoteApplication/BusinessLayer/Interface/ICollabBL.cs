using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity AddCollab(long notesId, string email);
        public string DeleteCollab(long collabId, string email);
        public List<CollabEntity> GetCollab(long userId);
    }
}
