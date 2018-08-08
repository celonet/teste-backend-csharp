using System;
using Infrastructure.TorreHanoi.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.TorreHanoi.Domain
{
    [TestClass]
    public class TorreHanoiUnit
    {
        private const string CategoriaTeste = "Domain/TorreHanoi";

        private Mock<ILogger> _mockLogger;

        [TestInitialize]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _mockLogger.Setup(s => s.Logar(It.IsAny<string>(), It.IsAny<TipoLog>()));
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Construtor_Deve_Retornar_Sucesso()
        {
            var torre = new global::Domain.TorreHanoi.TorreHanoi(3, _mockLogger.Object);

            Assert.IsNotNull(torre);
            Assert.IsNotNull(torre.PassoAPasso);
            Assert.AreEqual(torre.Discos.Count, 3);
            Assert.AreEqual(torre.Intermediario.Discos.Count, 0);
            Assert.AreEqual(torre.Origem.Discos.Count, 3);
            Assert.AreEqual(torre.Destino.Discos.Count, 0);
            Assert.AreEqual(torre.Status, global::Domain.TorreHanoi.TipoStatus.Pendente);
            Assert.AreNotEqual(torre.DataCriacao, new DateTime());
            Assert.AreNotEqual(torre.Id, new Guid());
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Processar_Deve_Retornar_Sucesso()
        {
            var qtdeDiscos = 3;
            var qtdePassos = Math.Pow(2, qtdeDiscos) - 1;

            var torre = new global::Domain.TorreHanoi.TorreHanoi(qtdeDiscos, _mockLogger.Object);

            torre.Processar();

            Assert.AreEqual(torre.Status, global::Domain.TorreHanoi.TipoStatus.FinalizadoSucesso);
            Assert.AreEqual(torre.Discos.Count, qtdeDiscos);
            Assert.AreEqual(qtdePassos, torre.PassoAPasso.Count);
            Assert.AreNotEqual(torre.DataCriacao, new DateTime());
            Assert.AreNotEqual(torre.Id, new Guid());
        }
    }
}
