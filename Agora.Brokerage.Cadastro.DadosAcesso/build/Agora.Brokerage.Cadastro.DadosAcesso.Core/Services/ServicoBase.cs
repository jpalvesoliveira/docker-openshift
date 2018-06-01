using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Core.Services
{
    public class ServicoBase<T> : IServicoBase<T> where T : class
    {
        protected IRepositorioBase<T> _repositorio;

        public ServicoBase(IRepositorioBase<T> repo)
        {
            _repositorio = repo;
        }

    }
}
