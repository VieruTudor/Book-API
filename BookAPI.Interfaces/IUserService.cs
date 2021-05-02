using BookAPI.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookAPI.Interfaces
{
    public interface IUserService
    {
        string Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
    }
}
