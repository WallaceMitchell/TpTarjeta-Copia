using NUnit.Framework;

namespace TestProject1

{
    public class BoletoTests
    {
        [Test]
        public void MasDatosSobreBoleto1 ()
        {
            Tiempo tiempo = new TiempoFalso();

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new Tarjeta(tiempo);

            tarjeta.addSaldo(2000);

            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual("Tarjeta Normal", boleto.getTipoDeTarjeta());
            Assert.AreEqual(tarjeta.getId(), boleto.getIdDeTarjeta());
            Assert.AreEqual(800, boleto.getSaldo());
        }

        [Test]
        public void MasDatosSobreBoleto2 ()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaParcial(tiempo);

            tarjeta.addSaldo(8000);

            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual("Tarjeta Parcial", boleto.getTipoDeTarjeta());
            Assert.AreEqual(tarjeta.getId(), boleto.getIdDeTarjeta());
            Assert.AreEqual(7400, boleto.getSaldo());
        }

        [Test]
        public void MasDatosSobreBoleto3()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaGratuita(tiempo);

            tarjeta.addSaldo(8000);

            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual("Tarjeta Gratuita", boleto.getTipoDeTarjeta());
            Assert.AreEqual(tarjeta.getId(), boleto.getIdDeTarjeta());
            Assert.AreEqual(0, boleto.getImporte());
        }

        [Test]
        public void MasDatosSobreBoleto4()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaCompleta(tiempo);

            tarjeta.addSaldo(6000);

            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual("Tarjeta Completa", boleto.getTipoDeTarjeta());
            Assert.AreEqual(tarjeta.getId(), boleto.getIdDeTarjeta());
            Assert.AreEqual(0, boleto.getImporte());
        }
    }
}