using System.IO;

namespace Example
{
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using System.Collections.Generic;

    internal class Example
    {
        private static void Main()
        {
            MyTest().Wait();
        }

        private static string _email = "mikeycdavis@gmail.com";
        private static string _emailName = "Michael Davis";

        static async Task MyTest()
        {
            try
            {
                // Retrieve the API key from the environment variables. See the project README for more info about setting this up.
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apiKey);

                // Send a Single Email using the Mail Helper
                var from = new EmailAddress("test@example.com", "Example User");
                var subject = "Hello World from the SendGrid CSharp Library Helper!";
                var to = new EmailAddress(_email, _emailName);
                var plainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!";
                var htmlContent = "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var bytes = File.ReadAllBytes("Z:\\Programming\\GitHub\\sendgrid-csharp\\ExampleNet45Project\\Michael_Davis_Technical_resume_2019.pdf");
                var base64Attachment = Convert.ToBase64String(bytes);
                bytes = File.ReadAllBytes("Z:\\Programming\\GitHub\\sendgrid-csharp\\ExampleNet45Project\\Faizon.png");
                var base64Image = Convert.ToBase64String(bytes);

                var myAttachments = new List<Attachment>
                {
                    new Attachment
                    {
                        Content = base64Attachment,
                        Disposition = "inline",
                        ContentId = "Resume",
                        Filename = "Michael_Davis_Technical_resume_2019.pdf",
                        Type = "application/pdf"
                    },
                    new Attachment
                    {
                        Content = base64Image,
                        Type = "image/png",
                        Disposition = "inline",
                        ContentId = "Faizon",
                        Filename = "Faizon.png"
                    }
                };
                msg.AddAttachments(myAttachments);
                var response = await client.SendEmailAsync(msg);
                Console.WriteLine(msg.Serialize());
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Headers);
                Console.WriteLine("\n\nPress <Enter> to continue.");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        static async Task Execute()
        {
            // Retrieve the API key from the environment variables. See the project README for more info about setting this up.
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");

            var client = new SendGridClient(apiKey);

            // Send a Single Email using the Mail Helper
            var from = new EmailAddress("test@example.com", "Example User");
            var subject = "Hello World from the SendGrid CSharp Library Helper!";
            var to = new EmailAddress("test@example.com", "Example User");
            var plainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!";
            var htmlContent = "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();

            // Send a Single Email using the Mail Helper with convenience methods and initialized SendGridMessage object
            msg = new SendGridMessage()
            {
                From = new EmailAddress("test@example.com", "Example User"),
                Subject = "Hello World from the SendGrid CSharp Library Helper!",
                PlainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!",
                HtmlContent = "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>"
            };
            msg.AddTo(new EmailAddress("test@example.com", "Example User"));

            response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();

            // Send a Single Email using the Mail Helper, entirely with convenience methods
            msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("test@example.com", "Example User"));
            msg.SetSubject("Hello World from the SendGrid CSharp Library Helper!");
            msg.AddContent(MimeType.Text, "Hello, Email from the helper [SendSingleEmailAsync]!");
            msg.AddContent(MimeType.Html, "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>");
            msg.AddTo(new EmailAddress("test@example.com", "Example User"));

            response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();

            // Send a Single Email Without the Mail Helper
            string data = @"{
              'personalizations': [
                {
                  'to': [
                    {
                      'email': 'test@example.com'
                    }
                  ],
                  'subject': 'Hello World from the SendGrid C# Library!'
                }
              ],
              'from': {
                'email': 'test@example.com'
              },
              'content': [
                {
                  'type': 'text/plain',
                  'value': 'Hello, Email!'
                }
              ]
            }";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            response = await client.RequestAsync(SendGridClient.Method.POST,
                                                 json.ToString(),
                                                 urlPath: "mail/send");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();

            // GET Collection
            string queryParams = @"{
                'limit': 100
            }";
            response = await client.RequestAsync(method: SendGridClient.Method.GET,
                                                          urlPath: "asm/groups",
                                                          queryParams: queryParams);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue to POST.");
            Console.ReadLine();

            // POST
            string requestBody = @"{
              'description': 'Suggestions for products our users might like.', 
              'is_default': false, 
              'name': 'Magic Products'
            }";
            json = JsonConvert.DeserializeObject<object>(requestBody);
            response = await client.RequestAsync(method: SendGridClient.Method.POST,
                                                 urlPath: "asm/groups",
                                                 requestBody: json.ToString());
            var ds_response = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue to GET single.");
            Console.ReadLine();

            if (ds_response != null && ds_response.ContainsKey("id"))
            {
                string group_id = ds_response["id"].ToString();

                // GET Single
                response = await client.RequestAsync(method: SendGridClient.Method.GET,
                    urlPath: string.Format("asm/groups/{0}", group_id));
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Body.ReadAsStringAsync().Result);
                Console.WriteLine(response.Headers);
                Console.WriteLine("\n\nPress <Enter> to continue to PATCH.");
                Console.ReadLine();


                // PATCH
                requestBody = @"{
                    'name': 'Cool Magic Products'
                }";
                json = JsonConvert.DeserializeObject<object>(requestBody);

                response = await client.RequestAsync(method: SendGridClient.Method.PATCH,
                    urlPath: string.Format("asm/groups/{0}", group_id),
                    requestBody: json.ToString());
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Body.ReadAsStringAsync().Result);
                Console.WriteLine(response.Headers.ToString());

                Console.WriteLine("\n\nPress <Enter> to continue to PUT.");
                Console.ReadLine();

                // DELETE
                response = await client.RequestAsync(method: SendGridClient.Method.DELETE,
                    urlPath: string.Format("asm/groups/{0}", group_id));
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Headers.ToString());
                Console.WriteLine("\n\nPress <Enter> to DELETE and exit.");
                Console.ReadLine();
            }
        }
    }
}
