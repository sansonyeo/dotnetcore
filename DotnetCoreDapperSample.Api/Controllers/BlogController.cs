using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Transactions;
using DotnetCoreDapperSample.Api.Exceptions;
using DotnetCoreDapperSample.Api.Models;
using DotnetCoreDapperSample.Api.Repositories;

namespace DotnetCoreDapperSample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogRepository _blogRepository;

        public BlogController(BlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public IEnumerable<Blog> Get()
        {
            return _blogRepository.GetList();
        }

        [HttpGet("{id}")]
        public ActionResult<Blog> Get(int id)
        {
            var blog = _blogRepository.Get(id);
            if (blog == null) throw new AppException(Messages.Msg002);
            return blog;
        }

        [HttpPost]
        public Blog Post(Blog blog)
        {
            if (_blogRepository.Get(blog.Id) != null)
            {
                throw new AppException(Messages.Msg001);
            }

            // TransactionScope を利用した実装例
            using (TransactionScope scope = new TransactionScope())
            {
                _blogRepository.Create(blog);
                scope.Complete();
            }

            // IDbTransaction を利用した実装例(를 이용한 구현 예)
            // using IDbTransaction tran = _blogRepository.BeginTransaction();
            // _blogRepository.Create(blog);
            // tran.Commit();

            return _blogRepository.Get(blog.Id);
        }
    }
}
