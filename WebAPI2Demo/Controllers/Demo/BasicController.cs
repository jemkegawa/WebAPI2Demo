using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using Swashbuckle.Swagger.Annotations;
using WebAPI2Demo.Models.Demo;

namespace WebAPI2Demo.Controllers.Demo
{
	/// <summary>
	///		This is an example API. APIs are normally named with nouns.
	/// </summary>
	[RoutePrefix("api/demo/basic")] // Custom routing prefix. 
	public class BasicController : ApiController
	{
		//TODO: Change this to a simple POCO instead
		private static List<BasicModel> _data = new List<BasicModel> { new BasicModel { Id = 1, Value = "value1" }, new BasicModel { Id = 2, Value = "value2" } };

		/// <summary>
		///		An HTTP Get is as it sounds, it retrieves resources. When there 
		///		are no parameters, the purpose is to usually retrieve all resources.
		/// </summary>
		/// <returns></returns>
		[Route("all")] // Custom route for the action
		[SwaggerResponse(System.Net.HttpStatusCode.OK, "This occurs when the request is successful and the resources have been sent back.", typeof(List<BasicModel>))]
		public IHttpActionResult Get()
		{
			// This will return an HTTP 200 with data
			return Ok(_data);
		}

		/// <summary>
		///		An HTTP Get is as it sounds, it retrieves resources. In this case
		///		since there are parameters, the retrieval will be based on the parameter
		/// </summary>
		/// <param name="id">The ID</param>
		/// <returns></returns>
		[Route("")]
		[SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "This occurs when the value is not valid.")]
		[SwaggerResponse(System.Net.HttpStatusCode.NotFound, "This occurs when the id is invalid or does not exist.")]
		[SwaggerResponse(System.Net.HttpStatusCode.OK, "This occurs when the request is successful and the resource has been sent back.", typeof(BasicModel))]
		public IHttpActionResult Get(int id)
		{
			if (id < 1)
			{
				// When validations are not met, it is best to send back an HTTP 400 to let them know why
				return BadRequest($"{nameof(id)} must be greater than 0.");
			}

			var basicModel = _data.FirstOrDefault(model => model.Id == id);

			if (basicModel != null)
			{
				return Ok(basicModel);
			}
			else
			{
				// Return not found because the URL they reached-out to does not exist
				return NotFound();
			}
		}

		/// <summary>
		///		An HTTP Post is used for creating new resources.
		/// </summary>
		/// <param name="basicModel">The basic model</param>
		/// <returns></returns>
		[Route("")]
		[SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "This occurs when the value is not valid.")]
		[SwaggerResponse(System.Net.HttpStatusCode.Conflict, "This occurs when the value already exists.")]
		[SwaggerResponse(System.Net.HttpStatusCode.Created, "This occurs when the request is successful and the resource has been created.", typeof(BasicModel))]
		public IHttpActionResult Post([FromBody]BasicModel basicModel)
		{
			if (string.IsNullOrWhiteSpace(basicModel.Value))
			{
				// Another way to handle invalid validations is by adding errors to the modelstate
				ModelState.AddModelError(nameof(basicModel.Value), "value cannot be null, empty, or all whitespace");
			}

			if (!ModelState.IsValid)
			{
				// And then if the modelstate is invalid, we tell them with an HTTP 400
				return BadRequest(ModelState);
			}

			var basicModelFromData = _data.FirstOrDefault(model => model.Value.Equals(basicModel.Value, System.StringComparison.OrdinalIgnoreCase));

			if (_data.Any(model => model.Id == basicModel.Id))
			{
				// Since Posts create new resources, when the resource already exists, it is a conflict (HTTP 409)
				return Conflict();
			}			

			_data.Add(new BasicModel { Id = basicModel.Id, Value = basicModel.Value });

			// When there is a way to retrieve the value that was created, you want to send back
			// an HTTP 201 with response header "location" to indicate how to retrieve the resource created
			return CreatedAtRoute("DemoApi", new { controller = "basic", id = basicModel.Id }, basicModel);
		}

		/// <summary>
		///		An HTTP Put allows you to update or replace a resource. PUT calls are idempotent 
		///		which basically means what you pass it is what will save.
		/// </summary>
		/// <param name="id">The ID</param>
		/// <param name="basicModel">The basic model</param>
		/// <returns></returns>
		[Route("")]
		[SwaggerResponse(System.Net.HttpStatusCode.NotFound, "This occurs when the id is invalid or does not exist.")]
		[SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "This occurs when the value is not valid.")]
		[SwaggerResponse(System.Net.HttpStatusCode.NoContent, "This occurs when the request is successful and the resource has been updated/replaced.")]
		public IHttpActionResult Put(int id, [FromBody]BasicModel basicModel)
		{
			if (id < 1 || !_data.Any(model => model.Id == id))
			{
				// URL does not exist
				return NotFound();
			}

			if (string.IsNullOrWhiteSpace(basicModel.Value))
			{
				return BadRequest($"{basicModel.Value} cannot be null, empty, or all whitespace");
			}

			BasicModel val = _data.First(model => model.Id == id);
			val.Value = basicModel.Value;

			// PUTS are idempotent, so there is no need to return any values back.
			// An HTTP 204 suffices.
			return StatusCode(System.Net.HttpStatusCode.NoContent);
		}

		/// <summary>
		///		An HTTP Delete is as it sounds, it deletes a resource
		/// </summary>
		/// <param name="id">The ID</param>
		/// <returns></returns>
		[Route("")]
		[SwaggerResponse(System.Net.HttpStatusCode.Forbidden, "This occurs when the id is less than 1 or does not exist.")]
		[SwaggerResponse(System.Net.HttpStatusCode.Gone, "This occurs if the request is valid but the resource has already been deleted.")]
		[SwaggerResponse(System.Net.HttpStatusCode.NoContent, "This occurs when the request is successful and the resource has been deleted.")]
		public IHttpActionResult Delete(int id)
		{
			if (id < 1 || !_data.Any(model => model.Id == id))
			{
				// Depending on your requirements, you may want to return an HTTP 403 instead 
				// of an HTTP 400 when validations are not meant. This approach is usually
				// favored in scenarios where there are external (non-inhouse) consumers.
				return StatusCode(System.Net.HttpStatusCode.Forbidden);
			}

			var isSuccess = _data.Remove(_data.First(model => model.Id == id));

			if (isSuccess)
			{
				// The data has been deleted, so just say it was successfull with an HTTP 204 
				return StatusCode(System.Net.HttpStatusCode.NoContent);
			}

			// The record has already been deleted (somehow) so send an HTTP 410
			return StatusCode(System.Net.HttpStatusCode.Gone);
		}

		/// <summary>
		///		An HTTP Patch is used to update a specific piece of an resource. 
		/// </summary>
		/// <param name="id">The ID</param>
		/// <param name="basicModel">The value</param>
		/// <returns></returns>
		[Route("")]
		[SwaggerResponse(System.Net.HttpStatusCode.Unauthorized, "Thou shall never patch a thing!")]
		public IHttpActionResult Patch(int id, [FromBody] BasicModel basicModel) {
			// For demo purposes, if security is implemented, you can respond with
			// an HTTP 401 to indicate they are not authorized. You can either choose
			// to include a challenge like below or not. When you do, it'll send to the
			// caller an HTTP response header key of "www-authenticate" with the value passed
			// in the constructor.
			return Unauthorized(new System.Net.Http.Headers.AuthenticationHeaderValue("Basic"));
		}
	}
}