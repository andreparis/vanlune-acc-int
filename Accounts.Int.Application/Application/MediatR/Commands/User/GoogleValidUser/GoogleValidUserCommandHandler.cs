using Accounts.Int.Application.MediatR.Base;
using Accounts.Int.Domain.Entities;
using Accounts.Int.Infraestructure.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounts.Int.Application.Application.MediatR.Commands.User.GoogleValidUser
{
    public class GoogleValidUserCommandHandler : AbstractRequestHandler<GoogleValidUserCommand>
    {
        private IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _logger;

        public GoogleValidUserCommandHandler(IConfiguration configuration,
           IHttpClientFactory clientFactory,
           ILogger logger)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _logger = logger;
        }

        internal override HandleResponse HandleIt(GoogleValidUserCommand request, CancellationToken cancellationToken)
        {
            var isGoogle = request.GoogleLogin != null && !string.IsNullOrEmpty(request.GoogleLogin.AccessToken);
            var isValid = false;

            if (isGoogle) isValid = ValidGoogleToken(request.GoogleLogin.AccessToken).Result;

            return new HandleResponse()
            {
                Content = isValid
            };
        }

        private async Task<bool> ValidGoogleToken(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            _configuration["GOOGLE_VALIDATED"] + "?access_token=" + token);
            request.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                _logger.Info($"Response is {responseString}");

                var json = JsonConvert.DeserializeObject<GoogleResponse>(responseString);

                return json.AccessType.Equals("online") && json.VerifiedEmail;
            }
            else
                return false;
        }
    }
}
