﻿namespace HallScheduler.Server.DataTransferObjects.Users
{
    using System.ComponentModel.DataAnnotations;

    using Data.Common.Constants;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserDetailedDTO : IMapFrom<User>
    {
        public string Id { get; set; }

        [MaxLength(ValidationConstants.FirstNameMaxLength, ErrorMessage = ValidationConstants.FirstNameMaxLengthErrorMessage)]
        [MinLength(ValidationConstants.FirstNameMinLength, ErrorMessage = ValidationConstants.FirstNameMinLengthErrorMessage)]
        public string FirstName { get; set; }

        [MaxLength(ValidationConstants.LastNameMaxLength, ErrorMessage = ValidationConstants.LastNameMaxLengthErrorMessage)]
        [MinLength(ValidationConstants.LastNameMinLength, ErrorMessage = ValidationConstants.LastNameMinLengthErrorMessage)]
        public string LastName { get; set; }

        [MaxLength(ValidationConstants.UsernameMaxLength, ErrorMessage = ValidationConstants.UsernameMaxLengthErrorMessage)]
        [MinLength(ValidationConstants.UsernameMinLength, ErrorMessage = ValidationConstants.UsernameMinLengthErrorMessage)]
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        [MinLength(ValidationConstants.EmailMinLength, ErrorMessage = ValidationConstants.EmailMinLengthErrorMessage)]
        public string Email { get; set; }

        public string FullName
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }
        }
    }
}
