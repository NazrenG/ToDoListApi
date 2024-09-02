using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApiFormatter.Dto;
using WebApiFormatter.Entities;

namespace WebApiFormatter.Formatters
{

    public class VCardOutputFormatter : TextOutputFormatter
    {
        public VCardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var sb = new StringBuilder();
            sb.AppendLine("Id-Fullname-SeriaNo-Age-Score");

            if (context.Object is IEnumerable<UserDto> list)
            {
                foreach (var item in list)
                {
                    FormatVCard(sb, item);
                }
            }
            else if (context.Object is UserDto student)
            {
                FormatVCard(sb, student);
            }
            await response.WriteAsync(sb.ToString());
        }

        private void FormatVCard(StringBuilder sb, UserDto item)
        {
            sb.AppendLine($"{item.Id}-{item.Fullname}-{item.SeriaNo}-{item.Age}-{item.Score}");
           
        }
    }

}
