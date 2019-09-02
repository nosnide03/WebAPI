using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
	public class MockRepository : IUserRepository
	{
		private List<User> users;

		public MockRepository()
		{
			this.users = new List<User>() {
			new User { Id= 1, Name = "Edinson", LastName = "Galicia", Address = "Perú", CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
			new User { Id = 2, Name = "Jose", LastName = "Galicia", Address = "Venezuela", CreateDate = DateTime.Now, UpdateDate = DateTime.Now }
			};
		}

		public IEnumerable<User> GetUsers()
		{
			return users;
		}

		public User GetUser(int id)
		{
			return users.Where(x => x.Id.Equals(id)).FirstOrDefault();
		}

		public ResultDTO CreateUser(User user)
		{
			ResultDTO result = new ResultDTO();

			try
			{
				int id = users.OrderByDescending(x => x.Id).First().Id;
				user.Id = id + 1;
				user.CreateDate = DateTime.Now;
				user.UpdateDate = DateTime.Now;
				result.IsSuccess = true;
			}
			catch (Exception e)
			{
				result.IsSuccess = false;
				result.Error = new ErrorDTO() { Code = e.HResult.ToString(), Message = e.Message };
			}

			return result;
		}

		public ResultDTO UpdateUser(User user)
		{
			ResultDTO result = new ResultDTO();

			try
			{
				User tmp = users.Find(x => x.Id.Equals(user.Id));

				if (tmp == null)
				{
					result.Error = new ErrorDTO() { Code = "", Message = "User Not Found" };
					return result;
				}

				tmp.Name = user.Name;
				tmp.LastName = user.LastName;
				tmp.Address = user.Address;
				tmp.UpdateDate = DateTime.Now;
				int index = users.FindIndex(x => x.Id.Equals(user.Id));
				users[index] = tmp;
				result.IsSuccess = true;
			}
			catch (Exception e)
			{
				result.IsSuccess = false;
				result.Error = new ErrorDTO() { Code = e.HResult.ToString(), Message = e.Message };
			}

			return result;
		}

		public ResultDTO DeleteUser(int id)
		{
			ResultDTO result = new ResultDTO();

			try
			{
				User user = users.Find(x => x.Id.Equals(id));

				if (user == null)
				{
					ErrorDTO error = new ErrorDTO() { Code = "0", Message = "User Not Found" };
					result.Error = error;
					return result;
				}

				users.Remove(user);

				result.IsSuccess = true;
			}
			catch (Exception e)
			{
				result.IsSuccess = false;
				result.Error = new ErrorDTO() { Code = e.HResult.ToString(), Message = e.Message };
			}

			return result;
		}
	}
}
