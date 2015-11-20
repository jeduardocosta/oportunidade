using MinutoSeguros.BlogFeed.API.Models;
using MinutoSeguros.BlogFeed.API.Parsers;
using MinutoSeguros.BlogFeed.Core.Exceptions;
using MinutoSeguros.BlogFeed.Core.Readers;
using System;
using System.Web.Http;

namespace MinutoSeguros.BlogFeed.API.Controllers
{
    public class TopWordsController : ApiController
    {
        private readonly IRequestParametersParser _requestParametersParser;
        private readonly IBlogFeedReader _blogFeedReader;

        public TopWordsController(IRequestParametersParser requestParametersParser, IBlogFeedReader blogFeedReader)
        {
            _requestParametersParser = requestParametersParser;
            _blogFeedReader = blogFeedReader;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var requestParameters = _requestParametersParser.Parse(Request);
                var blogFeedContents = _blogFeedReader.Read(requestParameters.FeedUrl);
                var topWordsResponse = new TopWordsResponse(requestParameters, blogFeedContents);
                return Ok(topWordsResponse);
            }
            catch (CustomErrorException customErrorException)
            {
                return InternalServerError(customErrorException);
            }
            catch (Exception)
            {
                return InternalServerError(new InternalServerErrorException());
            }
        }
    }
}