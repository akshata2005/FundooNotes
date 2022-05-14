using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.FundoNoteContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class LabelRL : IlabelRL
    {
        FundoContext fundo;
        public IConfiguration Configuration { get; }

        //Creating constructor for initialization
        public LabelRL(FundoContext fundo, IConfiguration configuration)
        {
            this.fundo = fundo;
            this.Configuration = configuration;
        }

        public async Task Addlabel(int userId, int Noteid, string LabelName)
        {
            try
            {
                var user = fundo.Users.FirstOrDefault(u => u.userID == userId);
                var note = fundo.Notes.FirstOrDefault(b => b.NoteId == Noteid);
                Entity.Label label = new Entity.Label
                {
                    User = user,
                    Note = note
                };
                label.LabelName = LabelName;
                fundo.Labels.Add(label);
                await fundo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<Entity.Label>> Getlabel(int userId)
        {
            try
            {
                List<Entity.Label> reuslt = await fundo.Labels.Where(u => u.userId == userId).Include(u => u.User).Include(u => u.Note).ToListAsync();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Entity.Label>> GetlabelByNoteId(int NoteId)
        {
            try
            {
                List<Entity.Label> reuslt = await fundo.Labels.Where(u => u.NoteId == NoteId).Include(u => u.User).Include(u => u.Note).ToListAsync();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Entity.Label> UpdateLabel(int userId, int LabelId, string LabelName)
        {
            try
            {

                Entity.Label reuslt = fundo.Labels.FirstOrDefault(u => u.LabelId == LabelId && u.userId == userId);

                if (reuslt != null)
                {
                    reuslt.LabelName = LabelName;
                    await fundo.SaveChangesAsync();
                    var result = fundo.Labels.Where(u => u.LabelId == LabelId).FirstOrDefaultAsync();
                    return reuslt;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteLabel(int LabelId, int userId)
        {
            try
            {
                var result = fundo.Labels.FirstOrDefault(u => u.LabelId == LabelId && u.userId == userId);
                fundo.Labels.Remove(result);
                await fundo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
