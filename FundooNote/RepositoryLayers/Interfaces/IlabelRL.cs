using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IlabelRL
    {
        Task Addlabel(int userId, int Noteid, string LabelName);
        Task<List<Label>> Getlabel(int userId);
        Task<List<Label>> GetlabelByNoteId(int NoteId);
        Task<Label> UpdateLabel(int userId, int LabelId, string LabelName);
        Task DeleteLabel(int LabelId, int userId);
    }
}
