using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.FundoNoteContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        FundoContext fundo;
        ILabelBL labelBL;
        public LabelController(ILabelBL labelBL, FundoContext fundo)
        {
            this.labelBL = labelBL;
            this.fundo = fundo;
        }

        //HTTP method to handle add label request
        [Authorize]
        [HttpPost("Addlabel/{NoteId}/{LabelName}")]
        public async Task<ActionResult> AddLabel(int NoteId, string LabelName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                await this.labelBL.Addlabel(userId, NoteId, LabelName);
                return this.Ok(new { success = true, message = $"Label added successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to handle get label request
        [Authorize]
        [HttpGet("Getlabel")]
        public async Task<ActionResult> GetLabel()
        {
            try
            {
                List<Label> list = new List<Label>();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                list = await this.labelBL.Getlabel(userId);
                if (list == null)
                {
                    return this.BadRequest(new { success = false, message = "Failed to get label" });
                }
                return this.Ok(new { success = true, message = $"Label get successfully", data = list });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to handle get label request
        [Authorize]
        [HttpGet("GetlabelByNoteId/{NoteId}")]
        public async Task<ActionResult> GetLabelByNoteId(int NoteId)
        {
            try
            {
                List<Label> list = new List<Label>();
                list = await this.labelBL.Getlabel(NoteId);
                if (list == null)
                {
                    return this.BadRequest(new { success = true, message = "Failed to get label" });
                }
                return this.Ok(new { success = true, message = $"Label get successfully", data = list });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to handle update label request
        [Authorize]
        [HttpPut("UpdateLabel/{LabelId}/{LabelName}")]
        public async Task<ActionResult> UpdateLabel(string LabelName, int LabelId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var result = await this.labelBL.UpdateLabel(userId, LabelId, LabelName);
                if (result == null)
                {
                    return this.BadRequest(new { success = true, message = "Updation of Label failed" });
                }
                return this.Ok(new { success = true, message = $"Label updated successfully", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to handle delete label request
        [Authorize]
        [HttpDelete("DeleteLabel/{LabelId}")]
        public async Task<ActionResult> DeleteLabel(int LabelId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                await this.labelBL.DeleteLabel(LabelId, userId);
                return this.Ok(new { success = true, message = $"Label Deleted successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        }
    }

