using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Resources;
using System.Reflection;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;
using Agora.Brokerage.Cadastro.DadosAcesso.Shared.Models;
using Agora.Brokerage.Cadastro.DadosAcesso.Shared.Resources;
using AutoMapper;

namespace Agora.Brokerage.Cadastro.DadosAcesso.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/assessores")]
    [Produces("application/json")]
    public class AssessoresController : Controller
    {
        private readonly IAssessoresServico _AssessoresServico;
        private readonly ILogger _logger;
        private IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AssessoresServico"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public AssessoresController(IAssessoresServico AssessoresServico, IMapper mapper, ILogger<AssessoresController> logger)
        {
            _AssessoresServico = AssessoresServico;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna Lista de dados dos Assessores
        /// </summary>
        /// <returns>Retorna a lista de assessores</returns>
        /// <response code="200">Assessores listados com sucesso</response> 
        /// <response code="404">Assessores nao encontrado</response> 
        /// <response code="503">Não foi possível encontrar Assessores</response>            
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Models.Assessor>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(503)]
        public async Task<IActionResult> Read()
        {
            return await Task.Run<IActionResult>(() =>
            {
                _logger.LogInformation(LoggingEvents.GetItem, $"Buscando Assessores");
                var assessores = Task.Run<IEnumerable<Model.Models.Assessor>>(() => _AssessoresServico.Listar()).Result;
                var item = _mapper.Map<IEnumerable<Model.Models.Assessor>, IEnumerable<Models.Assessor>>(assessores);

                if (item.Count().Equals(0))
                {
                    _logger.LogWarning(Mensagem.GetMensagem("AssessorInexistente"));
                    return NotFound();
                }
                return new ObjectResult(item);
            });

        }
    }
}