namespace DATN.Core.ViewModel.CategoryVM
{
    public class CategoryTranslationVM
    {
        public int CategoryTranslationId { get; set; }
        public string TranslatedName { get; set; }

        public int CategoryId { get; set; }
        public CategoryVM Category { get; set; }
        public int LanguageId { get; set; }
        public LanguageVM Language { get; set; }
    }
}
