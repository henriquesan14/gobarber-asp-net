using ApiGoBarber.DTOs;
using ApiGoBarber.Entities;
using ApiGoBarber.ExceptionUtil;
using ApiGoBarber.Extensions;
using ApiGoBarber.Repositories;
using ApiGoBarber.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BCrypt.Net;
using ApiGoBarber.Validators;
using FluentValidation.Results;
using ApiGoBarber.Repositories.Interfaces;
using System.Linq.Expressions;

namespace ApiGoBarber.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IFileRepository _fileRepository;
        private readonly IProviderCacheRepository _providerCacheRepository;
        private readonly ITokenService _tokenService;
        private readonly UserValidator _userValidator;
        private readonly UpdateUserValidator _updateUserValidator;
        private readonly CredentialsValidator _credentialsValidator;

        public UserService(IUserRepository repository, IMapper mapper, ITokenService tokenService, UserValidator userValidator,
            UpdateUserValidator updateUserValidator, CredentialsValidator credentialsValidator, IFileRepository fileRepository,
            IProviderCacheRepository providerCacheRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenService = tokenService;
            _userValidator = userValidator;
            _updateUserValidator = updateUserValidator;
            _credentialsValidator = credentialsValidator;
            _fileRepository = fileRepository;
            _providerCacheRepository = providerCacheRepository;
        }

        public async Task<IEnumerable<ProviderDTO>> GetProviders()
        {
            var providersCache = await _providerCacheRepository.GetProviders();
            if(providersCache != null)
            {
                return providersCache;
            }
            Expression<Func<User, bool>> predicate = u => u.Provider;
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = u => u.OrderBy(u => u.Name);
            IEnumerable<User> providers = await _repository.GetAsync(predicate, orderBy, "Avatar");
            IEnumerable<ProviderDTO> providersDto = _mapper.Map<List<ProviderDTO>>(providers);
            await _providerCacheRepository.UpdateProviders(providersDto);
            return providersDto;
        }

        public async Task<AuthResponseDTO> Login(UserCredentialsDTO dto)
        {
            ValidationResult result = _credentialsValidator.Validate(dto);
            if (result.Errors.Count > 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, result.Errors);
            }
            User userExist = await _repository.GetByEmail(dto.Email);
            if(userExist == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized, new
                {
                    Message = "Usuário/senha inválido(s)"
                });
            }
            bool password = BCrypt.Net.BCrypt.Verify(dto.Password, userExist.Password);
            if (!password)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized, new
                {
                    Message = "Usuário/senha inválido(s)"
                });
            }
            var token = _tokenService.GenerateToken(userExist);
            userExist.Password = null;
            return new AuthResponseDTO {
                Token = token,
                User = _mapper.Map<UserDTO>(userExist)
            };
        }

        public async Task<UserDTO> Save(UserDTO dto)
        {
            ValidationResult result = _userValidator.Validate(dto);
            if (result.Errors.Count > 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, result.Errors);
            }
            User userExist = await _repository.GetByEmail(dto.Email);
            if (userExist != null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, new { 
                    Message = "Já existe um usuário com este email"
                });
            }
            dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password, 8);
            User userCreated = await _repository.AddAsync(_mapper.Map<User>(dto));

            if (userCreated.Provider)
            {
                await _providerCacheRepository.DeleteProviders();
            }
            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task Update(UpdateUserDTO dto)
        {
            ValidationResult result = _updateUserValidator.Validate(dto);
            if (result.Errors.Count > 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, result.Errors);
            }
            User user = await _repository.GetByIdAsync(dto.Id);
            if(user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, new
                {
                    Message = "Usuário não encontrado"
                });
            }
            if(dto.Email != user.Email)
            {
                User userExist = await _repository.GetByEmail(dto.Email);
                if (userExist != null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest, new
                    {
                        Message = "Já existe um usuário com este email"
                    });
                }
            }
            if(dto.OldPassword != null && !BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.Password))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized, new
                {
                    Message = "Senha antiga não confere"
                });
            }
            User userUpdate = _mapper.Map(dto, user);
            if(userUpdate.Password != null) userUpdate.Password = BCrypt.Net.BCrypt.HashPassword(userUpdate.Password, 8);
            if (dto.AvatarId != 0)
            {
                Avatar avatar = await _fileRepository.GetByIdAsync(dto.AvatarId);
                if(avatar != null)
                {
                    userUpdate.Avatar = avatar;
                }
            }
            await _repository.UpdateAsync(userUpdate);
        }
    }
}
