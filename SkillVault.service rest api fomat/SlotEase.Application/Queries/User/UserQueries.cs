
using SlotEase.Application.DTO.User;
using SlotEase.Application.Interfaces.User;
using SlotEase.Domain.Interfaces;
using SlotEase.Infrastructure.Interfaces;
using SlotEase.Domain.Entities.Users;
namespace SlotEase.Application.Queries
{
    public class UserQueries : IUser
    {
       
        private readonly IRepository<UserDetails> _userDetailsRepository;
        private readonly IRepository<User, long> _userRepository;

        public UserQueries(IRepository<UserDetails> userDetailsRepository, IRepository<User, long> userRepository)
        {
            _userDetailsRepository = userDetailsRepository ?? throw new ArgumentNullException(nameof(userDetailsRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<List<UserDto>> GetUsersAsync(UserRequestDto userRequestDto)
        {
            try
            {
                var query = (from user in _userRepository.GetAll(false)
                            join userdetails in _userDetailsRepository.GetAll(false)
                            on user.Id equals userdetails.UserId
                            where String.IsNullOrEmpty(userRequestDto.email) || userdetails.Email.ToLower().Trim().Contains(userRequestDto.email.ToLower().Trim())
                            select new UserDto
                            {
                                Id = user.Id,
                                Email = userdetails.Email,
                                LastName = userdetails.LastName,

                              
                            }).ToList();


                return await Task.FromResult(query.Skip((userRequestDto.pageNumber - 1) * userRequestDto.pageSize)
                                    .Take(userRequestDto.pageSize)
                                    .ToList());
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the users.", ex);
            }
        }



        public async Task<UserDto> GetSingleUserAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetAll()
                                                .Where(e => e.IsActive && e.Id == id)
                                                .Select(user => new UserDto
                                                {
                                                    Id = user.Id,
                                                    Email = user.Email,
                                                    Password = user.Password,
                                             

                                                })
                                                .FirstOrDefaultAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the user.", ex);
            }
        }

    }
}
