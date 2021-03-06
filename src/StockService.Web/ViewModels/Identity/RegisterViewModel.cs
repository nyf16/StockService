﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockService.Web.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [StringLength(30, ErrorMessage ="{0} alanı en fazla {1}, en az {2} karakter olmalıdır.",MinimumLength =5)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Parola")]
        [StringLength(20,ErrorMessage ="{0} alanı en fazla {1}, en az {2} karakter olmalıdır.",MinimumLength =6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name ="Parola Doğrulama")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Şifreler aynı değil!")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Ad")]
        [StringLength(30,ErrorMessage ="{0} alanı en fazla {1}, en az {2} karakter olmalıdır.",MinimumLength =3)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Soyad")]
        [StringLength(30,ErrorMessage ="{0} alanı en fazla {1}, en az {2} karakter olmalıdır.",MinimumLength =3)]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Departman")]
        [StringLength(10,ErrorMessage ="{0} alanı en fazla {1}, en az {2} karakter olmalıdır.",MinimumLength =4)]
        public string Department { get; set; }
        [Required]
        [Display(Name = "Sicil Numarası")]        
        public int RegisterNumber { get; set; }

    }
}
