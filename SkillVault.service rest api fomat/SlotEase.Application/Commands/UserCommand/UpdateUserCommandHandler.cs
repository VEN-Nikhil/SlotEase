﻿using Microsoft.AspNetCore.Mvc;
using SlotEase.Application.DTO.User;
using SlotEase.Application.Interfaces.User;
using SlotEase.Domain.Entities.Users;
using SlotEase.Domain.Interfaces;
using SlotEase.Infrastructure;
using SlotEase.Infrastructure.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SlotEase.Application.Commands.UserCommand
{
    public class UpdatedUserCommandHandler : IRequestHandler<UpdatedUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUser _user;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserDetails> _userDetailsRepository;

        public UpdatedUserCommandHandler(IUnitOfWork unitOfWork, IUser user, IRepository<User, long> userRepository, IRepository<UserDetails> userDetailsRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userDetailsRepository = userDetailsRepository ?? throw new ArgumentNullException(nameof(userDetailsRepository));
        }

        public async Task<bool> Handle(UpdatedUserCommand request, CancellationToken cancellationToken)
        {
            // Check if the user exists
            var user = await CheckUserExists(request.UserUpdateDto.Id);
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            // Update user fields
            user.Email = request.UserUpdateDto.Email;
          
            user.FirstName = request.UserUpdateDto.FirstName;
            user.LastName = request.UserUpdateDto.Lastname;
            user.Gender = request.UserUpdateDto.Gender;
            user.LastModificationTime = DateTime.UtcNow;

            _userRepository.Update(user);

            // Check if user details exist
            var userDetails = await CheckUserDetailsExists(request.UserUpdateDto.Id);
            if (userDetails != null)
            {
                // Update user details
                userDetails.phoneNumber = request.UserUpdateDto.PhoneNumber;
                userDetails.LastModificationTime = DateTime.UtcNow;
                _userDetailsRepository.Update(userDetails);
            }
            _unitOfWork.SaveChanges();

            return true;
        }

        private async Task<User> CheckUserExists(long id)
        {
            return await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<UserDetails> CheckUserDetailsExists(long userId)
        {
            return await _userDetailsRepository.GetAll().FirstOrDefaultAsync(x => x.userId == userId);
        }
    }
}
