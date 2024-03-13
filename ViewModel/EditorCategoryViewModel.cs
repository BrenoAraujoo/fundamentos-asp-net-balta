

using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel
{
    public class EditorCategoryViewModel
    {

        [Required(ErrorMessage = "O Campo 'Name' é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O Campo 'Slug' é obrigatório")]
        public string Slug { get; set; }
    }
}
