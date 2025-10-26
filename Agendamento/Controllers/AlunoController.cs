using Agendamento.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IServAluno _servAluno;

        public AlunoController(IServAluno servAluno)
        {
            _servAluno = servAluno;
        }
    }
}
