
using System;
using System.ComponentModel;
using System.Security.AccessControl;

public class Boleto
{

    private static int id_generador = 1;
    private int id;
    private bool interurbana;
    private int id_de_tarjeta;
    private string tipo_de_tarjeta;
    private double importe;
    private double saldo;
    private double excedente;
    private bool informe_negativo;
    private DateTime fecha;

    public Boleto(Tarjeta tarjeta, bool interurbana, double importe, double saldo, double excedente)
    {

        this.id = id_generador;
        this.interurbana = interurbana;
        id_generador += 1;
        this.id_de_tarjeta = tarjeta.getId();
        this.tipo_de_tarjeta = tarjeta.getTipo();
        this.importe = importe;
        this.saldo = saldo;
        this.excedente = excedente;
        this.informe_negativo = tarjeta.getInformeNegativo();
        this.fecha = tarjeta.getTiempo().now();

    }

    public int getId()
    {

        return this.id;

    }

    public bool getInterurbana()
    {

        return this.interurbana;

    }

    public int getIdDeTarjeta()
    {

        return this.id_de_tarjeta;

    }

    public string getTipoDeTarjeta()
    {

        return this.tipo_de_tarjeta;

    }

    public double getExcedente()
    {

        return this.excedente;

    }

    public DateTime getFecha()
    {

        return this.fecha;

    }

    public bool getInformeNegativo()
    {

        return this.getInformeNegativo();

    }

    public double getImporte()
    {

        return this.importe;

    }

    public double getSaldo()
    {

        return this.saldo;

    }

    public void showBoleto()
    {

        Console.WriteLine("\n----------------------------------------------------");

        if (this.interurbana)
        {

            Console.WriteLine("Boleto " + this.id + " (Interurbano)\n");

        }
        else
        {

            Console.WriteLine("Boleto " + this.id + " (Urbano)\n");

        }

        Console.WriteLine("Fecha: " + this.fecha + "\n");

        Console.WriteLine("Tarjeta: " + this.id_de_tarjeta + ", " + this.tipo_de_tarjeta);

        Console.WriteLine("Importe: " + this.importe);
        Console.WriteLine("Saldo restante: " + this.saldo + " (Excedente: " + this.excedente + ")");

        if (this.informe_negativo)
        {

            Console.WriteLine("(Saldo negativo cancelado)");

        }

        Console.WriteLine("----------------------------------------------------\n");

    }

}