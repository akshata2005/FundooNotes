using DatabaseLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        Task<Note> AddNote(NotePostModel notePostModel, int userId);
        Task<Note> GetNote(int noteId, int userId);
        Task<List<Note>> GetAllNote(int userId);
        Task DeleteNote(int noteId, int userId);
        Task<Note> UpdateNote(NotePostModel notePostModel, int noteId, int userId);
        Task<Note> ArchieveNote(int noteId, int userId);
        Task<Note> PinNote(int noteId, int userId);
        Task<Note> TrashNote(int noteId, int userId);
        Task<Note> ChangeColor(int noteId, int userId, string newColor);
    }
}
