using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using WebAPI2Demo.Models.Demo;

namespace WebAPI2Demo.Controllers.Demo
{
	/// <summary>
	///		An API to demonstrate the various ways to use validation
	/// </summary>
	[RoutePrefix("api/demo/validation")]
	public class ValidationController : ApiController
	{
		// GET api/<controller>/5
		[Route("")]
		public IHttpActionResult Get([Range(10, 9999)] int id)
		{
			return Ok(id);
		}

		// POST api/<controller>
		[Route("")]
		public IHttpActionResult Post([FromBody]PersonModel value)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok();
		}
	}
}