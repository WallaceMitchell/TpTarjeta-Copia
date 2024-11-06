using NUnit.Framework;

namespace TestProject1

{
    public class TarjetaParcialTests
    {

        [Test]
        public void TestMedioBoleto()
        {
            TiempoFalso tiempo = new TiempoFalso();
            TarjetaParcial tarjeta = new TarjetaParcial(tiempo);            //test que revisa que el medio boleto cueste la mitad que el normal
            tarjeta.addSaldo(2000);
            tiempo.addHours(8);
            Colectivo colectivo = new Colectivo(1, "2A", "pot 212");
            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(tarjeta.getSaldo(), 1400, "El saldo restante debería ser de 1400 ya que el medio vale 600");
        }

    }
}