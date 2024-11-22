using AutoMapper;
using TallerBackendIDWM.Src.DTOs.User;
using TallerBackendIDWM.Src.Models;
using TallerBackendIDWM.Src.Services.Interface;

namespace TallerBackendIDWM.Src.Services.Implements
{
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;
        public MapperService(IMapper mapper){
            _mapper = mapper;
        }
        public EditUserInfoDto EditUserDtoToEditUser(EditUserDto editUserDto)
        {
            var mappedUser = _mapper.Map<EditUserInfoDto>(editUserDto);
            return mappedUser;
        }

        public IEnumerable<UserDto> MapUsers(IEnumerable<User> users)
        {
            var mappedUser = users.Select(u => _mapper.Map<UserDto>(u)).ToList();
            return mappedUser;
        }

        public User RegisterClientDtoToUser(RegisterUserDto registerUserDto)
        {
            var mappedUser = _mapper.Map<User>(registerUserDto);
            return mappedUser;
        }

        public UserDto UserToUserDto(User user)
        {
            var mappedUser = _mapper.Map<UserDto>(user);
            return mappedUser;
        }
    }
}