using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace WebAPI2Demo.App_Start
{
	/// <summary>
	///		You can have multiple of these registered
	/// </summary>
	public class GlobalExceptionLogger : ExceptionLogger
	{
		/// <summary>
		///		Used for logging exceptions synchronously
		/// </summary>
		/// <param name="context"></param>
		public override void Log(ExceptionLoggerContext context)
		{
			base.Log(context);
		}

		/// <summary>
		///		Use for logging exceptions asynchronously
		/// </summary>
		/// <param name="context"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
		{
			return base.LogAsync(context, cancellationToken);
		}

		/// <summary>
		///		Can use to add own custom logic for whether an exception should be logged
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool ShouldLog(ExceptionLoggerContext context)
		{
			return base.ShouldLog(context);
		}
	}
}