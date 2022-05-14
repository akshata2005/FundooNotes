using BusinessLayer.Interfaces;
using DatabaseLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class NoteController : ControllerBase
    {
        FundoContext fundo;
        INoteBL noteBL;
        public NoteController(INoteBL noteBL, FundoContext fundo)
        {
            this.noteBL = noteBL;
            this.fundo = fundo;
        }

        //HTTP method to handle registration user request
        [HttpPost("AddNote")]
        public async Task<ActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var result = await this.noteBL.AddNote(notePostModel, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Added Successfully!!", data = result });

                }
                return this.BadRequest(new { success = true, message = "Failed to add!!" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetNote/{NoteId}")]
        public async Task<ActionResult> GetNote(int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var result = await this.noteBL.GetNote(NoteId, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Note get successfully", data = result });
                }
                return this.BadRequest(new { success = true, message = $"Failed to get note" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllNotes")]
        public async Task<ActionResult> GetAllNotes()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                List<Note> result = new List<Note>();
                result = await this.noteBL.GetAllNote(userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Notes get successfully", data = result });
                }
                return this.BadRequest(new { success = true, message = $"Failed to get notes", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpDelete("DeleteNote/{noteId}")]
        public async Task<ActionResult> DeleteNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                await this.noteBL.DeleteNote(noteId, userId);
                return this.Ok(new { success = true, message = "Note deleted successfully!!!" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("UpdateNote/{NoteId}")]
        public async Task<IActionResult> UpdateNote(NotePostModel notePostModel, int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var result = await this.noteBL.UpdateNote(notePostModel, NoteId, userId);
                return this.Ok(new { success = true, message = $"Note updated successfully!!!", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("ArchieveNote/{noteId}")]
        public async Task<ActionResult> IsArchieveNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var res = await this.noteBL.ArchieveNote(noteId, userId);
                if (res != null)
                    return this.Ok(new { success = true, message = "Note Archieved successfully!!!" });
                else
                    return this.BadRequest(new { success = false, message = "Failed to archieve note or Id does not exists" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpPut("IsPinned/{noteId}")]
        public async Task<ActionResult> IsPinned(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var res = await this.noteBL.PinNote(noteId, userId);
                if (res != null)
                    return this.Ok(new { success = true, message = "Note pinned successfully!!!" });
                else
                    return this.BadRequest(new { success = false, message = "Failed to pin note or Id does not exists" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpPut("IsTrash{noteId}")]
        public async Task<ActionResult> IsTrash(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var res = await this.noteBL.TrashNote(noteId, userId);
                if (res != null)
                    return this.Ok(new { success = true, message = "Note trashed successfully!!!" });
                else
                    return this.BadRequest(new { success = false, message = "Failed to trash note or Id does not exists" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpPut("ChangeColorNote/{noteId}")]
        public async Task<ActionResult> ChangeColorNote(int noteId, string color)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var res = await this.noteBL.ChangeColor(noteId, userId, color);
                if (res != null)
                    return this.Ok(new { success = true, message = "Note color changed successfully!!!" });
                else
                    return this.BadRequest(new { success = false, message = "Failed to change color note or Id does not exists" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



        

