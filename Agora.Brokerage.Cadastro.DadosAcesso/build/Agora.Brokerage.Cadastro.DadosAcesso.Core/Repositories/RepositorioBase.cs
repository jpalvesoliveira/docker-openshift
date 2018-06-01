using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Core.Repositories
{
    public abstract class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        protected IConnectionFactory _factory;

        public RepositorioBase(IConnectionFactory factory)
        {
            _factory = factory;
        }
    }
}
