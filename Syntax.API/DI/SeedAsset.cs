using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syntax.API.Context;
using Syntax.API.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Syntax.API.DI
{
    public class SeedAsset
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SeedAsset> _logger;

        public SeedAsset(ApplicationDbContext context, ILogger<SeedAsset> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task CreateAssetClassesAsync()
        {
            var assets = new List<Asset>
            {
                new Asset
                {
                    Name = "ITAUUNIBANCO PN N1",
                    Symbol = "ITUB4",
                    Description = "Itaú Unibanco Holding S.A. é uma instituição financeira brasileira, sediada em São Paulo, que resultou da fusão do Banco Itaú e do Unibanco, em novembro de 2008.",
                    IdAssetClass = 1,
                    Grade = 8,
                    CreationDate = DateTime.Now

                },
                new Asset
                {
                    Name = "PETROBRAS PN",
                    Symbol = "PETR4",
                    Description = "Petróleo Brasileiro S.A. é uma empresa de capital aberto, cujo acionista majoritário é o Governo do Brasil, sendo, portanto, uma empresa estatal de economia mista.",
                    IdAssetClass = 1,
                    Grade = 7,
                    CreationDate = DateTime.Now

                },
                new Asset
                {
                    Name = "VALE ON",
                    Symbol = "VALE3",
                    Description = "Vale S.A. é uma mineradora multinacional brasileira e uma das maiores operadoras de logística do país. É uma das maiores empresas de mineração do mundo e também a maior produtora de minério de ferro, de pelotas e de níquel.",
                    IdAssetClass = 1,
                    Grade = 9,
                    CreationDate = DateTime.Now

                },
                new Asset
                {
                    Name = "BRADESCO PN N1",
                    Symbol = "BBDC4",
                    Description = "Banco Bradesco S.A. é um banco brasileiro e um dos maiores do país no setor financeiro privado.",
                    IdAssetClass = 1,
                    Grade = 8,
                    CreationDate = DateTime.Now

                },
                new Asset
                {
                    Name = "B3 ON",
                    Symbol = "B3SA3",
                    Description = "B3 S.A. - Brasil, Bolsa, Balcão é uma empresa brasileira de infraestrutura de mercado financeiro, resultante da fusão da Bolsa de Valores, Mercadorias & Futuros de São Paulo (BM&FBOVESPA) com a Central de Custódia e de Liquidação Financeira de Títulos (Cetip).",
                    IdAssetClass = 1,
                    Grade = 7,
                    CreationDate = DateTime.Now

                },
                new Asset
                {
                    Name = "AMBEV S/A ON",
                    Symbol = "ABEV3",
                    Description = "Ambev é uma empresa de produção de bebidas, fundada em 1999, resultante da fusão entre as empresas Antarctica e Brahma.",
                    IdAssetClass = 1,
                    Grade = 6,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "BB SEGURIDADE ON",
                    Symbol = "BBSE3",
                    Description = "BB Seguridade Participações S.A. é uma empresa holding que controla as empresas de seguros do Banco do Brasil.",
                    IdAssetClass = 1,
                    Grade = 8,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "RENOVA ENERGIA ON",
                    Symbol = "RNEW11",
                    Description = "Renova Energia é uma empresa brasileira do setor de energia renovável, com foco em energia eólica.",
                    IdAssetClass = 1,
                    Grade = 5,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "SABESP ON",
                    Symbol = "SBSP3",
                    Description = "SABESP é uma empresa brasileira de saneamento básico, responsável pelo fornecimento de água e tratamento de esgoto no estado de São Paulo.",
                    IdAssetClass = 1,
                    Grade = 6,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "CIA SIDERÚRGICA NACIONAL ON",
                    Symbol = "CSNA3",
                    Description = "Companhia Siderúrgica Nacional é uma empresa brasileira do setor de siderurgia, que produz aço e outros produtos derivados.",
                    IdAssetClass = 1,
                    Grade = 7,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "CIELO ON",
                    Symbol = "CIEL3",
                    Description = "Cielo S.A. é uma empresa brasileira de soluções de pagamentos eletrônicos.",
                    IdAssetClass = 1,
                    Grade = 6,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "ITAUSA PN",
                    Symbol = "ITSA4",
                    Description = "Itaúsa Investimentos Itaú S.A. é uma empresa holding brasileira que controla empresas como Itaú Unibanco, Duratex, Alpargatas, entre outras.",
                    IdAssetClass = 1,
                    Grade = 8,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "BRASKEM PNA",
                    Symbol = "BRKM5",
                    Description = "Braskem S.A. é uma empresa brasileira do setor petroquímico, que produz resinas termoplásticas, produtos químicos e petroquímicos.",
                    IdAssetClass = 1,
                    Grade = 6,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "ULTRAPAR ON",
                    Symbol = "UGPA3",
                    Description = "Ultrapar Participações S.A. é uma empresa brasileira do setor de distribuição de combustíveis e de produtos químicos.",
                    IdAssetClass = 1,
                    Grade = 6,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "LOCALIZA ON",
                    Symbol = "RENT3",
                    Description = "Localiza Rent a Car S.A. é uma empresa brasileira do setor de aluguel de carros.",
                    IdAssetClass = 1,
                    Grade = 7,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "MRV ON",
                    Symbol = "MRVE3",
                    Description = "MRV Engenharia e Participações S.A. é uma empresa brasileira do setor de construção civil, que atua principalmente no segmento de imóveis residenciais.",
                    IdAssetClass = 1,
                    Grade = 7,
                    CreationDate = DateTime.Now

                },
                new Asset
                {
                    Name = "HGLG11",
                    Symbol = "HGLG11",
                    Description = "O HGLG11 é um fundo imobiliário do tipo tijolo, que tem como objetivo investir em empreendimentos imobiliários. Seu portfólio é diversificado, incluindo empreendimentos corporativos, educacionais e de varejo, entre outros.",
                    IdAssetClass = 2,
                    Grade = 8,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "KNRI11",
                    Symbol = "KNRI11",
                    Description = "O KNRI11 é um fundo imobiliário que investe em empreendimentos imobiliários de diferentes tipos, como shoppings, escritórios, hotéis e galpões logísticos, entre outros. Sua estratégia de investimento é focada em empreendimentos com potencial de geração de renda recorrente.",
                    IdAssetClass = 2,
                    Grade = 7,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "BRCR11",
                    Symbol = "BRCR11",
                    Description = "O BRCR11 é um fundo imobiliário do tipo tijolo, que investe em empreendimentos imobiliários de diferentes tipos, como edifícios comerciais, galpões logísticos e shopping centers. Seu objetivo é gerar renda recorrente para os cotistas por meio da locação dos imóveis.",
                    IdAssetClass = 2,
                    Grade = 7,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "HGRE11",
                    Symbol = "HGRE11",
                    Description = "O HGRE11 é um fundo imobiliário que investe em empreendimentos imobiliários do tipo tijolo, como edifícios corporativos e galpões logísticos. Seu objetivo é gerar renda recorrente para os cotistas por meio da locação dos imóveis.",
                    IdAssetClass = 2,
                    Grade = 8,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "XPML11",
                    Symbol = "XPML11",
                    Description = "O XPML11 é um fundo imobiliário que investe em empreendimentos imobiliários de diferentes tipos, como edifícios corporativos, shoppings e galpões logísticos. Sua estratégia de investimento é focada em empreendimentos bem localizados e com alto potencial de valorização.",
                    IdAssetClass = 2,
                    Grade = 9,
                    CreationDate = DateTime.Now
                },
                new Asset
                {
                    Name = "VISC11",
                    Symbol = "VISC11",
                    Description = "O VISC11 é um fundo imobiliário que investe em empreendimentos imobiliários de diferentes tipos, como edifícios corporativos, shoppings e galpões logísticos. Sua estratégia de investimento é focada em empreendimentos bem localizados e com alto potencial de valorização.",
                    IdAssetClass = 2,
                    Grade = 8,
                    CreationDate = DateTime.Now

                },

            };

            foreach (var asset in assets)
            {
                if (!_context.Assets.Any(ac => ac.Symbol == asset.Symbol))
                {
                    _context.Assets.Add(asset);
                }
            }
            _logger.LogInformation("Asset classes were seeded.");

            return _context.SaveChangesAsync();

        }
    }
}
