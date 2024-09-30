using System;

public class Tarjeta {

  private static int id_generador = 1;

  private static double[] cargas_permitidas = {2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000};
  
  private const double limite = -480.0;
  private int id;
  private double saldo;  
  public Tarjeta () {

    this.id = id_generador;
    id_generador += 1;
    this.saldo = 0;
    
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

      double saldo_resultante = this.saldo + saldo;
      
      if (saldo_resultante <= 9900) { 

        this.saldo = saldo_resultante;
        
        Console.WriteLine("Se han ingresado con éxito $" + saldo + " en la tarjeta " + this.id);
        
      } else {

        Console.WriteLine("El máximo de saldo es 9900, no se pudo concretar la operación");
        
      }
      
    } else {
      
      Console.WriteLine("Ha ingresado una carga inválida, no se pudo concretar la operación");
      
    }

    this.showSaldo();
    
  }

  public void showSaldo () {

    Console.WriteLine("Saldo actual: " + this.saldo);

  }

  public bool comprarPasaje (double precio) {

    bool flag = false;
    
    if (this.saldo - precio >= limite) {

      flag = true;
      this.saldo -= precio;
      
    } else {
    
      Console.WriteLine("No dispone de suficiente saldo");
      
    }

    return flag;
    
  }

  public double getSaldo() {

    return this.saldo;
    
  }
   
}