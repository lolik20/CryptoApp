using ADRaffy.ENSNormalize;
using Bybit.Net.Clients;
using CryptoExchange.Entities;
using CryptoExchange.Interfaces;
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
        private readonly IEthService _ethService;
        public PaymentWorker(ApplicationContext context, IEthService ethService)
        {
            _context = context;
            _ethService = ethService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {



                var payments = await _context.Payments.Include(x => x.PaymentData).Where(x => x.PaymentStatus == PaymentStatus.InProgress).ToListAsync();
                foreach (var payment in payments)
                {
                    var networkCurrency = await _context.CurrencyNetworks.Include(x => x.Network).Include(x => x.Currency).FirstOrDefaultAsync(x => x.NetworkId == payment.PaymentData!.NetworkId && x.CurrencyId == payment.PaymentData!.CurrencyId);
                    switch (networkCurrency!.Network!.ChainProtocol)
                    {
                        case ChainProtocol.ERC20:
                            CurrencyType currencyType = networkCurrency.Currency!.Type;

                            decimal balance = await _ethService.GetBalance(networkCurrency.Network.Url, networkCurrency.ContractAddress, payment.PaymentData!.WalletAddress, currencyType);
                            if (balance >= payment.PaymentData.ToAmount)
                            {
                                payment.PaymentStatus = PaymentStatus.Succesful;
                                await _context.SaveChangesAsync();
                            }
                            break;
                        default:
                            throw new NotImplementedException();
                           //далее нужна обработка TRC
                    }
                    
                }
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken).ContinueWith(x => { });
            }
        }
    }


}
