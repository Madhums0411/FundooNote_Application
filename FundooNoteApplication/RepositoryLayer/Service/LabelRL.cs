using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundoContext fundoContext;

        public LabelRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        public LabelEntity CreateLabel(long UserId, long NoteId, string LabelName)
        {
            try
            {
                LabelEntity label = new LabelEntity();
                label.UserId = UserId;
                label.NotesId = NoteId;
                label.LabelName = LabelName;
                fundoContext.Add(label);
                int res = fundoContext.SaveChanges();
                if (res > 0)
                {
                    return label;
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
        public List<LabelEntity> GetLabel(long UserId)
        {
            try
            {
                var result = fundoContext.LabelTable.Where(u => u.UserId == UserId).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                    return null;
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
                var labelNameCheck = fundoContext.LabelTable.Where(x => x.LabelID == labelId).FirstOrDefault();
                if (labelNameCheck != null)
                {
                    labelNameCheck.LabelName = newLabelName;
                    fundoContext.SaveChanges();
                    return labelNameCheck;
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
        public string LabelDelete(long labelId, long noteId)
        {
            try
            {
                var labelTable = fundoContext.LabelTable.Where(x => x.LabelID == labelId && x.NotesId == noteId).FirstOrDefault();
                if (labelTable != null)
                {
                    fundoContext.LabelTable.Remove(labelTable);
                    fundoContext.SaveChanges();
                    return "Successfully Deleted The Label";
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
