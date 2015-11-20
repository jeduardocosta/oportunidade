using MinutoSeguros.BlogFeed.API.Models;
using MinutoSeguros.BlogFeed.API.Parsers;
using MinutoSeguros.BlogFeed.Core.Exceptions;
using MinutoSeguros.BlogFeed.Core.Readers;
using System;
using System.Web.Http;

namespace MinutoSeguros.BlogFeed.API.Controllers
{
    public class PostsController : ApiController
    {
        private readonly IRequestParametersParser _requestParametersParser;
        private readonly IBlogFeedReader _blogFeedReader;

        public PostsController(IRequestParametersParser requestParametersParser, IBlogFeedReader blogFeedReader)
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
                var postResponse = new PostResponse(requestParameters, blogFeedContents);
                return Ok(postResponse);
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