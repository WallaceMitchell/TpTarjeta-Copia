
using System;
using System.Collections.Generic;

public class TarjetaParcial : Tarjeta {

    private double factor = 1.0 / 2.0;

    private int boletos_gratuitos = 2;

    (DateTime fecha, int viajes_gratuitos) registro_viajes_gratuitos; 

    public TarjetaParcial () : base() {

      base.tipo = "Tarjeta Parcial";
      registro_viajes_gratuitos = (DateTime.Now.Date, boletos_gratuitos);

    }

    public override double getImporte (double precio_neto, bool boleto_gratuito) {

        if (boleto_gratuito) {

          return 0.0;

        }

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

    public override bool comprarPasaje (double precio, ref bool boleto_gratuito) {

    bool flag = false;

    if (enCooldown()) {

      if (DateTime.Now.Date != registro_viajes_gratuitos.fecha) {

        registro_viajes_gratuitos = (DateTime.Now.Date, boletos_gratuitos);

      }

      if (boleto_gratuito) {

        if (registro_viajes_gratuitos.viajes_gratuitos > 0) {

          this.registro_viajes_gratuitos.viajes_gratuitos -= 1;
          
        } else {

          boleto_gratuito = false;
          Console.WriteLine("No hay boletos gratuitos disponibles\n");

        }

      }

      double precio_final = getImporte(precio, boleto_gratuito);

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