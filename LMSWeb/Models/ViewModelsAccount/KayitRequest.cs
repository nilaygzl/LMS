using System.ComponentModel.DataAnnotations;

namespace LMSWeb.Models.ViewModels
{
    public class KayitRequest
    {
        [Required]
        public string name { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public string re_pass { get; set; }

    }
}
