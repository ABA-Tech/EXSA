using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class PhotoIntervention
{
    public Guid IdPhoto { get; set; }

    public Guid IdIntervention { get; set; }

    public string UrlBlob { get; set; } = null!;

    public string TypePhoto { get; set; } = null!;

    public DateTime DatePrise { get; set; }

    public Guid IdUploadeur { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual Intervention IdInterventionNavigation { get; set; } = null!;

    public virtual Utilisateur IdUploadeurNavigation { get; set; } = null!;

    public virtual RefTypePhoto TypePhotoNavigation { get; set; } = null!;
}
