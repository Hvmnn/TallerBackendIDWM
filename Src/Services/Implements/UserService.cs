using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerBackendIDWM.Src.DTOs.User;
using TallerBackendIDWM.Src.Models;
using TallerBackendIDWM.Src.Repositories.Implements;
using TallerBackendIDWM.Src.Repositories.Interfaces;
using TallerBackendIDWM.Src.Services.Interface;

namespace TallerBackendIDWM.Src.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapperService _mapperService;
        private readonly ITokenService _tokenService;
        
        public UserService(IUserRepository userRepository, IGenderRepository genderRepository, IMapperService mapperService, IRoleRepository roleRepository, ITokenService tokenService){
            _userRepository = userRepository;
            _genderRepository = genderRepository;
            _mapperService = mapperService;
            _roleRepository = roleRepository;
            _tokenService = tokenService;
        }
        public async Task<bool> ChangeUserPassword(int id, ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetUserById(id);
            if(user == null){
                throw new Exception("El usuario no existe.");
            }

            var verifyOldPass = BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, user.Password);
            if(!verifyOldPass){
                throw new Exception("La constraseña es incorrecta.");
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string newPassHash = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword, salt);

            var result = await _userRepository.ChangePassword(id, newPassHash);
            return result;
        }

        public async Task<bool> ChangeUserState(int id, bool userState)
        {
            var user = await _userRepository.GetUserById(id);
            var role = await _roleRepository.GetRoleByName("Admin");

            if(user == null){
                throw new Exception("El usuario no existe.");
            }

            if(role == null){
                throw new Exception("Erros en el servidor.");
            }

            if(user.RoleId == role.Id){
                throw new Exception("No se puede cambiar el estado de un administrador");
            }

            var result = await _userRepository.ChangeUserState(id, userState);
            return result;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = _tokenService.GetUserIdFromToken();
            if(user != id){
                throw new UnauthorizedAccessException("No tienes permiso para eliminar esta cuenta.");
            }
            return await _userRepository.DelUser(id);
        }

        public async Task<bool> EditUser(int id, EditUserDto editUserDto)
        {
            if(editUserDto.GenderId != null && !_genderRepository.ValidateGenderId(int.Parse(editUserDto.GenderId)).Result){
                throw new Exception("El genero no es valido.");
            }

            var mappedInfoUser = _mapperService.EditUserDtoToEditUser(editUserDto);

            var result = await _userRepository.EditUser(id, mappedInfoUser);
            return result;
        }

        public async Task<IEnumerable<Gender>> GetGenders()
        {
            var genders = await _genderRepository.GetGenders();
            return genders;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var mappedUser = _mapperService.MapUsers(users);
            return mappedUser;
        }

        public async Task<IEnumerable<UserDto>> SearchUsers(string query)
        {
            var users = await _userRepository.SearchUser(query);
            var mappedUser = _mapperService.MapUsers(users);
            return mappedUser;
        }
    }
}