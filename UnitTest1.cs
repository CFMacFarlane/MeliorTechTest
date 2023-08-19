using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Services;
using Melior.InterviewQuestion.Types;

namespace PaymentTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BasicBacsPayment(PaymentService paymentService)
        {
            var Request = new MakePaymentRequest()
            {
                Amount = 1,
                CreditorAccountNumber = "test1",
                DebtorAccountNumber = "test2",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.Bacs
            };

            MakePaymentResult result = paymentService.MakePayment(Request);
        }

        [TestMethod]
        public void BasicFasterPayPayment(PaymentService paymentService)
        {
            var Request = new MakePaymentRequest()
            {
                Amount = 1,
                CreditorAccountNumber = "test1",
                DebtorAccountNumber = "test2",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.FasterPayments
            };

            MakePaymentResult result = paymentService.MakePayment(Request);
        }

        [TestMethod]
        public void BasicChapsPayment(PaymentService paymentService)
        {
            var Request = new MakePaymentRequest()
            {
                Amount = 1,
                CreditorAccountNumber = "test1",
                DebtorAccountNumber = "test2",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.Chaps
            };

            MakePaymentResult result = paymentService.MakePayment(Request);
        }
    }
}