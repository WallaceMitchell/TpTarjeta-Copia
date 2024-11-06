
using System;
using System.Collections.Generic;

public class TarjetaParcial : Tarjeta {

  private RegistroViaje rv;
  public TarjetaParcial (Tiempo tiempo) : base(tiempo) {

    base.tipo = "Tarjeta Parcial";
    base.factor = 0.5;
    rv = new RegistroViaje(this.tiempo.now().Date, 0, 4);

  }

  public bool ViajeParcial () {

    return rv.MakeViaje(tiempo.now().Date);

  }

  public bool hasCooldownElapsed () {

    TimeSpan? time_difference = base.tiempo.now() - base.ultimo_viaje;

    if (time_difference.HasValue) {

      return time_difference.Value.TotalMinutes > 5.0;

    }

    return true;
     
  }

}