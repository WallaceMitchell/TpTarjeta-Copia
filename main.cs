
using System;

class Program {

  static void Main () {

    TiempoFalso tiempo = new TiempoFalso();

    Colectivo colectivo = new Colectivo(1, "2A", "pot 212");
    
    Tarjeta tarjeta = new TarjetaGratuita(tiempo);

    tarjeta.addSaldo(8000);

    for (int i = 0; i < 10; i++) {

      Boleto? boleto = colectivo.pagarCon(tarjeta, false);

      if (boleto is not null) {

        boleto.showBoleto();

      }

      tiempo.addHours(4);

    }
    

  }
  
}