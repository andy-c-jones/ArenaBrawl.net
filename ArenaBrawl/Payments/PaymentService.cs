using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace ArenaBrawl.Payments
{
    public class PaymentService
    {
        private readonly string _apiKey;
        private readonly string _successUrl;
        private readonly string _cancelUrl;

        public PaymentService(IConfiguration configuration)
        {
            _apiKey = configuration.GetValue<string>("Payments:ApiKey");
            _successUrl = configuration.GetValue<string>("Payments:SuccessUrl");
            _cancelUrl = configuration.GetValue<string>("Payments:CancelUrl");
        }

        public string CreatePaymentSession(Currency currency, int amount)
        {
            StripeConfiguration.ApiKey = _apiKey;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions> {
                    new SessionLineItemOptions {
                        Name = "Donation",
                        Description = "A donation to keep the service running",
                        Amount = amount,
                        Currency = currency.IsoCode,
                        Quantity = 1,
                    },
                },
                SuccessUrl = _successUrl,
                CancelUrl = _cancelUrl,
            };

            var service = new SessionService();
            var session = service.Create(options);
            return session.Id;
        }
    }
}
