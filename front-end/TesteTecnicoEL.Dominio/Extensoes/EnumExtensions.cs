using System;
using System.Collections.Generic;
using System.Linq;

namespace TesteTecnicoEL.Dominio.Extensoes
{
    static class EnumExtensions
    {
        public static IEnumerable<Enum> ObterFlagsIndividuais(this Enum value)
        {
            return ObterFlags(value, ObterValoresDosFlags(value.GetType()).ToArray());
        }

        private static IEnumerable<Enum> ObterFlags(Enum valorEnum, Enum[] valoresDisponiveis)
        {
            var bits = Convert.ToUInt64(valorEnum);
            var resultados = new List<Enum>();
            for (int i = valoresDisponiveis.Length - 1; i >= 0; i--)
            {
                ulong mask = Convert.ToUInt64(valoresDisponiveis[i]);
                if (i == 0 && mask == 0L)
                    break;
                if ((bits & mask) == mask)
                {
                    resultados.Add(valoresDisponiveis[i]);
                    bits -= mask;
                }
            }
            if (bits != 0L)
                return Enumerable.Empty<Enum>();
            if (Convert.ToUInt64(valorEnum) != 0L)
                return resultados.Reverse<Enum>();
            if (bits == Convert.ToUInt64(valorEnum) && valoresDisponiveis.Length > 0 && Convert.ToUInt64(valoresDisponiveis[0]) == 0L)
                return valoresDisponiveis.Take(1);
            return Enumerable.Empty<Enum>();
        }

        private static IEnumerable<Enum> ObterValoresDosFlags(Type tipo)
        {
            var flag = 0x1;
            foreach (var valor in Enum.GetValues(tipo).Cast<Enum>())
            {
                var bits = Convert.ToUInt64(valor);
                if (bits == 0L)
                    //yield return value;
                    continue; // skip the zero value
                while (flag < bits) flag <<= 1;
                if (flag == bits)
                    yield return valor;
            }
        }
    }
}
