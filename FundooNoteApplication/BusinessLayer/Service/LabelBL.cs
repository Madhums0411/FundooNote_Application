using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;

        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public LabelEntity CreateLabel(long UserId, long NoteId, string LabelName)
        {
            try
            {
                return labelRL.CreateLabel(UserId, NoteId, LabelName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<LabelEntity> GetLabel(long userId)
        {
            try
            {
                return labelRL.GetLabel(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public LabelEntity UpdateLabel(long labelId, string newLabelName)
        {
            try
            {
                return labelRL.UpdateLabel(labelId, newLabelName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string LabelDelete(long labelId, long noteId)
        {
            try
            {
                return labelRL.LabelDelete(labelId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
