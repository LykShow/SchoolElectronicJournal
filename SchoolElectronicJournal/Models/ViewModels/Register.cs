using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolElectronicJournal.Models.ViewModels
{
    public class Register
    {
        [Required(ErrorMessage = "Введите Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите Фамилию")]
        public string SoName { get; set; }
        [Required(ErrorMessage = "Введите Email")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите Пароль")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
