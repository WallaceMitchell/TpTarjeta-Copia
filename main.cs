
using System;

class Program {

  static void Main () {

    Colectivo colectivo = new Colectivo(1, "pot 212");
    
    Tarjeta tarjeta = new Tarjeta();
    Tarjeta tarjeta2 = new Tarjeta();

    tarjeta.addSaldo(10000);
    tarjeta.addSaldo(5000);
    tarjeta.addSaldo(5000);

    for (int i = 0; i < 10; i++) {

      Boleto? boleto = colectivo.pagarCon(tarjeta);

      if (boleto is not null) {

        boleto.showBoleto();

      } else {
  
        Console.WriteLine("Boleto no generado");
        
      }
      
    }
    
  }  
  
}