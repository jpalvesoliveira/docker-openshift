using System;
using System.Text.RegularExpressions;


namespace Agora.Brokerage.Cadastro.DadosAcesso.Shared.Validations
{
    public class HelperValidations
    {

        public static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }

        public static string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }

        public static bool IsValidCPF(long pCPF)
        {
            var CPF = pCPF.ToString(new string('0', 11));
            try
            {
                if (String.IsNullOrEmpty(CPF.ToString()))
                {
                    return false;
                }
                CPF = CPF.Replace("-", "");
                CPF = CPF.Replace(".", "");
                CPF = CPF.Replace("_", "");
                CPF = CPF.Replace(" ", "");

                if (CPF.Length != 11)
                {
                    return false;
                }
                // Caso coloque todos os numeros iguais
                switch (CPF)
                {
                    case "11111111111":
                        return false;
                    case "00000000000":
                        return false;
                    case "22222222222":
                        return false;
                    case "33333333333":
                        return false;
                    case "44444444444":
                        return false;
                    case "55555555555":
                        return false;
                    case "66666666666":
                        return false;
                    case "77777777777":
                        return false;
                    case "88888888888":
                        return false;
                    case "99999999999":
                        return false;
                    
                }

                int I; //utilizada nos FOR...
                string strCampo; //armazena do CPF que será utilizada para o cálculo
                string strCaracter; //armazena os digitos do CPF da direita para a esquerda
                int intNumero; //armazena o digito separado para cálculo (uma a um)
                int intMais; //armazena o digito específico multiplicado pela sua base
                long lngSoma; //armazena a soma dos digitos multiplicados pela sua base(intmais)
                double dblDivisao; //armazena a divisão dos digitos*base por 11
                long lngInteiro; //armazena inteiro da divisão
                int intResto; //armazena o resto
                int intDig1; //armazena o 1º digito verificador
                int intDig2; //armazena o 2º digito verificador
                string strConf; //armazena o digito verificador
                lngSoma = 0;
                intNumero = 0;
                intMais = 0;
                strCampo = Left(CPF, 9);

                //Inicia cálculos do 1º dígito
                for (I = 2; I <= 10; I++)
                {
                    strCaracter = Right(strCampo, I - 1);
                    intNumero = Convert.ToInt32(Left(strCaracter, 1));
                    intMais = intNumero * I;
                    lngSoma = lngSoma + intMais;
                }
                dblDivisao = lngSoma / 11;
                lngInteiro = ((long)dblDivisao) * 11;
                intResto = Convert.ToInt32(lngSoma - lngInteiro);
                if ((intResto == 0) || (intResto == 1))
                {
                    intDig1 = 0;
                }
                else
                {
                    intDig1 = 11 - intResto;
                }

                strCampo = strCampo + intDig1; //concatena o CPF com o primeiro digito verificador
                lngSoma = 0;
                intNumero = 0;
                intMais = 0;
                //Inicia cálculos do 2º dígito
                for (I = 2; I <= 11; I++)
                {
                    strCaracter = Right(strCampo, I - 1);
                    intNumero = Convert.ToInt32(Left(strCaracter, 1));
                    intMais = intNumero * I;
                    lngSoma = lngSoma + intMais;
                }
                dblDivisao = lngSoma / 11;
                lngInteiro = ((long)dblDivisao) * 11;
                intResto = Convert.ToInt32(lngSoma - lngInteiro);
                if ((intResto == 0) || (intResto == 1))
                {
                    intDig2 = 0;
                }
                else
                {
                    intDig2 = 11 - intResto;
                }
                strConf = intDig1.ToString() + intDig2.ToString();
                //Caso o CPF esteja errado dispara a mensagem
                if (strConf != Right(CPF, 2))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static bool ValidaEmail(string email)
        {
            if (email == null || email.IndexOf('\'') > 0)
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            }
        }

        public bool DataFormatoValido(DateTime data)
        {
            return !data.Equals(default(DateTime));
        }
    }
}
