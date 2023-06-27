using Api.Entities;
using Api.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IData _IData;
        public IConfiguration _configuration;

        public DataController(IConfiguration config, IData IData)
        {
            _IData = IData;
            _configuration = config;
        }

        // GET: api/data>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data>>> Get()
        {
            return await _IData.GetDatas();
        }

        // GET api/data/(id)
        [HttpGet("{id}")]
        public async Task<ActionResult<Data>> Get(int id)
        {
            var data = await Task.FromResult(_IData.GetData(id));
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }

        // POST api/data
        [HttpPost]
        public async Task<ActionResult<Data>> Post(Data data)
        {
            //validate
            await _IData.AddData(data);
            return await Task.FromResult(data);
        }

        // DELETE api/data/(id)
        [HttpDelete("{id}")]
        public async Task<ActionResult<Data>> Delete(int id)
        {
            var data = _IData.DeleteData(id);
            return await Task.FromResult(data);
        }
    }
}
