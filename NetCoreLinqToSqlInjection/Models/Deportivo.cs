namespace NetCoreLinqToSqlInjection.Models
{
    public class Deportivo: ICoche
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        public int Velocidad { get; set; }
        public int VelocidadMaxima { get; set; }
        public Deportivo()
        {
            this.Marca = "Buggati";
            this.Modelo = "Ni papa";
            this.Imagen = "deportivo.jpeg";
            this.Velocidad = 0;
            this.VelocidadMaxima = 320;
        }

        public void Acelerar()
        {
            this.Velocidad += 30;
            if (this.Velocidad >= this.VelocidadMaxima)
            {
                this.Velocidad = this.VelocidadMaxima;
            }
        }

        public void Frenar()
        {
            this.Velocidad -= 10;
            if (this.Velocidad < 0)
            {
                this.Velocidad = 0;
            }
        }
    }
}
