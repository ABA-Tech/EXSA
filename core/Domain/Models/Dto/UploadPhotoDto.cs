using Microsoft.AspNetCore.Http;

namespace Domain.Models.Dto
{
    public class UploadPhotoDto
    {
        public IFormFile[] Files { get; set; } = null!;
        public Guid IdIntervention { get; set; }
        public DateTime DatePrise { get; set; }
        public string TypePhoto { get; set; }
    }
}
