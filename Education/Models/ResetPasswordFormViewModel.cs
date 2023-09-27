
using System.ComponentModel.DataAnnotations;

namespace Education.Models;

public class ResetPasswordFormViewModel
{
    public Guid UserId { get; set; }

    [Display(Name = "Kayıt Olurken Yazdığınız Güvenlik Sorusu")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    public string? SecurityQuestion { get; set; }

    [Display(Name = "Parola")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Parola Tekrar")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "{0} ile {1} alanı aynı olmalıdır!")]
    public string? PasswordCheck { get; set; }

}
