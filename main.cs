
using System;

class Program {

  public static void Main () {

    TiempoFalso tiempo = new TiempoFalso();

    Colectivo colectivo = new Colectivo(1, "2A", "pot 212");
    
    Tarjeta tarjeta = new TarjetaCompleta(tiempo);

    tiempo.addHours(8);

    tarjeta.addSaldo(2000);

    for (int i = 0; i < 3; i++) {

      Boleto? boleto = colectivo.pagarCon(tarjeta, false);

      if (boleto is not null) {

        boleto.showBoleto();

      }

    }

  }
  
}