using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DockerComposeApi.Controllers
{
    [ApiController]
    [Route("api/dockercommand")]
    public class DockerCommandController : ControllerBase
    {
        private readonly DockerCommandContext _context;
        public DockerCommandController(ILogger<DockerCommandController> logger, DockerCommandContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public IEnumerable<DockerCommand> Get()
        {
            return _context.DockerCommand.ToList();
        }

        [HttpPost()]
        public IEnumerable<DockerCommand> Create(DockerCommandVO dockerCommandVO)
        {
            var dockerCommand = new DockerCommand
            {
                Command = dockerCommandVO.Command,
                Description = dockerCommandVO.Description,
                Example = dockerCommandVO.Example,
            };
            _context.DockerCommand.Add(dockerCommand);
            _context.SaveChanges();
            return _context.DockerCommand.ToList();
        }
    }
}
