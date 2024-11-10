public class Recibo
{
    public int Id { get; set; }
    public string ?Descripcion { get; set; }
    public decimal Monto { get; set; }
    public decimal MontoPagado{get;set;}
    public bool Pagado { get; set; } = false; // Por defecto no está pagado
}