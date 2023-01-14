using System;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Common.DTOs;
using Post.Query.Api.DTOs;
using Post.Query.Api.Queries;
using Post.Query.Domain.Entities;

namespace Post.Query.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostLookUpController: ControllerBase
    {
        private readonly ILogger<PostLookUpController> _logger;
        private readonly IQueryDispatcher<PostEntity> _queryDispatcher;

        public PostLookUpController(ILogger<PostLookUpController> logger, IQueryDispatcher<PostEntity> queryDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindAllPostsQuery());

                if (posts == null || !posts.Any())
                {
                    return NoContent();
                }

                var count = posts.Count;

                return Ok(new PostLookUpResponse
                {
                    Posts = posts,
                    Message = $"Successfully returned {count} post{(count > 1 ? "s" : string.Empty)}!"
                });
            }
            catch(Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to retrieve all posts!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }

        [HttpGet("byId/{postId}")]
        public async Task<ActionResult> GetByPostIdAsync(Guid postId)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostByIdQuery { Id = postId });
                if (posts == null || !posts.Any())
                {
                    return NoContent();
                }

                return Ok(new PostLookUpResponse
                {
                    Posts = posts,
                    Message = "Successfully returned post!"
                });
            }
            catch(Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to find a post by ID!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }

        [HttpGet("byAuthor/{author}")]
        public async Task<ActionResult> GetPostsByAuthorAsync(string author)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostsByAuthorQuery{ Author = author });

                if (posts == null || !posts.Any())
                {
                    return NoContent();
                }

                var count = posts.Count;

                return Ok(new PostLookUpResponse
                {
                    Posts = posts,
                    Message = $"Successfully returned {count} post{(count > 1 ? "s" : string.Empty)}!"
                });
            }
            catch(Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to find posts by author!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }

        [HttpGet("withComments")]
        public async Task<ActionResult> GetPostsWithCommentsAsync()
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostsWithCommentsQuery());

                if (posts == null || !posts.Any())
                {
                    return NoContent();
                }

                var count = posts.Count;

                return Ok(new PostLookUpResponse
                {
                    Posts = posts,
                    Message = $"Successfully returned {count} post{(count > 1 ? "s" : string.Empty)}!"
                });
            }
            catch(Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to find posts with comments!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }


        [HttpGet("withLikes/{numberOfLikes}")]
        public async Task<ActionResult> GetPostsWithLikesAsync(int numberOfLikes)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostsWithLikesQuery{NumberOfLikes = numberOfLikes});

                if (posts == null || !posts.Any())
                {
                    return NoContent();
                }

                var count = posts.Count;

                return Ok(new PostLookUpResponse
                {
                    Posts = posts,
                    Message = $"Successfully returned {count} post{(count > 1 ? "s" : string.Empty)}!"
                });
            }
            catch(Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to find posts with likes!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}
