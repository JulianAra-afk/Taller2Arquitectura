using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class RecibosController : ControllerBase
{
    private readonly PasarelaContext _context;

    public RecibosController(PasarelaContext context)
    {
        _context = context;
    }

    // Método para crear un recibo
    [HttpPost("crear")]
    public async Task<IActionResult> CrearRecibo([FromBody] Recibo recibo)
    {
        recibo.Pagado = false; // Inicialmente, el recibo no está pagado
        recibo.MontoPagado=0;
        _context.Recibos.Add(recibo);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(ObtenerReciboPorId), new { id = recibo.Id }, recibo);
    }

    // Método para obtener un recibo por su ID
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerReciboPorId(int id)
    {
        var recibo = await _context.Recibos.FindAsync(id);
        if (recibo == null)
        {
            return NotFound();
        }
        return Ok(recibo);
    }

    // Método para obtener todos los recibos
    [HttpGet("todos")]
    public async Task<IActionResult> ObtenerTodosLosRecibos()
    {
        var recibos = await _context.Recibos.ToListAsync();
        return Ok(recibos);
    }

    // Método para pagar un recibo completamente
    [HttpPost("pagar/{id}")]
    public async Task<IActionResult> PagarRecibo(int id)
    {
        var recibo = await _context.Recibos.FindAsync(id);
        if (recibo == null)
        {
            return NotFound();
        }

        if (recibo.Pagado)
        {
            return BadRequest("El recibo ya está pagado.");
        }

        recibo.Pagado = true;
        recibo.MontoPagado=recibo.Monto;
        _context.Entry(recibo).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(recibo);
    }

    [HttpPost("pagar-parcial/{id}/{montoPago}")]
    public async Task<IActionResult> PagarParcial(int id, decimal montoPago)
    {
        var recibo = await _context.Recibos.FindAsync(id);
        if (recibo == null)
        {
            return NotFound();
        }

        if (recibo.Pagado)
        {
            return BadRequest("El recibo ya está pagado.");
        }

        // Verificar que el monto a pagar no sea mayor que el total pendiente
        if (montoPago > (recibo.Monto - recibo.MontoPagado))
        {
            return BadRequest("El monto a pagar es mayor que el saldo pendiente.");
        }

        // Descontar el monto del saldo
        recibo.MontoPagado += montoPago;

        // Si el monto pagado es igual al total, marcar como pagado
        if (recibo.MontoPagado >= recibo.Monto)
        {
            recibo.Pagado = true;
        }

        _context.Entry(recibo).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(recibo);
    }
}
