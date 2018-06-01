using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Model.Enums
{
    public enum EPassoCadastro
    {
        [Description("Dados de Acesso")]
        InformacoesAcesso =0,
        [Description("Dados Pessoais")]
        InformacoesPessoais = 1,
        [Description("Dados Financeiros")]
        InformacoesFinanceiras =2,
        [Description("Dados Adicionais")]
        InformacoesAdicionais =3        
     }
}
