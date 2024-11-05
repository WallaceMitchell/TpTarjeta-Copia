
using System;

public class RegistroViaje {
  
    private DateTime fecha;
    private int viajes;
    private int limite;

    public RegistroViaje (DateTime fecha, int viajes, int limite) {

        this.fecha = fecha;
        this.viajes = viajes;
        this.limite = limite;

    }

    public bool MakeViaje (DateTime fecha) {

        if (fecha == this.fecha) {

            bool ans = this.viajes < this.limite;

            this.viajes += 1;

            return ans;

        } else {

            this.fecha = fecha;
            viajes = 1;

            return true;

        }

    }

}
