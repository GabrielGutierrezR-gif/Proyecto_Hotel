namespace Hotel_Utilitarios
{
    public class Respuesta<T>
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public T? Datos { get; set; }
        public List<string> Errores { get; set; } = new();

        public static Respuesta<T> Ok(T datos, string mensaje = "Operación exitosa.")
        {
            return new Respuesta<T>
            {
                Exito = true,
                Mensaje = mensaje,
                Datos = datos
            };
        }

        public static Respuesta<T> Error(string mensaje, List<string>? errores = null)
        {
            return new Respuesta<T>
            {
                Exito = false,
                Mensaje = mensaje,
                Errores = errores ?? new List<string>()
            };
        }
    }
}