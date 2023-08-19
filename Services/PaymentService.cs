using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Types;
using System.Configuration;

namespace Melior.InterviewQuestion.Services
{
    public class PaymentService : IPaymentService
    {
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

            Account account = null;

            if (dataStoreType == "Backup")
            {
                var accountDataStore = new BackupAccountDataStore();
                account = accountDataStore.GetAccount(request.DebtorAccountNumber);
            }
            else
            {
                var accountDataStore = new AccountDataStore();
                account = accountDataStore.GetAccount(request.DebtorAccountNumber);
            }

            var result = new MakePaymentResult();
            result.Success = true;

            //check if account exists and payment scheme is allowed
            if (account == null || !account.AllowedPaymentSchemes.HasFlag(request.PaymentScheme))
            {
                result.Success = false;
            }
            //then check specifics for faster payments and chaps
            else if (request.PaymentScheme == PaymentScheme.FasterPayments && account.Balance < request.Amount)
            {
                result.Success = false;
            }
            else if (request.PaymentScheme == PaymentScheme.Chaps && account.Status != AccountStatus.Live)
            {
                result.Success = false;
            }

            if (result.Success)
            {
                account.Balance -= request.Amount;

                if (dataStoreType == "Backup")
                {
                    var accountDataStore = new BackupAccountDataStore();
                    accountDataStore.UpdateAccount(account);
                }
                else
                {
                    var accountDataStore = new AccountDataStore();
                    accountDataStore.UpdateAccount(account);
                }
            }

            return result;
        }
    }
}
