using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        IRepository<Aluno> repo = new AlunoRepository(""); // Colocar a connection string do banco de dados
        public struct BuscaRequest
        {
            public string Proprieadade { get; }
            public object Valor { get; }

            public BuscaRequest(string Proprieadade, object Valor)
            {
                this.Proprieadade = Proprieadade;
                this.Valor = Valor;
            }
        }

        [HttpPost]
        [HttpPost("criarAluno")]
        public IActionResult Post(Aluno aluno)
        {
            int id = repo.Inserir(aluno.Nome, aluno.Idade, aluno.Email, aluno.DataNascimento);
            if(id != 0)
            {
                return Ok(id);
            }
            return BadRequest("Dados do aluno fornecido estão incorretos");
        }


        [HttpGet]
        [HttpGet("todosAlunos")]
        public IActionResult GetAll() => Ok(repo.Listar());


        [HttpPut]
        [HttpPut("atualizarAluno")]
        public IActionResult Put(Aluno aluno)
        {
            int linhasAfetadas = repo.Atualizar(aluno.Id, aluno.Nome, aluno.Idade, aluno.Email, aluno.DataNascimento);
            if(linhasAfetadas > 0)
            {
                return Ok(aluno);
            }
            return BadRequest("Não foi possível atualizar o aluno");
        }


        [HttpDelete]
        [HttpDelete("deletarALuno")]
        public IActionResult Delete(int id)
        {
            int linhasAfetadas = repo.Excluir(id);
            if(linhasAfetadas > 0)
            {
                return Ok(id);
            }
            return BadRequest("Não foi possível deletar o aluno");
        }


        [HttpPost]
        [HttpPost("buscarAlunos")]
        public IActionResult GetAlunos(BuscaRequest busca) => Ok(repo.Buscar(busca.Proprieadade, busca.Valor));
    }
}
