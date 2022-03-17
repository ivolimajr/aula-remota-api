using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Shared.Helpers.Validadores
{
    public class CpfValidador : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            string val = (string)value;
            string valor = val.Replace(".", "");

            valor = valor.Replace("-", "");

            if (valor.Length != 11) return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0]) igual = false;

            if (igual || valor == "12345678909") return false;

            int[] Numbers = new int[11];

            for (int i = 0; i < 11; i++)
                Numbers[i] = int.Parse(valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++) soma += (10 - i) * Numbers[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (Numbers[9] != 0) return false;
            }
            else if (Numbers[9] != 11 - resultado) return false;

            soma = 0;

            for (int i = 0; i < 10; i++) soma += (11 - i) * Numbers[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (Numbers[10] != 0) return false;
            }
            else if (Numbers[10] != 11 - resultado) return false;

            return true;

        }
    }
}

