using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FluentValidation.Results;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Validations;
using Agora.Brokerage.Cadastro.DadosAcesso.Shared.Models;
using Agora.Brokerage.Cadastro.DadosAcesso.Shared.Resources;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;

namespace Agora.Brokerage.Cadastro.DadosAcesso.API.Controllers
{
    /// <summary>
    /// Serviços de gravação de senhas
    /// </summary>
    [Route("api/cadastro/senha")]
    [Produces("application/json")]
    public class SenhaController : Controller
    {
        private readonly ISenhaServico _SenhaServico;
        private readonly IDadosAcessoServico _DadosAcessoServico;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Serviço Criar Senha
        /// </summary>
        /// <param name="senhaServico"></param>
        /// <param name="DadosAcessoServico"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public SenhaController(ISenhaServico senhaServico, IDadosAcessoServico DadosAcessoServico, IMapper mapper, ILogger<SenhaController> logger)
        {
            _SenhaServico = senhaServico;
            _DadosAcessoServico = DadosAcessoServico;
            _logger = logger;
             _mapper = mapper;
    }

        /// <summary>
        /// Criar senha 
        /// </summary>
        /// <param name="pSenha"></param>
        /// <returns>Retorna se a senha foi criada com sucesso</returns>
        /// <response code="200">Senha criada com sucesso</response> 
        /// <response code="503">Não foi possível criar a senha</response>            
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(503)]
        public async Task<IActionResult> Create([FromBody] Models.Senha pSenha)
        {
            SenhaValidator validator = new SenhaValidator();
            ValidationResult results = validator.Validate(_mapper.Map<Models.Senha, Model.Models.Senha>(pSenha));

           if (results.IsValid)
            {
                _logger.LogInformation(LoggingEvents.GetItem, $"Buscando senha pelo CPF {pSenha.Cpf}");


                var item = await _DadosAcessoServico.Read(pSenha.Cpf);

                if (item != null)
                {
                    _logger.LogInformation(LoggingEvents.ListItems, "Criando Senha de Acesso");
                    IActionResult result = Ok(await (_SenhaServico.Create(_mapper.Map <Models.Senha, Model.Models.Senha>(pSenha))));
                  
                    return result;
                }

                else
                {
                    var message = Mensagem.GetMensagem("CpfInexistente");
                    _logger.LogWarning("Error: ", message);

                    return (StatusCode(200, new
                    {
                        Codigo = message.Split('-')[0],
                        Mensagem = message.Split('-')[1]
                    }));
                }
            }
            else
            {
                var message = Mensagem.GetMensagem("SenhaInvalida");
                _logger.LogError("Senha inválida." + message);

                return StatusCode(503, new
                {
                    Codigo = message.Split('-')[0],
                    Mensagem = message.Split('-')[1]
                });
            }           
        }       
    }
}