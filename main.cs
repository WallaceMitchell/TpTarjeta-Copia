
using System;

class Program {

  static void Main () {

    Colectivo colectivo = new Colectivo(1, "2A", "pot 212");
    
    Tarjeta tarjeta1 = new TarjetaParcial();
    Tarjeta tarjeta2 = new TarjetaParcial();

    tarjeta1.addSaldo(7000);
    tarjeta2.addSaldo(2000);

    for (int i = 0; i < 8; i++) {

      Boleto? boleto1 = colectivo.pagarCon(tarjeta1);
      
      if (boleto1 is not null) {

        boleto1.showBoleto();

      }

    }

    tarjeta1.addSaldo(5000);
    
    Boleto? boletoaux = colectivo.pagarCon(tarjeta1);

    if (boletoaux is not null) {

      boletoaux.showBoleto();

    }

    Boleto? boleto2 = colectivo.pagarCon(tarjeta2);

    if (boleto2 is not null) {

      boleto2.showBoleto();

    }

  }  
  
}