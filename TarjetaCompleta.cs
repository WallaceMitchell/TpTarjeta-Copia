
using System;

public class TarjetaCompleta : Tarjeta {

  private double factor = 1.0;

  public TarjetaCompleta () : base() {

    base.tipo = "Tarjeta Completa";

  }

  public override double getImporte (double precio_neto) {

    return precio_neto * factor;

  }

  public override bool comprarPasaje (double precio) {

    bool flag = false;
    
    double precio_final = getImporte(precio);

    if (base.saldo - precio_final >= base.limite) {

      flag = true;
      base.saldo -= precio_final;
      base.setLastViaje(DateTime.Now);

      if (base.saldo < 0) {

        base.saldo_negativo = true;

      }
      
    } else {
    
      Console.WriteLine("No dispone de suficiente saldo");
      
    }

    return flag;
    
  }

}