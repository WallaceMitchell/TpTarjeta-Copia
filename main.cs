
using System;

class Program {

  static void Main () {

    Colectivo colectivo = new Colectivo(1, "2A", "pot 212");
    
    Tarjeta tarjeta1 = new TarjetaParcial();

    tarjeta1.addSaldo(2000);

    Boleto? boleto1 = colectivo.pagarCon(tarjeta1, false);
    Boleto? boleto2 = colectivo.pagarCon(tarjeta1, true);
    Boleto? boleto3 = colectivo.pagarCon(tarjeta1, true);
    Boleto? boleto4 = colectivo.pagarCon(tarjeta1, false);
    Boleto? boleto5 = colectivo.pagarCon(tarjeta1, true);

    if (boleto1 is not null) {

      boleto1.showBoleto();

    }

    if (boleto2 is not null) {

      boleto2.showBoleto();

    }

    if (boleto3 is not null) {

      boleto3.showBoleto();

    }

    if (boleto4 is not null) {

      boleto4.showBoleto();

    }

    if (boleto5 is not null) {

      boleto5.showBoleto();

    }

  }  
  
}