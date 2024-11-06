namespace TestProject1

{
    public class TarjetaTests
    {

        private Tarjeta tarjeta;

        [SetUp]
        public void Setup()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(tiempo);
        }

        [Test]
        public void TestCargasPermitidas()
        {
            double[] cargasPermitidas = { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 };       //test que se revisa que las todas las cargas permitidas se carguen en la tarjeta

            foreach (var carga in cargasPermitidas)
            {
                tarjeta.addSaldo(carga);
                Assert.AreEqual(carga, tarjeta.getSaldo(), $"La carga {carga} no se ha realizado.");
                TiempoFalso tiempo = new TiempoFalso();
                tarjeta = new Tarjeta(tiempo);
            }
        }

        [Test]
        public void TestCargaInvalida()
        {
            tarjeta.addSaldo(1000);
            Assert.AreEqual(0, tarjeta.getSaldo(), "el saldo deberia quedarse como está.");      //test que revisa que una carga invalida no se sume al saldo inicial
        }

        [Test]
        public void TestCargaInvalida2()
        {
            tarjeta.addSaldo(2000); //esto está bien
            tarjeta.addSaldo(1000); //esto no ya que es invalido
            Assert.AreEqual(2000, tarjeta.getSaldo(), "el saldo deberia permanecer igual.");      //test que revisa que una carga invalida no se sume al saldo actual
        }

        [Test]
        public void PlataSuficiente()
        {
            TiempoFalso tiempo = new TiempoFalso();
            Tarjeta tarjeta = new Tarjeta(tiempo);      //test que corrobora que se permita comprar un boleto con saldo suficiente.
            Colectivo colectivo = new Colectivo(1, "2A", "pot212");
            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            tarjeta.addSaldo(2000);

            colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(800, tarjeta.getSaldo(), "El saldo no fue correctamente descontado.");
        }

        [Test]
        public void PlataInsuficiente()
        {
            TiempoFalso tiempo = new TiempoFalso();
            Tarjeta tarjeta = new Tarjeta(tiempo);      //test que corrobora que no se permita comprar un boleto con saldo insuficiente.
            Colectivo colectivo = new Colectivo(1, "2A", "pot212");
            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            tarjeta.addSaldo(2000);

            colectivo.pagarCon(tarjeta, false); //este se deberia comprar
            colectivo.pagarCon(tarjeta, false); //este tambien (saldo negativo)
            colectivo.pagarCon(tarjeta, false); //en este no se deberia comprar y el saldo queda en -400

            Assert.AreEqual(-400, tarjeta.getSaldo(), "El saldo debería ser de 800.");
        }

        [Test]
        public void TestLimite()
        {
            TiempoFalso tiempo = new TiempoFalso();
            Tarjeta tarjeta = new Tarjeta(tiempo);  //test que corrobora que la tarjeta no pueda pasarse del limite negativo -480.
            tarjeta.addSaldo(2000);

            Colectivo colectivo = new Colectivo(1, "2A", "pot212");
            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            colectivo.pagarCon(tarjeta, false); //saldo de 800
            colectivo.pagarCon(tarjeta, false); //saldo de -400
            colectivo.pagarCon(tarjeta, false); //esta vez no deberia comprar pasaje ya que se pasaria

            Assert.AreEqual(tarjeta.getSaldo(), -400, "Incorrecta administracion del saldo negativo");
        }

        [Test]
        public void TestSaldoAdeudado()
        {
            TiempoFalso tiempo = new TiempoFalso();
            Tarjeta tarjeta = new Tarjeta(tiempo);      //test que verifica que cuando se tiene un saldo negativo y se recarga la tarjeta, se reste de la recarga
            tarjeta.addSaldo(2000);                                     //lo que se adeudaba.
            Colectivo colectivo = new Colectivo(1, "2A", "pot212");
            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            colectivo.pagarCon(tarjeta, false); //saldo de 800
            colectivo.pagarCon(tarjeta, false); //saldo negativo de -400

            Assert.AreEqual(-400, tarjeta.getSaldo(), "El saldo debería ser -400");

            tarjeta.addSaldo(2000);

            double saldoNuevo = 2000 - 400; //recargo y cancelo deuda
            Assert.AreEqual(saldoNuevo, tarjeta.getSaldo(), "El saldo no se actualizó bien");
        }

        [Test] //Chequeamos que no se genere boleto al pasar 2 minutos entre compras
        public void LimitacionEnElPagoDeMediosBoletos1()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaParcial(tiempo);

            tarjeta.addSaldo(8000);

            Boleto? boleto1 = colectivo.pagarCon(tarjeta, false);

            tiempo.addMinutes(2);

            Boleto? boleto2 = colectivo.pagarCon(tarjeta, false);

            Assert.IsNull(boleto2);

        }

        [Test] //Chequeamos que no se genere boleto al pasar 4 minutos entre compras
        public void LimitacionEnElPagoDeMediosBoletos2()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaParcial(tiempo);

            tarjeta.addSaldo(8000);

            Boleto? boleto1 = colectivo.pagarCon(tarjeta, false);

            tiempo.addMinutes(4);

            Boleto? boleto2 = colectivo.pagarCon(tarjeta, false);

            Assert.IsNull(boleto2);

        }

        [Test] //Chequeamos que se genere boleto al pasar 5 minutos y 1 segundo entre compras
        public void LimitacionEnElPagoDeMediosBoletos3()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaParcial(tiempo);

            tarjeta.addSaldo(8000);

            Boleto? boleto1 = colectivo.pagarCon(tarjeta, false);

            tiempo.addMinutes(5);
            tiempo.addSeconds(1);

            Boleto? boleto2 = colectivo.pagarCon(tarjeta, false);

            Assert.IsNotNull(boleto2);

        }

        [Test] //Chequeamos que el costo de un boleto de tarjeta parcial luego de 4 compras en un día sea 1200.
        public void LimitacionEnElPagoDeMediosBoletos4()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaParcial(tiempo);

            tarjeta.addSaldo(8000);

            for (int i = 0; i < 4; i++)
            {
                Boleto? boleto = colectivo.pagarCon(tarjeta, false);
                tiempo.addHours(1);
            }

            Boleto? boletoTest = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(1200, boletoTest.getImporte());

        }

        [Test] //Chequeamos que el costo de un boleto de tarjeta parcial luego de 3 compras en un día sea 600.
        public void LimitacionEnElPagoDeMediosBoletos5()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaParcial(tiempo);

            tarjeta.addSaldo(8000);

            for (int i = 0; i < 3; i++)
            {
                Boleto? boleto = colectivo.pagarCon(tarjeta, false);
                tiempo.addHours(1);
            }

            Boleto? boletoTest = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(600, boletoTest.getImporte());

        }

        [Test] //Chequeamos que el costo de un boleto de tarjeta parcial luego de 5 compras en un día sea 1200.
        public void LimitacionEnElPagoDeMediosBoletos6()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaParcial(tiempo);

            tarjeta.addSaldo(8000);

            for (int i = 0; i < 5; i++)
            {
                Boleto? boleto = colectivo.pagarCon(tarjeta, false);
                tiempo.addHours(1);
            }

            Boleto? boletoTest = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(1200, boletoTest.getImporte());

        }

        [Test] //Chequeamos que el costo de un boleto de tarjeta parcial luego de 10 compras en un día sea 1200.
        public void LimitacionEnElPagoDeMediosBoletos7()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaParcial(tiempo);

            tarjeta.addSaldo(8000);
            tarjeta.addSaldo(8000);
            tarjeta.addSaldo(8000);

            for (int i = 0; i < 10; i++)
            {
                Boleto? boleto = colectivo.pagarCon(tarjeta, false);
                tiempo.addHours(1);
            }

            Boleto? boletoTest = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(1200, boletoTest.getImporte());

        }

        [Test] //Chequeamos que al comprar 1 boleto con tarjeta completa en un día, el costo por boleto sea 0.
        public void LimitacionEnElPagoDeFranquiciasCompletas1()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaCompleta(tiempo);

            tarjeta.addSaldo(8000);
            tarjeta.addSaldo(8000);
            tarjeta.addSaldo(8000);

            Boleto? boletoTest = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(0, boletoTest.getImporte());

        }

        [Test] //Chequeamos que al comprar 1 boleto interurbano con tarjeta completa en un día, el costo por boleto sea 0.
        public void LimitacionEnElPagoDeFranquiciasCompletas2()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaCompleta(tiempo);

            tarjeta.addSaldo(8000);
            tarjeta.addSaldo(8000);
            tarjeta.addSaldo(8000);

            Boleto? boletoTest = colectivo.pagarCon(tarjeta, true);

            Assert.AreEqual(0, boletoTest.getImporte());

        }

        [Test] //Chequeamos que al comprar 2 boletos interurbanos con tarjeta completa en un día, el costo por boleto sea 0.
        public void LimitacionEnElPagoDeFranquiciasCompletas3()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaCompleta(tiempo);

            tarjeta.addSaldo(8000);
            tarjeta.addSaldo(8000);
            tarjeta.addSaldo(8000);

            Boleto? boletoTest1 = colectivo.pagarCon(tarjeta, true);
            tiempo.addMinutes(10);
            Boleto? boletoTest2 = colectivo.pagarCon(tarjeta, true);

            Assert.AreEqual(0, boletoTest2.getImporte());

        }

        [Test] //Chequeamos que al comprar 2 boletos interurbanos con tarjeta completa en un día, el costo por boleto sea 1200.
        public void LimitacionEnElPagoDeFranquiciasCompletas4()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaCompleta(tiempo);

            tarjeta.addSaldo(8000);
            tarjeta.addSaldo(8000);
            tarjeta.addSaldo(8000);

            Boleto? boletoTest1 = colectivo.pagarCon(tarjeta, false);
            tiempo.addMinutes(10);
            Boleto? boletoTest2 = colectivo.pagarCon(tarjeta, false);
            tiempo.addMinutes(10);
            Boleto? boletoTest3 = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(1200, boletoTest3.getImporte());

        }
         
        [Test] //Chequeamos que al comprar 10 boletos interurbanos con tarjeta completa en un día, el costo por boleto sea 1200.
        public void LimitacionEnElPagoDeFranquiciasCompletas5()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new TarjetaCompleta(tiempo);

            tarjeta.addSaldo(8000);
            tarjeta.addSaldo(8000);
            tarjeta.addSaldo(8000);

            for (int i = 0; i < 10; i++)
            {

                Boleto? boleto = colectivo.pagarCon(tarjeta, false);
                tiempo.addMinutes(10);

            }

            Boleto? boletoTest = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(1200, boletoTest.getImporte());

        }

        [Test] //Comparamos saldo y excedente con valores calculados.
        public void SaldoDeLaTarjeta1()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new Tarjeta(tiempo);

            int valorEsperado = 0;

            for (int i = 0; i < 10; i++)
            {

                tarjeta.addSaldo(9000);
                valorEsperado += 9000;

            }

            valorEsperado -= 36000;

            Assert.AreEqual(36000, tarjeta.getSaldo());
            Assert.AreEqual(valorEsperado, tarjeta.getExcedente());

        }

        [Test] //Comparamos saldo y excedente con valores calculados.
        public void SaldoDeLaTarjeta2()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new Tarjeta(tiempo);

            int valorEsperado = 0;

            for (int i = 0; i < 50; i++)
            {

                tarjeta.addSaldo(9000);
                valorEsperado += 9000;

            }

            valorEsperado -= 36000;

            Assert.AreEqual(36000, tarjeta.getSaldo());
            Assert.AreEqual(valorEsperado, tarjeta.getExcedente());

        }

        [Test] //Comparamos saldo y excedente en caso de no sobrepasar el límite de saldo.
        public void SaldoDeLaTarjeta3()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new Tarjeta(tiempo);

            for (int i = 0; i < 3; i++)
            {

                tarjeta.addSaldo(9000);

            }

            Assert.AreEqual(27000, tarjeta.getSaldo());
            Assert.AreEqual(0, tarjeta.getExcedente());

        }

        [Test] //Comparamos saldo y excedente con valores precalculados.
        public void SaldoDeLaTarjeta4()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new Tarjeta(tiempo);

            for (int i = 0; i < 6; i++)
            {

                tarjeta.addSaldo(9000);

            }

            Assert.AreEqual(36000, tarjeta.getSaldo());
            Assert.AreEqual(18000, tarjeta.getExcedente());

        }

        [Test] //Comparamos saldo y excedente con valores precalculados luego de comprar boleto.
        public void SaldoDeLaTarjeta5()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new Tarjeta(tiempo);

            for (int i = 0; i < 6; i++)
            {

                tarjeta.addSaldo(9000);

            }

            Boleto boletoTest = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(36000, tarjeta.getSaldo());
            Assert.AreEqual(16800, tarjeta.getExcedente());

        }

        [Test] //Comparamos saldo y excedente con valores precalculados luego de comprar boleto y gastar todo el excedente. 
        public void SaldoDeLaTarjeta6()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new Tarjeta(tiempo);

            for (int i = 0; i < 4; i++)
            {

                tarjeta.addSaldo(9000);

            }

            tarjeta.addSaldo(2000);

            Boleto boletoTest1 = colectivo.pagarCon(tarjeta, false);
            Boleto boletoTest2 = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(35600, tarjeta.getSaldo());
            Assert.AreEqual(0, tarjeta.getExcedente());

        }

        [Test] //Comparamos saldo y excedente con valores precalculados luego de comprar boleto y no gastar todo el excedente. 
        public void SaldoDeLaTarjeta7()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new Tarjeta(tiempo);

            for (int i = 0; i < 4; i++)
            {

                tarjeta.addSaldo(9000);

            }

            tarjeta.addSaldo(2000);

            Boleto boletoTest1 = colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(36000, tarjeta.getSaldo());
            Assert.AreEqual(800, tarjeta.getExcedente());

        }

        [Test]
        public void TestTarifa29()
        {
            tarjeta = new Tarjeta(new TiempoFalso());
            for (int i = 0; i < 100; i++)
            {
                tarjeta.addSaldo(9000);
            }

            Colectivo colectivo = new Colectivo(1, "2A", "pot212");     //test que verifica que se hagan correctamente los descuentos segun los viajes mensuales.
            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            for (int i = 1; i <= 29; i++)
            {
                double precioBoleto = 1200.0;
                Assert.AreEqual(1200, boleto.getImporte(), $"Error en descuento en viaje {i}");
            }
        }

        [Test]
        public void Test20Porciento()
        {
            tarjeta = new Tarjeta(new TiempoFalso());
            for (int i = 0; i < 100; i++)
            {
                tarjeta.addSaldo(9000);
            }
            Colectivo colectivo = new Colectivo(1, "2A", "pot212");
            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            for (int i = 1; i <= 29; i++)
            {
                colectivo.pagarCon(tarjeta, false);
            }

            for (int i = 30; i < 78; i++)
            {
                boleto = colectivo.pagarCon(tarjeta, false);
                Assert.AreEqual(960, boleto.getImporte(), $"Error en descuento en viaje {i}");
            }
        }

        [Test]
        public void Test25Porciento()
        {
            tarjeta = new Tarjeta(new TiempoFalso());
            tarjeta = new Tarjeta(new TiempoFalso());
            for (int i = 0; i < 100; i++)
            {
                tarjeta.addSaldo(9000);
            }
            Colectivo colectivo = new Colectivo(1, "2A", "pot212");
            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            for (int i = 1; i <= 29; i++)
            {
                colectivo.pagarCon(tarjeta, false);
            }

            for (int i = 30; i < 78; i++)
            {
                colectivo.pagarCon(tarjeta, false);
            }

            boleto = colectivo.pagarCon(tarjeta, false);
            Assert.AreEqual(900, boleto.getImporte(), "Error en descuento en viaje 80");
        }

        [Test]
        public void TestTarifa81()
        {
            tarjeta = new Tarjeta(new TiempoFalso());
            tarjeta = new Tarjeta(new TiempoFalso());
            for (int i = 0; i < 100; i++)
            {
                tarjeta.addSaldo(9000);
            }
            Colectivo colectivo = new Colectivo(1, "2A", "pot212");
            Boleto? boleto = colectivo.pagarCon(tarjeta, false);

            for (int i = 1; i <= 80; i++)
            {
                colectivo.pagarCon(tarjeta, false);
            }

            for (int i = 81; i <= 85; i++)
            {
                Assert.AreEqual(1200.0, boleto.getImporte(), $"Error en descuento en viaje {i}"); //hacemos algunos viajes mas de prueba
            }
        }

        [Test]
        public void TestNormal()
        {
            TiempoFalso tiempo = new TiempoFalso();     //tiempo falso inicia en 00:00 hs (horario deshabilitado para viajes con beneficios)
            Tarjeta tarjeta = new Tarjeta(tiempo);
            Colectivo colectivo = new Colectivo(1, "2A", "pot212");

            tarjeta.addSaldo(2000);

            colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(800, tarjeta.getSaldo(), "El saldo no debería cambiar"); //se paga normal

        }

        [Test]
        public void TestParcial()
        {

            TiempoFalso tiempo = new TiempoFalso();     //hacemos lo mismo con la franquicia parcial
            TarjetaParcial tarjeta = new TarjetaParcial(tiempo);
            Colectivo colectivo = new Colectivo(1, "2A", "pot212");

            tarjeta.addSaldo(2000);

            colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(800, tarjeta.getSaldo(), "El saldo no debería cambiar"); //se paga normal

        }
        [Test]
        public void TestCompleta()
        {

            TiempoFalso tiempo = new TiempoFalso();     //hacemos lo mismo con la franquicia completa
            TarjetaCompleta tarjeta = new TarjetaCompleta(tiempo);
            Colectivo colectivo = new Colectivo(1, "2A", "pot212");

            tarjeta.addSaldo(2000);

            colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(800, tarjeta.getSaldo(), "El saldo no debería cambiar"); //se paga normal

        }

        [Test]
        public void TestGratuita() {

            TiempoFalso tiempo = new TiempoFalso();     //hacemos lo mismo con la franquicia de jubilados (100% viajes gratuitos)
            TarjetaGratuita tarjeta = new TarjetaGratuita(tiempo);
            Colectivo colectivo = new Colectivo(1, "2A", "pot212");

            tarjeta.addSaldo(2000);

            colectivo.pagarCon(tarjeta, false);

            Assert.AreEqual(800, tarjeta.getSaldo(), "El saldo no debería cambiar"); //se paga normal

        }


        [Test] //Chequeamos viaje interurbano (segundo parámetro de pagarCon = true)
        public void LineasInterurbanas1()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            Tarjeta tarjeta = new Tarjeta(tiempo);

            tarjeta.addSaldo(8000);

            Boleto? boletoTest = colectivo.pagarCon(tarjeta, true);

            Assert.AreEqual(2500, boletoTest.getImporte());
        }

        [Test] //Chequeamos viaje interurbano (segundo parámetro de pagarCon = true)
        public void LineasInterurbanas2()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            TarjetaParcial tarjeta = new TarjetaParcial(tiempo);

            tarjeta.addSaldo(8000);

            Boleto? boletoTest = colectivo.pagarCon(tarjeta, true);

            Assert.AreEqual(1250, boletoTest.getImporte());
        }

        [Test] //Chequeamos viaje interurbano (segundo parámetro de pagarCon = true)
        public void LineasInterurbanas3()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            TarjetaCompleta tarjeta = new TarjetaCompleta(tiempo);

            tarjeta.addSaldo(8000);

            Boleto? boletoTest1 = colectivo.pagarCon(tarjeta, true);
            tiempo.addMinutes(10);
            Boleto? boletoTest2 = colectivo.pagarCon(tarjeta, true);
            tiempo.addMinutes(10);
            Boleto? boletoTest3 = colectivo.pagarCon(tarjeta, true);

            Assert.AreEqual(0, boletoTest1.getImporte());
            Assert.AreEqual(0, boletoTest2.getImporte());
            Assert.AreEqual(2500, boletoTest3.getImporte());
        }

        [Test] //Chequeamos viaje interurbano (segundo parámetro de pagarCon = true)
        public void LineasInterurbanas4()
        {
            TiempoFalso tiempo = new TiempoFalso();
            tiempo.addHours(8);

            Colectivo colectivo = new Colectivo(2, "12", "pot212");
            TarjetaGratuita tarjeta = new TarjetaGratuita(tiempo);

            tarjeta.addSaldo(8000);

            Boleto? boletoTest = colectivo.pagarCon(tarjeta, true);

            Assert.AreEqual(0, boletoTest.getImporte());

        }

    }
}