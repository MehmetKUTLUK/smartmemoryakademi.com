
using System.ComponentModel.DataAnnotations;

namespace Education.Models;

public class RegisterViewModel
{
    [Display(Name = "Ad Soyad")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Text)]
    public string? Name { get; set; }


    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Display(Name = "Telefon Numarası")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    
    public string? PhoneNumber { get; set; }

    [Display(Name = "il")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Text)]
    public string? City { get; set; }

    [Display(Name = "İlçe")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Text)]
    public string? District { get; set; }

    [Display(Name = "Parola")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
   
    [DataType(DataType.Password)]
    public string? Password { get; set; }


    [Display(Name = "Parola Tekrar")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "{0} ile {1} alanı aynı olmalıdır!")]
    public string? ConfirmPassword { get; set; }

    [Display(Name = "Aktivasyon Kodu")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
   
    public required string PromoCode { get; set; }
    [Display(Name = "Güvenlik Sorusu : En Sevdiğiniz Hayvan ?")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    public required string SecurityQuestion { get; set; }
    public string? ReturnUrl { get; set; }
}
