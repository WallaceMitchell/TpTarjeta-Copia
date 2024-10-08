using System;
using System.ComponentModel.DataAnnotations;

public abstract class Tarjeta {

  private static int id_generador = 1;

  private static double[] cargas_permitidas = {2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000};
  
  protected double max_saldo = 36000;
  protected double limite = -480.0;
  protected double maxCooldown = 0.0;
  private int id;
  protected string tipo;
  protected double saldo = 0.0;  
  protected double excedente = 0.0;
  protected bool saldo_negativo;
  protected DateTime? last_viaje = null;

  public Tarjeta () {

    this.id = id_generador;
    id_generador += 1;
    this.saldo = 0;
    this.tipo = "";
    
  }
  
  public int getId () {

    return this.id;
    
  }
  
  public void addSaldo (double saldo) {

    bool permitido = false;
    
    for (int i = 0; i < cargas_permitidas.Length; i++) {

      if (saldo == cargas_permitidas[i]) {

        permitido = true;
        
      }
      
    }

    if (permitido) {

      double new_saldo = this.saldo + excedente + saldo;

      if (new_saldo > this.max_saldo) {

        excedente = new_saldo - this.max_saldo;
        new_saldo = this.max_saldo;

      }      

      this.saldo = new_saldo;

      Console.WriteLine("Carga exitosa");

    } else {
      
      Console.WriteLine("Ha ingresado una carga inválida, no se pudo concretar la operación");
      
    }

    this.showSaldo();
    
  }

  public void showSaldo () {

    Console.WriteLine("\n----------------------------------------------------");
    Console.WriteLine("Saldo actual: " + this.saldo + " (Excedente: " + this.excedente + ")");
    Console.WriteLine("----------------------------------------------------\n");

  }

  public double getSaldo() {

    return this.saldo;
    
  }

  public string getTipo() {

    return this.tipo;

  }

  public bool isNegativo() {

    return this.saldo_negativo;

  }

  public void setNegativo(bool negativo) {

    this.saldo_negativo = negativo;

  }

  public DateTime? getLastViaje() {

    return this.last_viaje;

  }

  public void setLastViaje (DateTime? fecha) {

    this.last_viaje = fecha;

  }

  public abstract double getImporte (double precio_neto, bool boleto_gratuito);

  public abstract bool comprarPasaje (double precio, ref bool boleto_gratuito);
   
}