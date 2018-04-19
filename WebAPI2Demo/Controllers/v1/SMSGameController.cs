using System;
using System.Linq;
using System.Web.Http;
using WebAPI2Demo.TheBusiness;
using WebAPI2Demo.TheData;

namespace WebAPI2Demo.Controllers.v0
{
    [RoutePrefix("api/smsgame/v1")]
    public class V1SMSGameController : ApiController
    {
        /// <summary>
		///		An HTTP Post is used for creating new resources.
		/// </summary>
		/// <param name="basicModel">The basic model</param>
		/// <returns></returns>
		[Route("startgame/{contestId:int}")]
        [HttpPost]
        public IHttpActionResult StartGame(int contestId)
        {
            var contest = SMSContest.SetContestStart(contestId, DateTime.Now);

            //send texts to all registered players!
            SMSContest.SendTexts(contest);
            
            return Ok(contest);
        }
    }
}
