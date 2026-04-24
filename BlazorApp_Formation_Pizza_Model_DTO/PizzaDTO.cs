using System.ComponentModel.DataAnnotations;

namespace BlazorApp_Formation_Pizza_Model_DTO
{
    public class PizzaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom de la pizza est obligatoire."), StringLength(25,MinimumLength =5,ErrorMessage ="Le nom de la pizza doit contenir entre 5 et 25 caractères.")]
        public required string NomPizza { get; set; }

        [Required(ErrorMessage = "La description de la pizza est obligatoire."), StringLength(100, MinimumLength = 10, ErrorMessage = "La description de la pizza doit contenir entre 10 et 100 caractères.")]
        public required string DescriptionPizza { get; set; }
        [Required(ErrorMessage = "Le prix de la pizza est obligatoire."), RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Le prix doit être un nombre valide.")]
        [Range(0, 250, ErrorMessage = "Le prix doit être compris entre 0 et 250.")]
        public required double PrixPizza { get; set; }
        [Required(ErrorMessage = "L'image de la pizza est obligatoire.")]
        public required string ImagePizza { get; set; }
    }
}
