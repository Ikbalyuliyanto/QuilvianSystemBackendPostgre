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

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetAgamaById(Guid id)
        // {
        //     var listdata = _applicationDbContext.Agamas.Find(id);
        //     if (listdata == null)
        //     {
        //         return NotFound(new { message = "Data tidak ditemukan." });
        //     }

        //     return Ok(new
        //     {
        //         message = "Ditemukan || 200 OK",
        //         data = listdata
        //     });
        // }

        [HttpPost]
        public async Task<IActionResult> AddAgama([FromBody] Agama newAgama)
        {
            if (newAgama == null)
            {
                return BadRequest(new { message = "Data tidak boleh kosong" });
            }

            // Validasi data yang diterima (misalnya NamaAgama tidak boleh kosong)
            if (string.IsNullOrEmpty(newAgama.NamaAgama))
            {
                return BadRequest(new { message = "Nama Agama tidak boleh kosong" });
            }

            // Menambahkan data Agama ke dalam database
            _applicationDbContext.Agamas.Add(newAgama);

            // Menyimpan perubahan ke dalam database
            await _applicationDbContext.SaveChangesAsync();

            // Mengembalikan respons sukses jika berhasil menambah data
            return CreatedAtAction(nameof(GetAllAgama), new { id = newAgama.AgamaId }, new
            {
                message = "Berhasil menambahkan Agama",
                data = newAgama
            });
        }

    }
}

