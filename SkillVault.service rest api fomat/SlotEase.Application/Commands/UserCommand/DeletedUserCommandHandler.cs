﻿using Microsoft.AspNetCore.Mvc;
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
    public class DeletedUserCommandHandler : IRequestHandler<DeletedUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUser _user;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserDetails> _userDetailsRepository;

        public DeletedUserCommandHandler(IUnitOfWork unitOfWork, IUser user, IRepository<User, long> userRepository, IRepository<UserDetails> userDetailsRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userDetailsRepository = userDetailsRepository ?? throw new ArgumentNullException(nameof(userDetailsRepository));
        }

        public async Task<bool> Handle(DeletedUserCommand request, CancellationToken cancellationToken)
        {
           
            var data = await CheckUserExists((int)request.Id);

            if (data != null)
            {
                data = new User
                {
                    Id = (int)request.Id,
                    IsDeleted = true,
                    DeletionTime = DateTime.Now

                };
                  _userRepository.Update(data);
                var userDetails = await CheckUserDetailsExists((int)(request.Id));
                if (userDetails != null)
                {
                    userDetails = new UserDetails
                    {
                        Id = Convert.ToInt16(userDetails.userId),
                        DeletionTime=DateTime.Now,
                        IsDeleted=userDetails.IsDeleted,
                       
                    };
                    _userDetailsRepository.Update(userDetails);
                }
            }
            _unitOfWork.SaveChanges();

            return true;
        }

        private async Task<User> CheckUserExists(int id)
        {
            return  _userRepository.GetAll().Where(x => x.Id == id).FirstOrDefault();
           
        }

        private async Task<UserDetails> CheckUserDetailsExists(long userId)
        {
             
            return _userDetailsRepository.GetAll().FirstOrDefault(x => x.userId == userId);
        }

     
    }
}
