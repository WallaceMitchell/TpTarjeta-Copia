
using System;

class Program {

  static void Main () {

    TiempoFalso tiempo = new TiempoFalso();

    Colectivo colectivo = new Colectivo(1, "2A", "pot 212");
    
    Tarjeta tarjeta1 = new TarjetaParcial(tiempo);

    for (int i = 0; i < 100; i++) {

      tarjeta1.addSaldo(9000);

    }

    for (int i = 0; i < 100; i++) {

      Boleto? boleto = colectivo.pagarCon(tarjeta1);

      if (boleto is not null) {

        boleto.showBoleto();

      }

      tiempo.addHours(3);
      tiempo.addMinutes(0);

    }

  }  
  
}