
using System;

public class TarjetaParcial : Tarjeta {

    private double factor = 1.0 / 2.0;

    public TarjetaParcial () : base() {

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