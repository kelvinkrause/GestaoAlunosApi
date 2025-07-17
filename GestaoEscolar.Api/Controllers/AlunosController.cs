using GestaoAlunos.Exception;
using GestaoEscolar.Application.UseCases.AtualizarDadosAlunoEMatriculas;
using GestaoEscolar.Application.UseCases.CadastrarAlunoEMatriculas;
using GestaoEscolar.Application.UseCases.ListarAlunosCadastrados;
using GestaoEscolar.Application.UseCases.ListarMatriculasPorAluno;
using GestaoEscolar.Application.UseCases.RemoverAluno;
using GestaoEscolar.Communication.Requests;
using GestaoEscolar.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GestaoEscolar.Api.Controllers
{
    [Route("api/alunos")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly CadastrarAlunoEMatriculasUseCase _cadastrarAluno;
        private readonly ListarAlunosCadastradosUseCase _listarAlunosCadastrados;
        private readonly ListarMatriculasPorAlunoUseCase _listarMatriculasPorAlunos;
        private readonly AtualizaAlunoEMatriculasUseCase _atualizaAlunosEMatriculas;
        private readonly RemoverAlunoEMatriculasUseCase _removerAlunoEMatriculas;

        public AlunosController(
            CadastrarAlunoEMatriculasUseCase cadastrarAlunoUseCase,
            ListarAlunosCadastradosUseCase listarAlunosCadastrados,
            ListarMatriculasPorAlunoUseCase listarMatriculasPorAluno,
            AtualizaAlunoEMatriculasUseCase atualizaAlunosEMatriculas,
            RemoverAlunoEMatriculasUseCase removerAlunoEMatriculas)
        {
            _cadastrarAluno = cadastrarAlunoUseCase;
            _listarAlunosCadastrados = listarAlunosCadastrados;
            _listarMatriculasPorAlunos = listarMatriculasPorAluno;
            _atualizaAlunosEMatriculas = atualizaAlunosEMatriculas;
            _removerAlunoEMatriculas = removerAlunoEMatriculas;
        }

        [HttpPost("alunos")]
        [ProducesResponseType(typeof(ResponseAlunoEMatriculas), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CadastrarAlunos([FromBody] RequestCadastrarAluno request)
        {
            try
            {
                var aluno = await _cadastrarAluno.Execute(request);
                return Created(string.Empty, aluno);
            }
            catch (GestaoEscolarException exception)
            {
                return BadRequest(new ResponseErrorMessage
                {
                    Errors = exception.GetErrorMessage()
                });
            }
        }

        [ProducesResponseType(typeof(IEnumerable<ResponseAluno>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessage), StatusCodes.Status400BadRequest)]
        [HttpGet("alunos")]
        public async Task<ActionResult<List<ResponseAluno>>> ListarTodos()
        {
            try
            {
                var alunos = await _listarAlunosCadastrados.Execute();
                return Ok(alunos);
            }
            catch (GestaoEscolarException exception)
            {
                return BadRequest(new ResponseErrorMessage
                {
                    Errors = exception.GetErrorMessage()
                });
            }
        }

        [ProducesResponseType(typeof(IEnumerable<ResponseMatricula>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessage), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}/matriculas")]
        public async Task<ActionResult<List<ResponseMatricula>>> ListarTodasMatriculasPorAluno(Guid id)
        {
            try
            {
                var matriculas = await _listarMatriculasPorAlunos.Execute(id);
                return Ok(matriculas);
            }
            catch (GestaoEscolarException exception)
            {
                return BadRequest(new ResponseErrorMessage
                {
                    Errors = exception.GetErrorMessage()
                });
            }
        }


        [ProducesResponseType(typeof(ResponseAlunoEMatriculas), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessage), StatusCodes.Status400BadRequest)]
        [HttpPut("alunos")]
        public async Task<ActionResult> AtualizarAlunoEMatriculas([FromBody] RequestAtualizaAluno request)
        {
            try
            {
                var aluno = await _atualizaAlunosEMatriculas.Execute(request);
                return Ok(aluno);
            }
            catch (GestaoEscolarException exception)
            {
                return BadRequest(new ResponseErrorMessage
                {
                    Errors = exception.GetErrorMessage()
                });
            } 
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessage), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverAlunosEMatriculas(Guid id)
        {
            try
            {
                await _removerAlunoEMatriculas.Execute(id);
                return Ok("Aluno deletado.");
            }
            catch (GestaoEscolarException exception)
            {
                return BadRequest(new ResponseErrorMessage
                {
                    Errors = exception.GetErrorMessage()
                });
            }
        }
    }
}
