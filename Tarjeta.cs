using System;

public class Tarjeta {

  private static int id_generador = 1;

  private static double[] cargasPermitidas = {2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000};
  
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
    
    for (int i = 0; i < cargasPermitidas.Length; i++) {

      if (saldo == cargasPermitidas[i]) {

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
    
  }

  public bool comprarPasaje (double precio) {

    bool flag = false;
    
    if (this.saldo >= precio) {

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