using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Api.DiaryModule.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diary.Api.DiaryModule
{
    [ApiController]
    [Route("[controller]")]
    public class DiaryController : ControllerBase
    {
        private readonly IDiaryAppService _appService;
        private readonly ILogger<DiaryController> _logger;

        public DiaryController(ILogger<DiaryController> logger, IDiaryAppService appService)
        {
            _appService = appService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var dto = _appService.Get(id);
            if (dto == null)
                return NotFound();
            return Ok(dto);
        }

        [HttpGet]
        public IEnumerable<DiaryDto> GetAll()
        {
            return _appService.GetAll();
        }

        [HttpPost]
        public int Post(DiaryDto entry)
        {
            return _appService.Create(entry);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _appService.Delete(id);
        }

        [HttpGet]
        [Route("Search")]
        public IEnumerable<DiaryDto> Search(string searchString)
        {
            return _appService.Search(searchString);
        }

        [HttpGet]
        [Route("Share")]
        public void Share(int id, string friend)
        {
            _appService.Share(id, friend);
        }

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

    }
}
