using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity CreateLabel(long UserId, long NoteId, string LabelName);
        public List<LabelEntity> GetLabel(long UserId);
        public LabelEntity UpdateLabel(long labelId, string newLabelName);
        public string LabelDelete(long labelId, long noteId);
    }
}
