
using System;

public class TiempoFalso : Tiempo {
  
  public DateTime tiempo;

  public TiempoFalso () {

    tiempo = new DateTime(2024, 11, 4);
    
  }

  public override DateTime now () {

    return tiempo;

  }

  public void addHours (int hours) {

    this.tiempo = this.tiempo.AddHours(hours);

  }
  public void addMinutes (int minutes) {

    this.tiempo = this.tiempo.AddMinutes(minutes);

  }

  public void addSeconds (int seconds) {

    this.tiempo = this.tiempo.AddSeconds(seconds);

  }
   
}
