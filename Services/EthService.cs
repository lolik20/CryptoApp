using CryptoExchange.Entities;
using Nethereum.RPC.Accounts;
using Nethereum.Web3;
using Nethereum.Hex.HexConvertors.Extensions;
using CryptoExchange.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Services
{
    public class EthService :IEthService
    {

        public async Task<Wallet> CreateWallet(string url)
        {
            var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
            var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
            IAccount account = new Nethereum.Web3.Accounts.Account(privateKey);
            IWeb3 web3 = new Web3(url);
            Wallet wallet = new Wallet(account.Address.ToLower(), privateKey);
            return wallet;
        }
    }
    public record Wallet (string address,string privateKey);
}
