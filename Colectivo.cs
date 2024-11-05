
using System;

public class Colectivo {

  private static double precio_pasaje = 1200.0;
  
  private int id;
  private string linea;
  private string patente;
  
  public Colectivo (int id, string linea, string patente) {

    this.id = id;
    this.linea = linea;
    this.patente = patente;
    
  }

  public Boleto? pagarCon (Tarjeta tarjeta) {

    bool comprado = tarjeta.comprarPasaje(precio_pasaje);

    if (comprado) {

      return new Boleto(tarjeta, tarjeta.getImporte(), tarjeta.getSaldo(), tarjeta.getExcedente());
      
    } else {

      return null;
      
    }
    
  }
  
}
