using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace AVANADE.INFRASTRUCTURE.ServicesComum.EnumService
{   
    /// <summary>
    /// Contém métodos de extensão modernos e performáticos para Enums,
    /// utilizando cache para otimizar o acesso aos atributos Description.
    /// </summary>
    public static class ConverterEnumService
    {
        private static readonly ConcurrentDictionary<string, string> DescriptionCache = new();
        private static readonly ConcurrentDictionary<Type, IReadOnlyDictionary<string, object>> ValueFromDescriptionCache = new();

        /// <summary>
        /// Obtém a string do atributo [Description] de um valor de enum.
        /// Se o atributo não existir, retorna o nome do próprio valor do enum.
        /// O resultado é armazenado em cache para acessos futuros.
        /// </summary>
        /// <typeparam name="TEnum">O tipo do enum.</typeparam>
        /// <param name="enumValue">O valor do enum.</param>
        /// <returns>A descrição do enum.</returns>
        public static string GetDescription<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
        {
            string key = $"{typeof(TEnum).FullName}.{enumValue}";

            return DescriptionCache.GetOrAdd(key, _ =>
            {
                // Usa GetCustomAttribute<T> que é mais limpo e direto
                var descriptionAttribute = enumValue
                    .GetType()
                    .GetField(enumValue.ToString())
                    ?.GetCustomAttribute<DescriptionAttribute>();

                // Usa o operador de coalescência nula (??) para o fallback
                return descriptionAttribute?.Description ?? enumValue.ToString();
            });
        }

    }
}
