using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace WebAPI2Demo.App_Start
{
	/// <summary>
	///		There can only be one of these per API
	/// </summary>
	public class GlobalExceptionHandler : ExceptionHandler
	{
		/// <summary>
		///		Used for handling exceptions synchronously
		/// </summary>
		/// <param name="context">Provides informaiton about the exception</param>
		public override void Handle(ExceptionHandlerContext context)
		{
			// This is the exception thrown
			//context.Exception

			// This contains addtional metadata about the exception (i.e., controller and action)
			//context.ExceptionContext;

			// This contains information about the request (i.e., content, headers, http method)
			//context.Request;

			// This contains additional metadata of the request (i.e., security, routing, configuration)
			//context.RequestContext;
			
			// You'll update this to return a customized response
			//context.Result;

			base.Handle(context);
		}

		/// <summary>
		///		Used for handling exceptions asynchronously
		/// </summary>
		/// <param name="context"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			return base.HandleAsync(context, cancellationToken);
		}

		/// <summary>
		///		Can use to add own custom logic for whether an exception should be handled
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool ShouldHandle(ExceptionHandlerContext context)
		{
			return base.ShouldHandle(context);
		}
	}
}