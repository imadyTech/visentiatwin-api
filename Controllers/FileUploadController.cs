using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using VisentiaTwin_API.DataContexts;
using VisentiaTwin_API.DataModels;

[Route("api/[controller]")]
[ApiController]
public class FileUploadController : ControllerBase
{
    private readonly VTSystemContext _context;

    public FileUploadController(VTSystemContext context)
    {
        _context = context;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var fileId = Guid.NewGuid();
        var fileName = Path.GetFileName(file.FileName);
        var fileFormat = Path.GetExtension(file.FileName).TrimStart('.');
        var filePath = Path.Combine("Uploads", fileId + Path.GetExtension(file.FileName));

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var uploadedFile = new VTFileStorage
        {
            VTFileId = fileId,
            FileName = fileName,
            Format = fileFormat,
            Path = filePath
        };

        _context.VTFiles.Add(uploadedFile);
        await _context.SaveChangesAsync();

        return Ok(uploadedFile.VTFileId );
    }

    [HttpGet("download")]
    public async Task<IActionResult> DownloadFile(Guid id)
    {
        var uploadedFile = await _context.VTFiles.FindAsync(id);

        if (uploadedFile == null)
        {
            return NotFound("File not found.");
        }

        var memory = new MemoryStream();
        using (var stream = new FileStream(uploadedFile.Path, FileMode.Open, FileAccess.Read))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        // Create the filename in the format guid.fbx
        string downloadFileName = $"{id}.fbx";

        return File(memory, GetContentType(uploadedFile.Path), downloadFileName);
    }

    private string GetContentType(string path)
    {
        var types = GetMimeTypes();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }

    private Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
        {
            { ".txt", "text/plain" },
            { ".pdf", "application/pdf" },
            { ".doc", "application/vnd.ms-word" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { ".xls", "application/vnd.ms-excel" },
            { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            { ".png", "image/png" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".gif", "image/gif" },
            { ".csv", "text/csv" },
            { ".fbx", "application/octet-stream" }
        };
    }
}
