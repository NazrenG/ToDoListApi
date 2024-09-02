using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApiFormatter.Dto;

namespace WebApiFormatter.Formatters
{
    public class VCardInputFormatter : TextInputFormatter
    {
        public VCardInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }


        public override async Task<InputFormatterResult> ReadRequestBodyAsync(
            InputFormatterContext context, Encoding effectiveEncoding)
        {
            var httpContext = context.HttpContext.Request;

            using var reader = new StreamReader(httpContext.Body, effectiveEncoding);
            string? content = await reader.ReadToEndAsync();


            
            var split = content.Split("-");
            await ReadLineAsync($"{split[0]}", reader, context);
            await ReadLineAsync($"-", reader, context);
            await ReadLineAsync($"{split[1]}", reader, context);
            await ReadLineAsync($"-", reader, context);
            if (split.Length == 5)
            {
                var userDto = new UserDto
                {
                    Id = int.Parse(split[0]),
                    Fullname = split[1],
                    SeriaNo = split[2],
                    Age = int.Parse(split[3]),
                    Score = int.Parse(split[4])
                };

                return await InputFormatterResult.SuccessAsync(userDto);
            }


            return await InputFormatterResult.FailureAsync();

        }

        private static async Task<string> ReadLineAsync(
            string expectedText, StreamReader reader, InputFormatterContext context
             )
        {
            var line = await reader.ReadLineAsync();

            if (line is null || !line.StartsWith(expectedText))
            {
                var errorMessage = $"Looked for '{expectedText}' and got '{line}'";

                context.ModelState.TryAddModelError(context.ModelName, errorMessage);


                throw new Exception(errorMessage);
            }

            return line;
        }
    }
}
