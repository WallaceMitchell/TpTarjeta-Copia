
using System;

public class Boleto {

  private static int id_generador = 1;
  
  private int id;
  private double importe;
  private double saldo;
  
  public Boleto (double importe, double saldo) {

    this.id = id_generador;
    id_generador += 1;
    this.importe = importe;
    this.saldo = saldo;
    
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

    Console.WriteLine("Boleto " + this.id);
    Console.WriteLine("Importe: " + this.importe);
    Console.WriteLine("Saldo restante: " + this.saldo);
    
  }
  
}