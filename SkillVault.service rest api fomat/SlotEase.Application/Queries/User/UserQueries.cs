
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
                            on user.Id equals userdetails.userId
                            where String.IsNullOrEmpty(userRequestDto.email) || user.Email.Trim().Contains(userRequestDto.email.ToLower().Trim())
                            select new UserDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Gender = user.Gender,
                                phoneNumber= userdetails.phoneNumber,
                                IsActive = user.IsActive,
                                IsVerified = user.IsVerified,
                                CreationTime = user.CreationTime,
                                LastSignIn = user.LastSignIn,
                        
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
                var query = (from user in _userRepository.GetAll(false)
                             join userdetails in _userDetailsRepository.GetAll(false)
                             on user.Id equals userdetails.userId
                             where user.Id == id
                             select new UserDto
                             {
                                 Id = user.Id,
                                 Email = user.Email,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Gender = user.Gender,
                                 phoneNumber = userdetails.phoneNumber,
                                 IsActive = user.IsActive,
                                 IsVerified = user.IsVerified,
                                 CreationTime = user.CreationTime,
                                 LastSignIn = user.LastSignIn
                             }).FirstOrDefault();

                // Return the user details or null if not found
                return await Task.FromResult(query);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the user.", ex);
            }
        }


    }
}
