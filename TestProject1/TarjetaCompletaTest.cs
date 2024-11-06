using NUnit.Framework;

namespace TestProject1

{
    public class TarjetaCompletaTests
    {

        [Test]
        public void TestPagoSiemprePermitido()
        {
            TiempoFalso tiempo = new TiempoFalso();

            Colectivo colectivo = new Colectivo(1, "2A", "pot 212");

            Tarjeta tarjeta = new TarjetaCompleta(tiempo);

            tiempo.addHours(8);

            tarjeta.addSaldo(2000);

            for (int i = 0; i < 2; i++)
            {

                Boleto? boleto = colectivo.pagarCon(tarjeta, false);   //test que verifica que las tarjetas completas siempre puedan comprar, ya sea gratuitamente o el total del boleto.

                if (boleto is not null)
                {

                    boleto.showBoleto();

                }
                Assert.AreEqual(tarjeta.getSaldo(), 2000);

            }

            Boleto? boleto1 = colectivo.pagarCon(tarjeta, false);
            Assert.AreEqual(tarjeta.getSaldo(), 800);
        }

    }
}