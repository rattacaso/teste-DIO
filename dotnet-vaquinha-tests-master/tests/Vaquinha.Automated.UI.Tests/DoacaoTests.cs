using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Vaquinha.Tests.Common.Fixtures;
using Xunit;

namespace Vaquinha.AutomatedUITests
{
	public class DoacaoTests : IDisposable, IClassFixture<DoacaoFixture>, 
                                               IClassFixture<EnderecoFixture>, 
                                               IClassFixture<CartaoCreditoFixture>
	{
		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;

		private readonly DoacaoFixture _doacaoFixture;
		private readonly EnderecoFixture _enderecoFixture;
		private readonly CartaoCreditoFixture _cartaoCreditoFixture;

		public DoacaoTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
        {
            _doacaoFixture = doacaoFixture;
            _enderecoFixture = enderecoFixture;
            _cartaoCreditoFixture = cartaoCreditoFixture;
        }
		public void Dispose()
		{
			_driverFactory.Close();
		}

		[Fact]
		public void DoacaoUI_AcessoTelaHome()
		{
			// Arrange
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			// Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("vaquinha-logo"));

			// Assert
			webElement.Displayed.Should().BeTrue(because:"logo exibido");
		}
		[Fact]
		public void DoacaoUI_CriacaoDoacao()
		{
			//Arrange
			var doacao = _doacaoFixture.DoacaoValida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			//Act

			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			//IMPLEMENTAÇÃO:
			IWebElement campoNome = null;
			campoNome = _driver.FindElement(By.Id("DadosPessoais_Nome"));
			campoNome.SendKeys(doacao.DadosPessoais.Nome);
			campoNome.Click();
			

			IWebElement campoEmail = null;
			campoEmail = _driver.FindElement(By.Id("DadosPessoais_Email"));
			campoEmail.SendKeys(doacao.DadosPessoais.Email);
			campoEmail.Click();

			IWebElement campoEndereco = null;
			campoEndereco = _driver.FindElement(By.Id("EnderecoCobranca_TextoEndereco"));
			campoEndereco.SendKeys(doacao.EnderecoCobranca.TextoEndereco);
			campoEndereco.Click();

			IWebElement campoEnderecoNumero = null;
			campoEnderecoNumero = _driver.FindElement(By.Id("EnderecoCobranca_Numero"));
			campoEnderecoNumero.SendKeys(doacao.EnderecoCobranca.Numero);
			campoEnderecoNumero.Click();

			IWebElement campoEnderecoCidade = null;
			campoEnderecoCidade = _driver.FindElement(By.Id("EnderecoCobranca_Cidade"));
			campoEnderecoCidade.SendKeys(doacao.EnderecoCobranca.Cidade);
			campoEnderecoCidade.Click();

			IWebElement campoEnderecoEstado = null;
			campoEnderecoEstado = _driver.FindElement(By.Name("EnderecoCobranca.Estado"));
			campoEnderecoEstado.SendKeys(doacao.EnderecoCobranca.Estado);
			campoEnderecoEstado.Click();

			IWebElement campoEnderecoCEP = null;
			campoEnderecoCEP = _driver.FindElement(By.Name("EnderecoCobranca.CEP"));
			campoEnderecoCEP.SendKeys(doacao.EnderecoCobranca.CEP);
			campoEnderecoCEP.Click();

			IWebElement campoEnderecoComplemento = null;
			campoEnderecoComplemento = _driver.FindElement(By.Name("EnderecoCobranca.Complemento"));
			campoEnderecoComplemento.SendKeys(doacao.EnderecoCobranca.Complemento);
			campoEnderecoComplemento.Click();

			IWebElement campoEnderecoTelefone = null;
			campoEnderecoTelefone = _driver.FindElement(By.Name("EnderecoCobranca.Telefone"));
			campoEnderecoTelefone.SendKeys(doacao.EnderecoCobranca.Telefone);
			campoEnderecoTelefone.Click();

			IWebElement campoFormaDePagamentoNome = null;
			campoFormaDePagamentoNome = _driver.FindElement(By.Name("FormaPagamento.NomeTitular"));
			campoFormaDePagamentoNome.SendKeys(doacao.FormaPagamento.NomeTitular);
			campoFormaDePagamentoNome.Click();

			IWebElement campoFormaDePagamentoCartao = null;
			campoFormaDePagamentoCartao = _driver.FindElement(By.Name("FormaPagamento.NumeroCartaoCredito"));
			campoFormaDePagamentoCartao.SendKeys(doacao.FormaPagamento.NumeroCartaoCredito);
			campoFormaDePagamentoCartao.Click();

			IWebElement campoFormaDePagamentoValidade = null;
			campoFormaDePagamentoValidade = _driver.FindElement(By.Name("FormaPagamento.Validade"));
			campoFormaDePagamentoValidade.SendKeys(doacao.FormaPagamento.Validade);
			campoFormaDePagamentoValidade.Click();

			IWebElement campoFormaDePagamentoCVV = null;
			campoFormaDePagamentoCVV = _driver.FindElement(By.Name("FormaPagamento.CVV"));
			campoFormaDePagamentoCVV.SendKeys(doacao.FormaPagamento.CVV);
			campoFormaDePagamentoCVV.Click();

			//Assert
			_driver.Url.Should().Contain("/Doacoes/Create");
			//_driver.Url.Should().Contain("/Home/Index");
		}
	}
}