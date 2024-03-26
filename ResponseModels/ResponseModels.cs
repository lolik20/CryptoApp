namespace CryptoExchange.ResponseModels
{

    public record TopUpResponse(bool isSuccessful, string message) : BaseCommandResponse(isSuccessful, message);
    public record WithdrawResponse (bool isSuccessful,string message ): BaseCommandResponse(isSuccessful,message);
    public record ConvertResponse(bool isSuccessful, string message) : BaseCommandResponse(isSuccessful, message);
    public record CreatePaymentResponse(bool isSuccessful,string message):BaseCommandResponse(isSuccessful,message);
    public record UpdatePaymentResponse(bool isSuccessful,string message) :BaseCommandResponse(isSuccessful,message);
    public record CreateWithdrawalResponse(bool isSuccessful, string message) :BaseCommandResponse (isSuccessful,message);

}
