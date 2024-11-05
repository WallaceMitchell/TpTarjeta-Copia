using System;
using System.ComponentModel.DataAnnotations;

public class Tarjeta {

  private static int id_generador = 1;
  private static double[] cargas_permitidas = {2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000};
  protected double max_saldo = 36000;
  protected double limite = -480.0;
  protected double maxCooldown = 0.0;
  protected int id;
  protected string tipo;
  protected double saldo = 0.0;
  protected double excedente = 0.0;
  protected double importe;
  protected bool saldo_negativo;
  protected bool informe_negativo;
  protected double factor;
  protected Frecuente registro_viaje_frecuente;
  protected Tiempo tiempo;
  protected DateTime? ultimo_viaje;

  public Tarjeta (Tiempo tiempo) {

    this.id = id_generador;
    id_generador += 1;
    this.saldo = 0.0;
    this.factor = 1.0;
    this.tipo = "Tarjeta Normal";
    this.tiempo = tiempo;
    this.registro_viaje_frecuente = new Frecuente(this.tiempo.now().Month, 0);
    
  }
  
  public int getId () {

    return this.id;
    
  }
  public bool isCompleta () {

    return this.tipo == "Tarjeta Completa";

  }
  public bool isParcial () {

    return this.tipo == "Tarjeta Parcial";

  }
  private bool checkCarga (double saldo) {

    bool permitido = false;

    for (int i = 0; i < cargas_permitidas.Length; i++) {

      if (saldo == cargas_permitidas[i]) {

        permitido = true;

      }

    }

    if (!permitido) {

      Console.WriteLine("Ha ingresado una carga inválida, no se pudo concretar la operación");

    }

    return permitido;

  }
  private void toggleInformeNegativo () {

    this.informe_negativo = this.saldo_negativo && !(this.isNegativo());
    
  }
  public bool getInformeNegativo () {

    return this.informe_negativo;

  }
  private bool franjaHoraria () {

    DateTime hoy6hs = this.tiempo.now().Date.AddHours(6);
    DateTime hoy22hs = this.tiempo.now().Date.AddHours(22);

    return (this.tiempo.now() >= hoy6hs) && (this.tiempo.now() <= hoy22hs);

  }
  private double manageNewSaldo (double saldo) {

    double new_saldo = this.saldo + excedente + saldo;

    if (new_saldo >= this.max_saldo) {

      excedente = new_saldo - this.max_saldo;
      new_saldo = this.max_saldo;

    } else {

      excedente = 0.0;

    }

    return new_saldo;      

  }
  public void showSaldo () {

    Console.WriteLine("\n----------------------------------------------------");
    Console.WriteLine("Saldo actual: " + this.saldo + " (Excedente: " + this.excedente + ")");
    Console.WriteLine("----------------------------------------------------\n");

  }
  public double getSaldo() {

    return this.saldo;
    
  }
  public double getExcedente() {

    return this.excedente;

  }
  public string getTipo() {

    return this.tipo;

  }
  public Tiempo getTiempo() {

    return this.tiempo;

  }
  public double getDescuento() {

    return this.registro_viaje_frecuente.getDescuento(this.tiempo.now().Month);

  }
  public double getImporte() {

    return this.importe;

  }
  public bool isNegativo() {

    return this.saldo < 0;

  }
  public void setNegativo(bool negativo) {

    this.saldo_negativo = negativo;

  }
  public void addSaldo (double saldo) {

    if (checkCarga(saldo)) {

      this.saldo = manageNewSaldo(saldo);
      this.showSaldo();

      Console.WriteLine("Carga exitosa");
    
    }

  }
  public double getImporte (double precio_neto) {

    double factor = this.factor;

    if (!franjaHoraria()) {

      factor = 1.0;

    } else {

      if ((this is TarjetaCompleta tarjetaCompleta) && !tarjetaCompleta.ViajeGratuito()) {

        factor = 1.0;

      }

      if ((this is TarjetaParcial tarjetaParcial) && !tarjetaParcial.ViajeParcial()) {

        factor = 1.0;

      } 

    }

    if (this is not TarjetaCompleta tc && this is not TarjetaParcial tp && this is not TarjetaGratuita tg) {

      factor = this.getDescuento();

    }

    return this.importe = precio_neto * factor;

  }
  public bool comprarPasaje (double precio) {

    double precio_final = getImporte(precio);

    if (saldo - precio_final >= limite) {

      if ((this is TarjetaParcial tarjetaParcial) && !tarjetaParcial.hasCooldownElapsed()) {

        Console.WriteLine("En tarjetas parciales debe esperar 5 minutos entre operaciones");

      } else {

        saldo = manageNewSaldo(-precio_final);

        bool negativo_question = isNegativo();
        toggleInformeNegativo();
        this.saldo_negativo = negativo_question;
        this.ultimo_viaje = tiempo.now();
        return true;

      }
      
    } else {
    
      Console.WriteLine("No dispone de suficiente saldo");
      
    }

    return false;
    
  }
   
}