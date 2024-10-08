
using System;
using System.ComponentModel;

public class Boleto {

  private static int id_generador = 1;
  private int id;
  private int id_de_tarjeta;
  private string tipo_de_tarjeta;
  private double importe;
  private double saldo;
  private DateTime fecha;
  private bool salir_de_negativo = false;
  
  public Boleto (Tarjeta tarjeta, double importe, double saldo) {

    this.id = id_generador;
    id_generador += 1;
    this.id_de_tarjeta = tarjeta.getId();
    this.tipo_de_tarjeta = tarjeta.getTipo();
    this.importe = importe;
    this.saldo = saldo;
    this.fecha = DateTime.Now;

    if (tarjeta.isNegativo() && (tarjeta.getSaldo() > 0)) {

        tarjeta.setNegativo(false);
        this.salir_de_negativo = true;

    }
    
  }

  public int getId () {

    return this.id;
    
  }

  public double getImporte () {

    return this.importe;
    
  }

  public double getSaldo () {

    return this.saldo;
    
  }
  
  public void showBoleto () {

    Console.WriteLine("\n----------------------------------------------------");
    Console.WriteLine("Boleto " + this.id + "\n");

    Console.WriteLine("Fecha: " + this.fecha + "\n");

    Console.WriteLine("Tarjeta: " + this.id_de_tarjeta + ", " + this.tipo_de_tarjeta);

    Console.WriteLine("Importe: " + this.importe);
    Console.WriteLine("Saldo restante: " + this.saldo);

    if (this.salir_de_negativo) {

      Console.WriteLine("\nSaldo negativo cancelado");

    }

    Console.WriteLine("----------------------------------------------------\n");
    
  }
  
}