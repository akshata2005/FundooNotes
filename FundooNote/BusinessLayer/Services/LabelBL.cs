using BusinessLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL

    {
        IlabelRL labelRL;
        public LabelBL(IlabelRL ilabelRL)
        {
            this.labelRL = ilabelRL;
        }
        public async Task Addlabel(int userId, int Noteid, string LabelName)
        {
            try
            {
                await this.labelRL.Addlabel(userId, Noteid, LabelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Label>> Getlabel(int userId)
        {
            try
            {
                return await this.labelRL.Getlabel(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Label>> GetlabelByNoteId(int NoteId)
        {
            try
            {
                return await this.labelRL.Getlabel(NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Label> UpdateLabel(int userId, int LabelId, string LabelName)
        {
            try
            {
                return await this.labelRL.UpdateLabel(userId, LabelId, LabelName);
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
                await this.labelRL.DeleteLabel(LabelId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}



