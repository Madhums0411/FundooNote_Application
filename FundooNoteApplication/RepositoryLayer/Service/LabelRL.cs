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

        public LabelEntity CreateLabel(long UserId, long NotesId, string LabelName)
        {
            try
            {
                LabelEntity label = new LabelEntity();
                label.UserId = UserId;
                label.NotesId = NotesId;
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
        
    }
}
