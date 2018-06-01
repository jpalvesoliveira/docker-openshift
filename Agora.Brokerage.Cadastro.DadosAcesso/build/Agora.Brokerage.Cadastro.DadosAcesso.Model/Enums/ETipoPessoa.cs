using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

/// <summary>
/// Objetivo: Agrupar Enumerators do cadastro referentes ao passo "Dados de Acesso"
/// </summary>
namespace Agora.Brokerage.Cadastro.DadosAcesso.Model.Enums
{    public enum ETipoPessoa
    {
        [Description("Pessoa Física")]
        PF = 1,
        [Description("Pessoa Jurídica")]
        PJ = 2
    }
}
