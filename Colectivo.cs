
using System;

public class Colectivo {

  private static double precio_pasaje_normal = 1200.0;
  private static double precio_pasaje_interurbana = 2500.0;
  
  private int id;
  private string linea;
  private string patente;
  
  public Colectivo (int id, string linea, string patente) {

    this.id = id;
    this.linea = linea;
    this.patente = patente;
    
  }

  public Boleto? pagarCon (Tarjeta tarjeta, bool interurbana) {

    Double precio_pasaje;

    if (interurbana) {

      precio_pasaje = precio_pasaje_interurbana;

    } else {

      precio_pasaje = precio_pasaje_normal;

    }

    bool comprado = tarjeta.comprarPasaje(precio_pasaje);

    if (comprado) {

      return new Boleto(tarjeta, interurbana, tarjeta.getImporte(), tarjeta.getSaldo(), tarjeta.getExcedente());
      
    } else {

      return null;
      
    }
    
  }
  
}
