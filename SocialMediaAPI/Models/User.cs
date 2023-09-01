using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialMediaAPI.DTOs.Users;

namespace SocialMediaAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }


        public string Password { get; set; }

        public UserStatus Status { get; set; }

        public List<User> Followed { get; set; }
        
        public List<User> Followers { get; set; }

        public User()
        {
            Followed ??= new List<User>();
            Followers ??= new List<User>();
            Status = UserStatus.Active;
        }

        
        public void DeactivateAccount()
        {
            Status = UserStatus.Inactive;
        }

        public void ActivateAccount()
        {
            Status = UserStatus.Active;
        } 

        public void DeleteAccount()
        {
            Status = UserStatus.Deleted;
        }

        public bool IsActive()
        {
            return Status == UserStatus.Active;
        }

        public void Follow(User followed)
        {
            if (Followed.Contains(followed)) throw new Exception("User already followed!");
            Followed.Add(followed);
        }
    }
}
