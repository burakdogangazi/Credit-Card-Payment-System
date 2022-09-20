namespace MockBanks
{
    /// <summary>
    /// Her banka ödeme sistemimimizden farklı girdiler bekleyecektir.
    /// Burada her ne kadar aynı yapılar kurulsa da bu yapılar gerçek ortamda farklılık içerir.
    /// </summary>
    public class MockVakifbank : IMockBank
    {
        public string TerminalNumber => "VKF_01";
        public string CustomerName => "ted_vakıf";
        public string CustomerPassword => "tedAbcde4321";
        public string BankName => "Vakifbank";
 
        public BankPaymentResult Pay(string terminalNumber, string customerName,
                                  string customerPassword,
                                  string cvv, string creditCardNumber, int validToDay,
                                  int validToMonth, int validToYear,
                                  decimal paymentAmount, Guid paymentProcessKey)
        {
            var validateIns = this.ValidateInstitution(terminalNumber, customerName, customerPassword);

            if (!validateIns.IsValid)
                return validateIns.ToBankPaymentResult();

            //Ödeme işlemleri başarılı kabul ediyoruz.
            //Burada daha önce cache'lenen ProcessKey'e ait verilerin doğrulanması gerekli.    
            return new BankPaymentResult
            {
                IsValid = true,
                ValidationMessage = "Ödeme başarılı bir şekilde alınmıştır."
            };
        }

        public BankValidationResult ValidateCard(string terminalNumber, string customerName,
                                  string customerPassword,
                                  string cvv, string creditCardNumber, int validToDay,
                                  int validToMonth, int validToYear,
                                  decimal paymentAmount)
        {

            var validateIns = this.ValidateInstitution(terminalNumber, customerName, customerPassword);

            if (!validateIns.IsValid)
                return validateIns;

            return creditCardNumber.ValidateCard(cvv, validToDay, validToMonth, validToYear, paymentAmount);
        }
    }
}
