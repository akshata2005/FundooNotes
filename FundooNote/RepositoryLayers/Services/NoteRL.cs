using DatabaseLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entity;
using RepositoryLayer.FundoNoteContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        //Initializing class
        FundoContext fundo;
        public IConfiguration Configuration { get; }

        //Creating constructor for initialization
        public NoteRL(FundoContext Fundo, IConfiguration configuration)
        {
            this.fundo = Fundo;
            this.Configuration = configuration;
        }

        //Creating method to add note 
        public async Task<Note> AddNote(NotePostModel notePostModel, int userId)
        {
            try
            {
                var user = fundo.Users.FirstOrDefault(u => u.userID == userId);
                Note note = new Note
                {
                    User = user
                };
                note.Title = notePostModel.Title;
                note.Description = notePostModel.Description;
                note.Color = notePostModel.BGColor;
                note.IsArchive = false;
                note.IsReminder = false;
                note.IsPin = false;
                note.IsTrash = false;
                note.CreatedAt = DateTime.Now;
                fundo.Add(note);
                await fundo.SaveChangesAsync();
                return note;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Note> GetNote(int noteId, int userId)
        {
            try
            {
                return await fundo.Notes.Where(u => u.NoteId == noteId)
                .Include(u => u.User).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Note>> GetAllNote(int userId)
        {
            try
            {
                return await fundo.Notes.Where(u => u.UserId == userId).Include(u => u.User).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteNote(int noteId, int userId)
        {
            try
            {
                Note res = fundo.Notes.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                fundo.Notes.Remove(res);
                await fundo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Note> UpdateNote(NotePostModel notePostModel, int noteId, int userId)
        {
            try
            {
                var res = fundo.Notes.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    res.Title = notePostModel.Title;
                    res.Description = notePostModel.Description;
                    res.Color = notePostModel.BGColor;
                    res.IsArchive = notePostModel.IsArchive;
                    res.IsReminder = notePostModel.IsReminder;
                    res.IsPin = notePostModel.IsPin;
                    res.IsTrash = notePostModel.IsTrash;
                    res.ModifiedAt = DateTime.Now;
                    await fundo.SaveChangesAsync();

                    return await fundo.Notes.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Note> ArchieveNote(int noteId, int userId)
        {
            try
            {
                var res = fundo.Notes.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    if (res.IsArchive == false)
                    {
                        res.IsArchive = true;
                    }
                    else
                    {
                        res.IsArchive = false;
                    }
                    await fundo.SaveChangesAsync();
                    return await fundo.Notes.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Note> PinNote(int noteId, int userId)
        {
            try
            {
                var res = fundo.Notes.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    if (res.IsPin == false)
                    {
                        res.IsPin = true;
                    }
                    else
                    {
                        res.IsPin = false;
                    }
                    await fundo.SaveChangesAsync();
                    return await fundo.Notes.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Note> TrashNote(int noteId, int userId)
        {
            try
            {
                var res = fundo.Notes.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    if (res.IsTrash == false)
                    {
                        res.IsTrash = true;
                    }
                    else
                    {
                        res.IsTrash = false;
                    }
                    await fundo.SaveChangesAsync();
                    return await fundo.Notes.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Note> ChangeColor(int noteId, int userId, string newColor)
        {
            try
            {
                var res = fundo.Notes.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (res != null)
                {
                    res.Color = newColor;
                    await fundo.SaveChangesAsync();
                    return await fundo.Notes.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}



        
    

