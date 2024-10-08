
using System;

public class Colectivo {

  private static double precio_pasaje = 940;
  
  private int id;
  private string linea;
  private string patente;
  
  public Colectivo (int id, string linea, string patente) {

    this.id = id;
    this.linea = linea;
    this.patente = patente;
    
  }

  public Boleto? pagarCon (Tarjeta tarjeta, bool boleto_gratuito) {

    bool comprado = tarjeta.comprarPasaje(precio_pasaje, ref boleto_gratuito);

    if (comprado) {

      return new Boleto(tarjeta, tarjeta.getImporte(precio_pasaje, boleto_gratuito), tarjeta.getSaldo(), boleto_gratuito);
      
    } else {

      return null;
      
    }
    
  }
  
}
