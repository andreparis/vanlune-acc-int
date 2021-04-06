using Accounts.Int.Application.MediatR.Base;
using Accounts.Int.Domain.Entities;
using Accounts.Int.Infraestructure.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Accounts.Int.Application.Application.MediatR.Commands.User.AuthenticateUser
{
    public class FacebookValidUserCommandHandler : AbstractRequestHandler<FacebookValidUserCommand>
    {
        private IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _logger;

        public FacebookValidUserCommandHandler(IConfiguration configuration,
           IHttpClientFactory clientFactory,
           ILogger logger)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _logger = logger;
        }

        internal override HandleResponse HandleIt(FacebookValidUserCommand request, CancellationToken cancellationToken)
        {
            var isFacebook = request.FacebookLogin != null && !string.IsNullOrEmpty(request.FacebookLogin.Email);
            var isValid = false;

            if (isFacebook) isValid = ValidFacebookToken(request.FacebookLogin.AccessToken, request.FacebookLogin.Id).Result;

            return new HandleResponse()
            {
                Content = isValid
            };
        }

        private async Task<bool> ValidFacebookToken(string token, string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            _configuration["FACEBOOK_VALIDATED"] + "?access_token=" + token);
            request.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                _logger.Info($"Response is {responseString}");

                var json = JsonConvert.DeserializeObject<FacebookResponse>(responseString);
                
                return json.Id.Equals(id);
            }
            else
                return false;
        }
    }
}
