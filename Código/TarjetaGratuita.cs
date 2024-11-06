
using System;

public class TarjetaGratuita : Tarjeta {

    public TarjetaGratuita (Tiempo tiempo) : base(tiempo) {

        base.tipo = "Tarjeta Gratuita";
        base.factor = 0.0;

    }

}