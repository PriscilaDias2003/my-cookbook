using AutoMapper;
using MyCookBook.Application.Services.AutoMapper;
using MyCookBook.Application.Services.Cryptography;
using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;
using MyCookBook.Domain.Repositories;
using MyCookBook.Domain.Repositories.User;
using MyCookBook.Exceptions;
using MyCookBook.Exceptions.ExceptionsBase;
using System.Threading.Tasks;

namespace MyCookBook.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordEncripter _passwordCrypted;
    private readonly IMapper _mapper;

    // Construtor da classe RegisterUserUseCase que injeta as dependências necessárias para o funcionamento do caso de uso.
    public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository, IUserReadOnlyRepository readOnlyRepository, IMapper mapper, PasswordEncripter passwordCrypted, IUnitOfWork unitOfWork)
    {
        // Atribuição das dependências injetadas aos campos privados da classe.
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _passwordCrypted = passwordCrypted;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        // Validar request
        await Validate(request);


        // Mapear a request em uma entidade
        var user = _mapper.Map<Domain.Entities.User>(request);

        user.Password = _passwordCrypted.Encrypt(request.Password);

        // Salvar entidade no banco de dados
        await _writeOnlyRepository.Add(user);
        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = request.Name
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {   
       var validator = new RegisterUserValidator();
       var result = validator.Validate(request);

        var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
        }

        if (result.IsValid == false)
        {
            var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessage);
        }
    }
}
    

