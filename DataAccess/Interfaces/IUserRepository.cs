using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
	public interface IUserRepository
	{
		IEnumerable<User> GetUsers();
		User GetUser(int id);
		ResultDTO CreateUser(User user);
		ResultDTO UpdateUser(User user);
		ResultDTO DeleteUser(int id);
	}
}
