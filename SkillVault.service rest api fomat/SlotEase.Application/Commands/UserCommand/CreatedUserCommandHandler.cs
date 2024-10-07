using Microsoft.AspNetCore.Mvc;
using SlotEase.Application.DTO.User;
using SlotEase.Application.Interfaces.User;
using SlotEase.Domain.Entities.Users;
using SlotEase.Domain.Interfaces;
using SlotEase.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlotEase.Application.Commands.UserCommand
{
    public class CreatedUserCommandHandler : IRequestHandler<CreatedUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUser _user;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserDetails> _userRepository1;

        public CreatedUserCommandHandler(IUnitOfWork unitOfWork, IUser user, IRepository<User, long> userRepository, IRepository<UserDetails> userRepository1)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userRepository1 = userRepository1 ?? throw new ArgumentNullException(nameof(userRepository1));
        }

        public async Task<bool> Handle(CreatedUserCommand request, CancellationToken cancellationToken)
        {
            long result = 0;
            var data = await CheckUserExists(request.UserCreateDto.Email);

            if (data == null)
            {
                data = new User
                {
                Email = request.UserCreateDto.Email,
                Password = request.UserCreateDto.Password,
                FirstName= request.UserCreateDto.FirstName,
                LastName= request.UserCreateDto.Lastname,
                Gender= request.UserCreateDto.Gender,
                CreatorUserId=0,
                IsDeleted = false,
                IsActive = true,
                CreationTime = DateTime.UtcNow,
                LastModificationTime= DateTime.UtcNow,
                IsVerified = false,
                LastModifierUserId=0,
                
               
                };
                result = _userRepository.InsertAndGetId(data);
                var userDetails = await CheckUserDetailsExists(result);
                if (userDetails == null)
                {
                    userDetails = new UserDetails
                    {
                        userId =   result,
                        LastModificationTime = DateTime.UtcNow,
                        CreationTime = DateTime.UtcNow,

                        companyId =1,
                        phoneNumber = request.UserCreateDto.PhoneNumber.ToString() ,

                        CreatorUserId = 0,
                        IsDeleted = false,
                    };

                    _userRepository1.Insert(userDetails);

                    /*
                         UserDetails userDet = new UserDetails();
                        userDet.LastModificationTime= DateTime.UtcNow;
                        userDet.CreationTime= DateTime.UtcNow;
                        userDet.phoneNumber = request.UserCreateDto.PhoneNumber.ToString();
                        _userRepository1.Insert(userDet);
                     
                     */
                }
            }
            _unitOfWork.SaveChanges();

            return true;
        }

        private async Task<User> CheckUserExists(string email)
        {
            return  _userRepository.GetAll().Where(x => x.Email == email).FirstOrDefault();
           
        }

        private async Task<UserDetails> CheckUserDetailsExists(long userId)
        {
            return _userRepository1.GetAll().FirstOrDefault(x => x.Id == userId);
        }

    }
}
