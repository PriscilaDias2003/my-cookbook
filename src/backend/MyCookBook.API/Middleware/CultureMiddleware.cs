using System.Globalization;

namespace MyCookBook.API.Middleware
{
    // Middleware para definir a cultura(língua da resposta da API) com base no cabeçalho Accept-Language da requisição
    public class CultureMiddleware

    {
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Método Invoke é chamado para cada requisição HTTP
        public async Task Invoke(HttpContext context)
        {
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);

            // Obter o valor do cabeçalho Accept-Language
            var requestCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            var cultureInfo = new CultureInfo("en");

            if (string.IsNullOrWhiteSpace(requestCulture) == false && supportedLanguages.Any(c => c.Name.Equals(requestCulture)))
            {
                cultureInfo = new CultureInfo(requestCulture);
            }

            // Definir a cultura atual da thread com base no cabeçalho Accept-Language
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
