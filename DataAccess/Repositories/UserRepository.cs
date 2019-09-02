using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DataAccess.Repositories
{
	public class UserRepository : IUserRepository
	{
		private UserManagementEntities db = new UserManagementEntities();

		public IEnumerable<User> GetUsers() {
			return db.User;
		}

		public User GetUser(int id)
		{
			return db.User.Where(x => x.Id.Equals(id)).FirstOrDefault();
		}

		public ResultDTO CreateUser(User user)
		{
			ResultDTO result = new ResultDTO();

			try
			{
				db.User.Add(user);
				db.SaveChanges();
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
				User tmp = db.User.Find(user.Id);

				if (tmp == null) {
					result.Error = new ErrorDTO() { Code = "", Message = "User Not Found" };
					return result;
				}

				tmp.Name = user.Name;
				tmp.LastName = user.LastName;
				tmp.Address = user.Address;
				tmp.UpdateDate = DateTime.Now;
				db.User.AddOrUpdate(tmp);
				db.SaveChanges();
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
				User user = db.User.Find(id);

				if (user == null) {
					ErrorDTO error = new ErrorDTO() { Code = "0", Message = "User Not Found" };
					result.Error = error;
					return result;
				}
				db.User.Remove(user);
				db.SaveChanges();
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
