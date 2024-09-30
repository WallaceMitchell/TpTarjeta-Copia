
using System;

public class TarjetaCompleta : Tarjeta {

    private double factor = 1.0;

    public TarjetaCompleta () : base() {

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
      
    } else {
    
      Console.WriteLine("No dispone de suficiente saldo");
      
    }

    return flag;
    
  }

}