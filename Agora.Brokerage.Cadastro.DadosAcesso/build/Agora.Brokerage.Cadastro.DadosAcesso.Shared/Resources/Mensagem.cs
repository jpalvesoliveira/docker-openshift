    using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Shared.Resources
{
    public static class Mensagem
    {

        private static ResourceManager rm = new ResourceManager("Agora.Brokerage.Cadastro.DadosAcesso.Shared.Resources.Mensagens", Assembly.GetExecutingAssembly());

        public static string GetMensagem(String name)
        {
            return rm.GetString(name);
        }
    }
}
