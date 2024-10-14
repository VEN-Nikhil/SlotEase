using SlotEase.API.Constants.ApiResponse;
using SlotEase.Application.Interfaces.User;
using SlotEase.Domain.Entities.Driver;
using SlotEase.Domain.Entities.Users;
using SlotEase.Domain.Interfaces;
using SlotEase.Infrastructure.Interfaces;
using System.Threading;

namespace SlotEase.Application.Commands.UserCommand
{
    public class CreatedUserCommandHandler : IRequestHandler<CreatedUserCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUser _user;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserDetails> _userDetailsRepository;
        private readonly IRepository<Driver> _driverRepository;

        public CreatedUserCommandHandler(IUnitOfWork unitOfWork, IUser user, IRepository<User, long> userRepository, IRepository<UserDetails> userRepository1, IRepository<Driver> driverRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userDetailsRepository = userRepository1 ?? throw new ArgumentNullException(nameof(userRepository1));
            _driverRepository = driverRepository ?? throw new ArgumentNullException(nameof(_driverRepository));
        }

        public async Task<string> Handle(CreatedUserCommand request, CancellationToken cancellationToken)
        {
            long result = 0;
            var data = await CheckUserExists(request.UserCreateDto.Email);

            if (data == null)
            {
                data = new User
                {
                    Email = request.UserCreateDto.Email,
                    Password = request.UserCreateDto.Password,
                    FirstName = request.UserCreateDto.FirstName,
                    LastName = request.UserCreateDto.Lastname,
                    Gender = request.UserCreateDto.Gender,
                    CreatorUserId = 0,
                    IsDeleted = false,
                    IsActive = true,
                    CreationTime = DateTime.UtcNow,
                    LastModificationTime = DateTime.UtcNow,
                    IsVerified = false,
                    LastModifierUserId = 0,
                    UserType = (int)request.UserCreateDto.UserType,
                };
                result = _userRepository.InsertAndGetId(data);
                var userDetails = await CheckUserDetailsExists(result);
                if (userDetails == null)
                {
                    userDetails = new UserDetails
                    {
                        userId = result,
                        LastModificationTime = DateTime.UtcNow,
                        CreationTime = DateTime.UtcNow,

                        companyId = 1,
                        phoneNumber = request.UserCreateDto.PhoneNumber.ToString(),

                        CreatorUserId = 0,
                        IsDeleted = false,
                    };

                    if (request.UserCreateDto.UserType == 4 && !string.IsNullOrWhiteSpace(request.UserCreateDto.UserType.ToString()))
                    {
                        var driver = new Driver
                        {
                            DriverId = result,
                            VendorId = request.UserCreateDto.VendorId,
                            LastModificationTime = DateTime.UtcNow,
                            CreationTime = DateTime.UtcNow,
                            CreatorUserId = 0,
                            IsDeleted = false,
                        };

                        // Insert or process the Driver object as needed
                        await _driverRepository.InsertAsync(driver);
                    }


                    await _userDetailsRepository.InsertAsync(userDetails);
                }
            }
            else
            {
                return MessageConstants.EmailAlreadyInUse;

            }
            _unitOfWork.SaveChanges();

            return MessageConstants.UserCreatedSuccess;

        }


        private async Task<User> CheckUserExists(string email)
        {
            return _userRepository.GetAll().Where(x => x.Email == email).FirstOrDefault();

        }

        private async Task<UserDetails> CheckUserDetailsExists(long userId)
        {
            return _userDetailsRepository.GetAll().FirstOrDefault(x => x.Id == userId);
        }

    }
}
