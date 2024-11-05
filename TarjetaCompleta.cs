
using System;

public class TarjetaCompleta : Tarjeta {

  private RegistroViaje rv;
  
  public TarjetaCompleta (Tiempo tiempo) : base(tiempo) {

    base.tipo = "Tarjeta Completa";
    base.factor = 0.0;
    rv = new RegistroViaje(this.tiempo.now().Date, 0, 2);

  }

  public bool ViajeGratuito () {

    return rv.MakeViaje(tiempo.now().Date);

  }

}