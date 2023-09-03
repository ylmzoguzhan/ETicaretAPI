using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Ürün adı boş olamaz");
            RuleFor(p => p.Name).MaximumLength(150).MinimumLength(5).WithMessage("Ürün adı 5 ile 150 karakter arası olmalı");

            RuleFor(p => p.Stock).NotNull().NotEmpty().WithMessage("Stok bilgisi boş olamaz");
            RuleFor(p => p.Stock).Must(s => s >= 0).WithMessage("Stok bilgisi negatif olamaz");

            RuleFor(p => p.Price).NotNull().NotEmpty().WithMessage("Fiyat bilgisi boş olamaz");
            RuleFor(p => p.Price).Must(s => s > 0).WithMessage("Fiyat bilgisi negatif olamaz");
        }
    }
}
 
