using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using AutoFixture;
using Moq;
using Accounts.Int.Application;
using Xunit;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        private Fixture _fixture;
        private Function _function;

        public Tests()
        {
            _fixture = new Fixture();
            _function = new Function();
        }

        [Fact]
        public void AuthUserByFb()
        {
            var lambdaContext = new Mock<ILambdaContext>();
            var apiContext = _fixture
                .Build<APIGatewayProxyRequest>()
                .With(x => x.Body, "{\"facebookLogin\":{\"name\":\"Andr\u00E9 Paris\",\"email\":\"andre.paris@yahoo.com.br\",\"picture\":{\"data\":{\"height\":50,\"is_silhouette\":false,\"url\":\"https:\\/\\/platform-lookaside.fbsbx.com\\/platform\\/profilepic\\/?asid=5205428666196965&height=50&width=50&ext=1619378951&hash=AeR-9nn68D1eARhi7Y4\",\"width\":50}},\"id\":\"5205428666196965\",\"accessToken\":\"EAAFQDiot3jgBABb38Xqsq5Nicpg7EWejO4EmtpE2eklcFcIbbvZCaBZCh81ZAHUFue7OWsnOc84jmZCS3FJ6M7xuwuhEMxeOrg6L6EZCEA9stZCwRmIU8uF6YbKgtGSHvgm5sFpAZB7K1hOPq8eyyC5aGiPovCoaMRXMnkp6RBSrgi5eRZCbQjAzG8aDcW8RN1QZAmy60uB20cgZDZD\",\"userID\":\"5205428666196965\",\"expiresIn\":5450,\"signedRequest\":\"De6LCmz5fqcGkObgioVPvUn3dhJiQ6LAP5wC_2vqOVM.eyJ1c2VyX2lkIjoiNTIwNTQyODY2NjE5Njk2NSIsImNvZGUiOiJBUURxUGpkcG9wb2szV1lnNDFRS1FpdlBsU0VrMkZULWYtRlZBNWdON1FoV0NxdENxaEhpVnZpQngxdS0wZ2dDUjN4b0MtbVYzelpIcTZHTURWc25DWERVbERUTWhCOGxKS3ZLY05haVQ3d2NDTHZ2aThHb0hRMUlrSS01TG5OOWxIdWgzcVhEVmhYNTVHal9aOWFiOXAwbWdVRFNpR2ZkcmZxeHI1S2VsdXJVak9tSm5ISmZ5aGMwcGVHTmg0UmQzSnVkVlRIU2xqVmdBaHM1cEUxczZMTjdBYkxvNXRrbk8zQXJ4V0NqcF9tNVBzRHZ5R29Dem9qM2VoV3ZTMkJvcE1IUUNWR0hxUzBXQkpPVFhkcU5HQmkwRWlVbHd3WUZQekY5aUdNc1JhNEVGRF9sdlp2NEZDN2xQZ01BMDlxVHJyOFpSTG9VaVBma3hWeF9PYjExSDZXTWtVRVpXcFJZSmhNTnN2YWtodDhmdWxxLUhVcTh1RDdsQ1FONmJDU016Vk0iLCJhbGdvcml0aG0iOiJITUFDLVNIQTI1NiIsImlzc3VlZF9hdCI6MTYxNjc4Njk1MH0\",\"graphDomain\":\"facebook\",\"data_access_expiration_time\":1624562950}}")
                .Create();

            var result = _function.AuthenticateAccount(apiContext, lambdaContext.Object);

        }
    }
}