
using System;

class Program {

  static void Main () {

    Colectivo colectivo = new Colectivo(1, "2A", "pot 212");
    
    Tarjeta tarjeta1 = new TarjetaCompleta();

    for (int i = 0; i < 10; i++) {

      tarjeta1.addSaldo(5000);

    }

  }  
  
}