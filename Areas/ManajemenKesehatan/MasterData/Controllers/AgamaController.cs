using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuilvianSystemBackendDev.Areas.ManajemenKesehatan.MasterData.Models;
using QuilvianSystemBackendDev.Areas.ManajemenKesehatan.MasterData.ViewModels;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QuilvianSystemBackend.Repositories;

namespace QuilvianSystemBackendDev.Areas.ManajemenKesehatan.MasterData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    //[EnableCors("AllowSpecific")]
    public class AgamaController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public AgamaController
        (
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment
        )
        {
            _applicationDbContext = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAgama()
        {
            var listdata = _applicationDbContext.Agamas.ToList();
            if (listdata == null || !listdata.Any())
            {
                return NotFound(new { message = "Belum ada data. || 404 Not Found" });
            }

            return Ok(new
            {
                message = "Berhasil || 200 OK",
                data = listdata
            });
        }
        

        //[HttpPost]
        //public async Task<IActionResult> CreateAgama([FromForm] AgamaViewModel vm)
        //{
        //    if (vm == null || !ModelState.IsValid)
        //    {
        //        return BadRequest(new { message = "Data tidak valid." });
        //    }

        //    try
        //    {
        //        // **Ambil User ID dari JWT Claims**
        //        var EmailLogin = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        var GetUserActive = _applicationDbContext.UserActives.Where(u => u.Email == EmailLogin).FirstOrDefault();
        //        var UserActiveId = GetUserActive.UserActiveId;

        //        if (string.IsNullOrEmpty(EmailLogin))
        //        {
        //            return Unauthorized(new { message = "User tidak terautentikasi!" });
        //        }

        //        var dateNow = DateTimeOffset.Now;
        //        var setDateNow = dateNow.ToString("yyMMdd");

        //        // Ambil data terakhir untuk hari ini (tanpa ToString di query)
        //        var lastCode = _applicationDbContext.Agamas
        //            .Where(d => d.CreateDateTime.Date == dateNow.Date)
        //            .OrderByDescending(k => k.KodeAgama)
        //            .FirstOrDefault();

        //        string kode;
        //        if (lastCode == null)
        //        {
        //            kode = $"AGM{setDateNow}0001";
        //        }
        //        else
        //        {
        //            var lastCodeTrim = lastCode.KodeAgama.Substring(3, 6);

        //            if (lastCodeTrim != setDateNow)
        //            {
        //                kode = $"AGM{setDateNow}0001";
        //            }
        //            else
        //            {
        //                kode = $"AGM{setDateNow}" + (Convert.ToInt32(lastCode.KodeAgama.Substring(9)) + 1).ToString("D4");
        //            }
        //        }

        //        // Cek Duplikasi
        //        var isDuplicate = _applicationDbContext.Agamas
        //            .Any(c => c.KodeAgama == kode && c.NamaAgama == vm.NamaAgama);

        //        if (isDuplicate)
        //        {
        //            return Conflict(new { message = "Terdapat duplikasi data! || 409 Conflict Data" });
        //        }

        //        // Simpan Data
        //        var data = new Agama
        //        {
        //            AgamaId = Guid.NewGuid(),                   
        //            KodeAgama = kode,
        //            NamaAgama = vm.NamaAgama                    
        //        };

        //        _applicationDbContext.Agamas.Add(data);
        //        _applicationDbContext.SaveChanges();

        //        return Created("", new
        //        {
        //            message = "Tambah Data Berhasil || 201 Created",                    
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = $"Terjadi kesalahan internal: {ex.Message}" });
        //    }            
        //}

        //[HttpPut("{id}")]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> UpdateAgama(Guid id, [FromForm] AgamaViewModel vm)
        //{
        //    if (vm == null || !ModelState.IsValid)
        //    {
        //        return BadRequest(new { message = "Data tidak valid." });
        //    }

        //    try
        //    {
        //        //Ambil User ID dari JWT Claims
        //        var EmailLogin = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        var GetUserActive = _applicationDbContext.UserActives.Where(u => u.Email == EmailLogin).FirstOrDefault();
        //        var UserActiveId = GetUserActive.UserActiveId;

        //        if (string.IsNullOrEmpty(EmailLogin))
        //        {
        //            return Unauthorized(new { message = "User tidak terautentikasi!" });
        //        }

        //        // **Cari Data Pasien**
        //        var data = _applicationDbContext.Agamas.Find(id);
        //        if (data == null)
        //        {
        //            return NotFound(new { message = "Data tidak ditemukan." });
        //        }

        //        // **Update Data Pasien**
        //        data.NamaAgama = vm.NamaAgama;

        //        data.UpdateBy = UserActiveId;
        //        data.UpdateDateTime = DateTimeOffset.Now;

        //        _applicationDbContext.Agamas.Update(data);
        //        _applicationDbContext.SaveChanges();

        //        return Ok(new
        //        {
        //            message = "Update Data Berhasil || 200 OK",                    
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = $"Terjadi kesalahan internal: {ex.Message}" });
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAgama(Guid id)
        //{
        //    try
        //    {
        //        //Ambil User ID dari JWT Claims
        //        var EmailLogin = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        var GetUserActive = _applicationDbContext.UserActives.Where(u => u.Email == EmailLogin).FirstOrDefault();
        //        var UserActiveId = GetUserActive.UserActiveId;

        //        if (string.IsNullOrEmpty(EmailLogin))
        //        {
        //            return Unauthorized(new { message = "User tidak terautentikasi!" });
        //        }

        //        // **Cari Data Pasien**
        //        var data = _applicationDbContext.Agamas.Find(id);
        //        if (data == null)
        //        {
        //            return NotFound(new { message = "Data tidak ditemukan." });
        //        }

        //        // **Soft Delete (Tandai Data sebagai Terhapus)**
        //        data.DeleteBy = UserActiveId;
        //        data.DeleteDateTime = DateTimeOffset.Now;
        //        data.IsDelete = true;

        //        _applicationDbContext.Agamas.Update(data);
        //        _applicationDbContext.SaveChanges();

        //        return Ok(new { message = "Data berhasil dihapus..." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = $"Terjadi kesalahan internal: {ex.Message}" });
        //    }
        //}

    }
}

