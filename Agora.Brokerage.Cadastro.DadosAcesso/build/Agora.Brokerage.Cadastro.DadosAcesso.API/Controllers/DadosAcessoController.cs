using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FluentValidation.Results;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Validations;
using Agora.Brokerage.Cadastro.DadosAcesso.Shared.Resources;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;
using Agora.Brokerage.Cadastro.DadosAcesso.Shared.Models;

namespace Agora.Brokerage.Cadastro.DadosAcesso.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/cadastro/dadosAcesso")]
    public class DadosAcessoController : Controller
    {
        private readonly IDadosAcessoServico _DadosAcessoServico;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        /// <summary>
        /// Servi�o Dados de Acesso
        /// </summary>
        /// <param name="DadosAcessoServico"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public DadosAcessoController(IDadosAcessoServico DadosAcessoServico, IMapper mapper, ILogger<DadosAcessoController> logger)
        {
            _DadosAcessoServico = DadosAcessoServico;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Salva os dados acesso inclu�dos pelo usu�rio
        /// </summary>       
        /// <param name="pDadosAcesso">Dados acesso inclu�dos pelo usu�rio</param>
        /// <returns>Retorna os dados acesso criados</returns>
        /// <response code="200">Dados acesso criado com sucesso OU dados incorretos</response> 
        /// <response code="503">Dados acesso n�o informados</response>            
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(503)]
        public async Task<IActionResult> Create([FromBody] Models.DadosAcessoPost pDadosAcesso)
        {
            DadosAcessoModelValidator validator = new DadosAcessoModelValidator();
            ValidationResult results = validator.Validate(_mapper.Map<Models.DadosAcessoPost, Model.Models.DadosAcesso>(pDadosAcesso));

            if (results.IsValid)
            {
                _logger.LogInformation(LoggingEvents.GetItem, $"Listando os Dados de Acesso pelo CPF {pDadosAcesso.CpfCnpj}");
                var dadosAcesso = await _mapper.Map<Task<Model.Models.DadosAcesso >, Task <Models.DadosAcessoPost>>(_DadosAcessoServico.Read(pDadosAcesso.CpfCnpj));
                if (dadosAcesso == null)
                {
                    if (Task.Run(() => _DadosAcessoServico.Create(_mapper.Map<Models.DadosAcessoPost, Model.Models.DadosAcesso>(pDadosAcesso))).Result)
                    {
                        _logger.LogInformation(LoggingEvents.InsertItem, $"CPF {pDadosAcesso.CpfCnpj} criado com sucesso");

                        var message = Mensagem.GetMensagem("DadosAcessoCriados");

                        return (StatusCode(200, new
                        {
                            Mensagem = message.Split('-')[1]
                        }));
                    } else
                    {
                        var message = results.Errors.Select(e => e.ErrorMessage).FirstOrDefault();
                        _logger.LogError("Dados de acesso inv�lidos." + message);

                        return StatusCode(503, new
                        {
                            Codigo = message.Split('-')[0],
                            Mensagem = message.Split('-')[1]
                        });

                    }
                }
                else if (dadosAcesso != null)
                {
                    //J� existe s� pode atualizar o Assessor
                    IActionResult result = Ok(await _DadosAcessoServico.Update(_mapper.Map<Models.DadosAcessoPost, Model.Models.DadosAcesso>(pDadosAcesso)));
                    return StatusCode(200, new
                    {
                        Mensagem = "Dados atualizados com sucesso!"
                    });
                }
                else
                {
                    _logger.LogInformation(LoggingEvents.InsertItem, $"Erro ao tentar criar os dados acesso do CPF {pDadosAcesso.CpfCnpj}");

                    var message = Mensagem.GetMensagem("ErroCriarDadosAcesso");

                    return StatusCode(503, new
                    {
                        Codigo = message.Split('-')[0],
                        Mensagem = message.Split('-')[1]
                    });
                }
            }
            else
            {
                var message = results.Errors.Select(e => e.ErrorMessage).FirstOrDefault();
                _logger.LogError("Dados de acesso inv�lidos." + message);

                return StatusCode(503, new
                {
                    Codigo = message.Split('-')[0],
                    Mensagem = message.Split('-')[1]
                });
            }
        }


        /// <summary>
        /// Buscar dados de acesso pelo CPF 
        /// </summary>
        /// <param name="pCPF"></param>
        /// <returns>Retorna os dados acesso criados</returns>
        /// <response code="200">Cpf encontrado na base de dados OU Cpf incorreto</response> 
        /// <response code="503">Cpf n�o consta na base de dados</response>  
        [HttpGet]
        [Route("{pCPF}")]
        [ProducesResponseType(typeof(Models.DadosAcesso), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(503)]
        public async Task<IActionResult> Read(long pCPF)
        {
            if (pCPF == 0)
            {
                var message = Mensagem.GetMensagem("CpfNuloOuVazio");
                _logger.LogError("Dados de acesso inv�lidos." + message);

                return StatusCode(503, new
                {
                    Codigo = message.Split('-')[0],
                    Mensagem = message.Split('-')[1]
                });
            }
            else
            {
                _logger.LogInformation(LoggingEvents.GetItem, $"Listando os Dados de Acesso pelo CPF {pCPF}");
                var dadosAcesso = await _mapper.Map <Task<Model.Models.DadosAcesso >, Task <Models.DadosAcesso>>(_DadosAcessoServico.Read(pCPF));
                if (dadosAcesso == null)
                {
                    _logger.LogWarning(LoggingEvents.GetItemNotFound, $"CPF {pCPF} n�o encontrado");
                    return StatusCode(404);
                }
                return new ObjectResult(dadosAcesso);
            }

        }

        /// <summary>
        /// Atualiza��o dos dados de acesso
        /// </summary>
        /// <param name="pDadosAcesso"></param>
        /// <returns></returns>
        /// <response code="200">Dados de acesso alterados com sucesso.</response> 
        /// <response code="503">CPF n�o consta na base de dados</response>    
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(503)]
        public async Task<IActionResult> Update([FromBody]Models.DadosAcessoPut pDadosAcesso)
        {
            DadosAcessoModelValidator validator = new DadosAcessoModelValidator();
            var dadosAcesso = _mapper.Map<Models.DadosAcessoPut, Model.Models.DadosAcesso>(pDadosAcesso);

            if (dadosAcesso.CpfCnpj != 0)
            {
                _logger.LogInformation(LoggingEvents.GetItem, $"Buscando Dados de Acesso pelo CPF {pDadosAcesso.CpfCnpj}");

                var item = await _DadosAcessoServico.Read(pDadosAcesso.CpfCnpj);

                if (item == null)//QUERY RETORNOU NULA, N�O H� O CPF NA BASE DE DADOS
                {
                    var message = Mensagem.GetMensagem("CpfInexistente");
                    _logger.LogWarning(message);

                    return (Ok(new
                    {
                        Codigo = message.Split('-')[0],
                        Mensagem = message.Split('-')[1]
                    }));
                }
                else//DADOS ATUALIZADOS
                {
                    //var dadosAcesso = await _mapper.Map<Task<IEnumerable<DadosAcesso>>, Task<IEnumerable<DadosAcessoViewModel>>>(_DadosAcessoServico.Update(pDadosAcesso));
                    IActionResult result = Ok(await _DadosAcessoServico.Update(dadosAcesso));
                    return StatusCode(200, new
                                            {
                                                Mensagem = "Dados atualizados com sucesso!"
                                            });
                };
            }
            else//MODELO FORA DO FORMATO ESPERADO
            {
                return StatusCode(503, new
                                        {
                                            Mensagem = "CPF CNPJ Inv�lido"
                                        });
            }
        }

        /// <summary>
        /// Excluir Dados de Acesso
        /// </summary>
        /// <param name="pCPF"></param>
        /// <returns>Retorna se foi dados de acesso foram desativados</returns>
        /// <response code="200">Dados de acesso desativado com sucesso</response> 
        /// <response code="503">N�o foi poss�vel localizar dados de acesso</response>            
        [HttpDelete]
        [Route("{pCPF}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(503)]
        public async Task<IActionResult> Delete(long pCPF)
        {
            if (pCPF.Equals(null))
            {
                var message = Mensagem.GetMensagem("CpfInexistente");

                return (Ok(new
                {
                    Codigo = message.Split('-')[0],
                    Mensagem = message.Split('-')[1]
                }));
            }

            var _deletado = await _DadosAcessoServico.Delete(pCPF);

            if (_deletado)
            {
                _logger.LogInformation(LoggingEvents.ListItems, "Desativando Dados de Acesso.");
                return StatusCode(200, new
                {
                    Mensagem = "Dados desativados com sucesso!"
                });
            }
            else
            {
                var message = Mensagem.GetMensagem("DadosAcessoDesativados");
                _logger.LogInformation(message);

                return StatusCode(503, new
                {
                    Codigo = message.Split('-')[0],
                    Mensagem = message.Split('-')[1]
                });
            }
        }
    }
}
