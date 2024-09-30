
using System;

class Program {

  static void Main () {

    Colectivo colectivo = new Colectivo(1, "pot 212");
    
    Tarjeta tarjeta1 = new TarjetaCompleta();
    Tarjeta tarjeta2 = new TarjetaParcial();

    tarjeta1.addSaldo(5000);
    tarjeta2.addSaldo(5000);

    Boleto? boleto1 = colectivo.pagarCon(tarjeta1);
    Boleto? boleto2 = colectivo.pagarCon(tarjeta2);

    if (boleto1 is not null) {

      boleto1.showBoleto();

    }

    if (boleto2 is not null) {

      boleto2.showBoleto();

    }

  }  
  
}