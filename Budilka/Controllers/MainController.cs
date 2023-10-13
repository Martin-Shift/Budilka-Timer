using Budilka.DbModels;
using Budilka.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Budilka.Controllers
{
    public class MainController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly TimerDbContext _context;
        public IActionResult Timer()
        {
            return View(_context.Timers.OrderByDescending(x=> x.Id).ToList());
        }
        public MainController(IWebHostEnvironment webHostEnvironment, TimerDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        [HttpPost("Main/RemoveTimer/{id}")]
        public async Task<IActionResult> RemoveTimer(int id)
        {
            var timer =  await _context.Timers.FirstAsync(x => x.Id == id);
            _context.Timers.Remove(timer);
            await _context.SaveChangesAsync();
            return Ok(new { });

        }
        [HttpPost("Main/SetTimer")]
        public async Task<IActionResult> SetTimer([FromBody] AddTimerModel model)
        {
          
            TimeSpan timeSpan = TimeSpan.FromSeconds(0);
            if (model.Date == null)
            {
                timeSpan = new TimeSpan(model.Days, model.Hours, model.Minutes, model.Seconds);
            }
            else
            {
                timeSpan = (TimeSpan)(model.Date - DateTime.Now) + TimeSpan.FromHours(3);
            }
            var timer = new LiveTimer()
            {
                TimeSpan = timeSpan,
                Title = model.Title,
            };



            await _context.Timers.AddAsync(timer);
            await _context.SaveChangesAsync();
            return Ok(new { time = timeSpan.TotalSeconds, id = timer.Id });

        }
        [HttpPost("/Main/SetTimerSound/{id}")]
        public async Task<IActionResult> SetTimerSound(IFormFile file, int id)
        {
            var sound = await CreateSound(file);
            var timer = _context.Timers.First(x => x.Id == id);
            timer.Sound = sound;
            await _context.SaveChangesAsync();
            return Ok(new { });
        }

        [HttpGet("/Main/GetTimer/{id}")]
        public async Task<IActionResult> GetTimer(int id)
        {
            var timer = await _context.Timers.Include(x => x.Sound).FirstAsync(timer => timer.Id == id);

            return Ok(timer);
        }

        public async Task<SoundFile> CreateSound(IFormFile file)
        {
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var dbFile = new SoundFile()
            {
                Filename = filename,
                RootDirectory = "uploads",
            };
            var localFilename =
                Path.Combine(_webHostEnvironment.WebRootPath, dbFile.RootDirectory, dbFile.Filename);
            using (var localFile = System.IO.File.Open(localFilename, FileMode.OpenOrCreate))
            {
                await file.CopyToAsync(localFile);
            }
            _context.Sounds.Add(dbFile);
            await _context.SaveChangesAsync();
            return dbFile;
        }
    }
}
