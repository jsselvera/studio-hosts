using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace SS.UI.MVC.Models
{
    public class ContactViewModel
    {
       
            [Required(ErrorMessage = "* Name is required")]
            public string Name { get; set; }

            [Required(ErrorMessage = "* Email is required")]
            [EmailAddress(ErrorMessage = "* Please enter a valid email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "* Subject is required")]
            public string Subject { get; set; }

            [Required(ErrorMessage = "* Message is required")]
            [StringLength(240, ErrorMessage = "* Max Character Limit: 240")]
            public string Message { get; set; }
        
    }
}