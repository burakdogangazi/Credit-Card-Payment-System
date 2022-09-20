namespace KrediKartiOdeme.Model
{
    public class Result
    {
        public Result()
        {
            this.ExceptionMessage = String.Empty;
            this.HasException = false;  
        }

        public Result(string exceptionMessage)
        {
            HasException = true;
            ExceptionMessage = exceptionMessage;
        }

        public Result(object entity)
        {
            this.ExceptionMessage = String.Empty;
            this.HasException = false;
            Entity = entity;
        }

        public object Entity { get; set; }
        
        public bool HasException { get; set; }
        
        public string ExceptionMessage { get; set; }
        public string PaymentMessage { get; internal set; }
    }
}
