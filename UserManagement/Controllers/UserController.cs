using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace UserManagement.Controllers
{
	public class UserController : ApiController
    {
		private readonly IUserRepository repository;

		public UserController(IUserRepository userRepository)
		{
			this.repository = userRepository;
		}

		// GET: api/User
		[ResponseType(typeof(IEnumerable<User>))]
        public IHttpActionResult Get()
        {
			IEnumerable<User> users = repository.GetUsers();
			return Ok(users);
        }

		// GET: api/User/5
		[ResponseType(typeof(User))]
		public IHttpActionResult GetUser(int id)
        {
			User user = repository.GetUser(id);
			if (user == null) {
				return NotFound();
			}
			return Ok(user);
		}

        // POST: api/User
		[HttpPost]
        public HttpResponseMessage Create(int id, [FromBody]User user)
        {
			user.CreateDate = DateTime.Now;
			user.UpdateDate = DateTime.Now;
			ResultDTO result = repository.CreateUser(user);
			if (result.IsSuccess)
				return Request.CreateResponse(HttpStatusCode.Created);
			else
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("{0} - {1}", result.Error.Code, result.Error.Message));
		}

        // PUT: api/User/5
		[HttpPut]
        public HttpResponseMessage UpdateUser(int id, [FromBody]User user)
        {
			ResultDTO result = repository.UpdateUser(user);
			if (result.IsSuccess)
				return Request.CreateResponse(HttpStatusCode.OK);
			else
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("{0} - {1}", result.Error.Code, result.Error.Message));
		}

		// DELETE: api/User/5
		[HttpDelete]
		public HttpResponseMessage Delete(int id)
        {
			ResultDTO result = repository.DeleteUser(id);
			if (result.IsSuccess)
				return Request.CreateResponse(HttpStatusCode.OK);
			else
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("{0} - {1}", result.Error.Code, result.Error.Message));
		}
    }
}
