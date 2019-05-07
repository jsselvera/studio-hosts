using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SS.DATA.EF
{
    [MetadataType(typeof(OwnerStudioMetadata))]
    public partial class OwnerStudio { }

    public class OwnerStudioMetadata
    {
        public int OwnerStudioID { get; set; }

        [Display(Name = "Studio Name")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, ErrorMessage = "* 50 Characters Max")]
        public string StudioName { get; set; }

        public string OwnerID { get; set; }

        [Display(Name = "Studio Photo")]
        [Required(ErrorMessage = "* Required")]
        public string StudioPhoto { get; set; }

        [Display(Name = "Equipment Available")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(300, ErrorMessage = "* 300 Characters Max")]
        public string StudioAbout { get; set; }

        [Display(Name = "Producer Available?")]
        public bool ProducerAvailable { get; set; }

        [Display(Name = "Date Added")]
        public System.DateTime DateAdded { get; set; }

        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(100, ErrorMessage = "* 100 Characters Max")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, ErrorMessage = "* 50 Characters Max")]
        public string City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(2, ErrorMessage = "* 2 Characters Max")]
        public string State { get; set; }

        [Display(Name = "ZipCode")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(5, ErrorMessage = "* 5 Characters Max")]
        public string ZipCode { get; set; }

        [Display(Name = "Reservation Limit")]
        [Required(ErrorMessage = "* Required")]
        public byte ReservationLimit { get; set; }

        [Display(Name = "Hourly Rate")]
        [Required(ErrorMessage = "* Required")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        public string StudioRules { get; set; }

        [Display(Name = "Embed Location")]
        [StringLength(400, ErrorMessage = "* 400 Characters Max")]
        public string MapLink { get; set; }
    }

    [MetadataType(typeof(ReservationMetadata))]
    public partial class Reservation { }

    public class ReservationMetadata
    {
        public int ReservationID { get; set; }

        [Display(Name = "Reservation Date")]
        [DataType(DataType.Date)]
        public System.DateTime ReservationDate { get; set; }

        [Display(Name = "Reservation Time")]
        //[DisplayFormat(DataFormatString = "{0:hh\\mm tt}", ApplyFormatInEditMode = true)]
        public System.DateTime ReservationTime { get; set; }

        public int OwnerStudioID { get; set; }

        public int ArtistAssetID { get; set; }
    }

    [MetadataType(typeof(ArtistDetailMetadata))]
    public partial class ArtistDetail { }

    public class ArtistDetailMetadata
    {
        public string ArtistID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, ErrorMessage = "* 50 Characters Max")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, ErrorMessage = "* 50 Characters Max")]
        public string LastName { get; set; }
    }

    [MetadataType(typeof(ArtistAssetMetadata))]
    public partial class ArtistAsset { }

    public class ArtistAssetMetadata
    {
        public int ArtistAssetID { get; set; }

        [Display(Name = "Artist Name")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, ErrorMessage = "* 50 Characters Max")]
        public string ArtistName { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, ErrorMessage = "* 50 Characters Max")]
        public string ArtistGenre { get; set; }

        [Display(Name = "Add Photo")]
        public string ArtistPhoto { get; set; }

        [Display(Name = "Artist Bio")]
        [StringLength(300, ErrorMessage = "* 300 Characters Max")]
        public string ArtistBio { get; set; }

        public string ArtistID { get; set; }

        [Display(Name = "Social Link")]
        [StringLength(300, ErrorMessage = "* 300 Characters Max")]
        public string ArtistLink { get; set; }
    }

}
