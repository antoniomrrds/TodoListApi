using FluentValidation;
using TodoListApi.Application.DTOs;

namespace TodoListApi.Application.Validators;

public class CreateTodoDtoValidator: AbstractValidator<CreateTodoDto>
{
    public CreateTodoDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("O título é obrigatório")
            .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A descrição é obrigatória")
            .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres");
    }
}