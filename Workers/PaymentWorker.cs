using ADRaffy.ENSNormalize;
using Bybit.Net.Clients;
using CryptoExchange.Entities;
using Microsoft.EntityFrameworkCore;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexTypes;
using Nethereum.Model;
using Nethereum.Web3;
using System.Diagnostics.Contracts;
using System.Net;
using System.Numerics;

namespace CryptoExchange.Workers
{
    public class PaymentWorker : BackgroundService
    {
        private readonly ApplicationContext _context;
        private readonly BybitRestClient _byBitServive;
        public PaymentWorker(ApplicationContext context)
        {
            _byBitServive = new BybitRestClient();
            _context = context;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //var credentials = new Net.Authentication.ApiCredentials("7dKosceST5rOT7TmYJ", "DC9whCp42HHirfbCIKlYOgbUVyHcQFjBwi1q");

                //_byBitServive.V5Api.SetApiCredentials(credentials);

                //var history = await _byBitServive.V5Api.Account.GetTransactionHistoryAsync();

                var payments = _context.Payments.Include(x => x.PaymentData).Where(x => x.PaymentStatus == PaymentStatus.InProgress).ToList();
                foreach (var payment in payments)
                {
                    var networkCurrency = _context.CurrencyNetworks.Include(x => x.Network).Include(x => x.Currency).First(x => x.NetworkId == payment.PaymentData!.NetworkId && x.CurrencyId == payment.PaymentData!.CurrencyId);
                    var web3 = new Web3(networkCurrency.Network!.Url);
                    decimal balance;
                    CurrencyType currencyType = networkCurrency.Currency!.Type;
                    if (currencyType == CurrencyType.Altcoin)
                    {
                        HexBigInteger hexBalance = await web3.Eth.GetBalance.SendRequestAsync(payment.PaymentData!.WalletAddress);
                        balance = Web3.Convert.FromWei(hexBalance);

                    }
                    else
                    {
                        string tokenAbi = File.ReadAllText("./tokenAbi.json").Replace("\r", "").Replace("\n", "").Replace(" ", "");

                        var contract = web3.Eth.GetContract(tokenAbi, networkCurrency.ContractAddress);
                        var function = contract.GetFunction("balanceOf");
                        double bigBalance = (double)await function.CallAsync<BigInteger>(payment.PaymentData!.WalletAddress);
                        balance = (decimal)(bigBalance / Math.Pow(10, 18));
                    }
                    if (balance >= payment.PaymentData.ToAmount)
                    {
                        payment.PaymentStatus = PaymentStatus.Succesful;
                        _context.SaveChanges();
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken).ContinueWith(x => { });
            }
        }
    }


}
