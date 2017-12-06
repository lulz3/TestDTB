using System;
using System.ComponentModel.DataAnnotations;

namespace TestDatabaze
{
    public class UserProfile
    {
        public Int32 Id { get; set; }
        public Int32 IsAdmin { get; set; }

        [Display(Name = "Jméno")]
        [StringLength(10, ErrorMessage = "Nemuze byt delsi nez 10")]
        [Required(ErrorMessage = "Vyplnte uzivatelske jmeno")]
        public String Username { get; set; }

        [Display(Name = "Heslo")]
        [StringLength(50, ErrorMessage = "Nemuze byt delsi nez 50")]
        [Required(ErrorMessage = "Vyplnte heslo")]
        public String Password { get; set; }

        [Display(Name = "Heslo Znovu")]
        [StringLength(50, ErrorMessage = "Nemuze byt delsi nez 50")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vyplnte heslo")]
        [Compare("Password", ErrorMessage = "Hesla se neshoduji")]
        public String ConfirmPassword { get; set; }

        public DateTime Date { get; set; }
    }
}