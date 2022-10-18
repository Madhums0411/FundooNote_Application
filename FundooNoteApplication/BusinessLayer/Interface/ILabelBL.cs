using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity CreateLabel(long UserId, long NoteId, string LabelName);
        public List<LabelEntity> GetLabel(long userId);
        public LabelEntity UpdateLabel(long labelId, string newLabelName);
        public string LabelDelete(long labelId, long noteId);
    }
}
