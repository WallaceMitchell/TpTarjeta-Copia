
using System;

public class Frecuente {

  private int mes;
  private int viajes;

  public Frecuente (int mes, int viajes) {

    this.mes = mes;
    this.viajes = viajes;

  }

  public double getDescuento (int mes) {

    if (mes == this.mes) {

        this.viajes += 1;

    } else {

        this.viajes = 1;

    }

    if ((this.viajes >= 1) && (this.viajes <= 29)) {

        return 1.0;

    } else if (this.viajes < 79) {

        return 0.8;

    } else if (this.viajes <= 80) {

        return 0.75;

    } else {

        return 1.0;

    }

  }
  
}
