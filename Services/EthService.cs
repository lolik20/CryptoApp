using CryptoExchange.Entities;
using Nethereum.RPC.Accounts;
using Nethereum.Web3;
using Nethereum.Hex.HexConvertors.Extensions;
using CryptoExchange.Interfaces;
using System.ComponentModel.DataAnnotations;
using CryptoExchange.Net.CommonObjects;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using CryptoExchange.Exceptions;

namespace CryptoExchange.Services
{
    public class EthService : IEthService
    {

        public Wallet CreateWallet(string url)
        {
            var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
            var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
            IAccount account = new Nethereum.Web3.Accounts.Account(privateKey);
            Wallet wallet = new Wallet(account.Address.ToLower(), privateKey);
            return wallet;
        }
        public async Task<decimal> GetBalance(string networkUrl, string contractAddress, string walletAddress, CurrencyType currencyType)
        {
            var web3 = new Web3(networkUrl);
            switch (currencyType)
            {
                case CurrencyType.Altcoin:
                    HexBigInteger hexBalance = await web3.Eth.GetBalance.SendRequestAsync(walletAddress);
                    return Web3.Convert.FromWei(hexBalance);
                case CurrencyType.Stable:
                    string tokenAbi = File.ReadAllText("./tokenAbi.json");

                    var contract = web3.Eth.GetContract(tokenAbi, contractAddress);
                    var function = contract.GetFunction("balanceOf");
                    double bigBalance = (double)await function.CallAsync<BigInteger>(walletAddress);
                    return (decimal)(bigBalance / Math.Pow(10, 18));
                default:
                    throw new NotFoundException("Currency type not found");
            }
        }

    }
    public record Wallet(string address, string privateKey);
}
