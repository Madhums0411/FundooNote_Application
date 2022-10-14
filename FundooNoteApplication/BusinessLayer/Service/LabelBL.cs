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

        public LabelEntity CreateLabel(long UserId, long NotesId, string LabelName)
        {
            try
            {
                return labelRL.CreateLabel(UserId, NotesId, LabelName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
