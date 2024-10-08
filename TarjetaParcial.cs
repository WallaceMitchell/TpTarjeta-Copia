
using System;

public class TarjetaParcial : Tarjeta {

    private double factor = 1.0 / 2.0;

    public TarjetaParcial () : base() {

      base.tipo = "Tarjeta Parcial";

    }

    public override double getImporte (double precio_neto) {

        return precio_neto * factor;

    }

    private bool enCooldown () {

      DateTime? last_viaje = base.getLastViaje();

      if (last_viaje is not null) {

        TimeSpan diferencia = DateTime.Now - (DateTime) last_viaje;

        return diferencia.TotalMinutes > base.maxCooldown;

      } else {

        return true;

      }

    }

    public override bool comprarPasaje (double precio) {

    bool flag = false;
    
    double precio_final = getImporte(precio);

    if (enCooldown()) {

      if (base.saldo - precio_final >= base.limite) {

        flag = true;
        base.saldo -= precio_final;
        base.setLastViaje(DateTime.Now);

        if (base.saldo < 0) {

          base.saldo_negativo = true;

        }
        
      } else {
      
        Console.WriteLine("No dispone de suficiente saldo\n");
        
      }

    } else {

      Console.WriteLine("Debe esperar más de " + (int) base.maxCooldown + " minutos entre viajes\n");

    }

    return flag;
    
  }

}